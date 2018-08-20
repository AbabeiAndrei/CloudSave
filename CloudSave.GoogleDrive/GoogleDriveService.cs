using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;

using CloudSave.Connector;
using CloudSave.GoogleDrive.Extensions;
using CloudSave.GeneralLibrary.Extensions;

using Google.Apis.Http;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2;

using MimeTypes;

using File = Google.Apis.Drive.v3.Data.File;

namespace CloudSave.GoogleDrive
{
    public class GoogleDriveService : CloudService
    {
        private const string SERVICE_NAME = "GoogleDrive";
        private const string ORIGINAL_PATH_KEY = "ORIGINAL_PATH_KEY";
        private const string APPS_FOLDER_MIME = "application/vnd.google-apps.folder";
        private const string TOKEN_FILE = "token1" + SERVICE_NAME +".json";

        private static readonly string[] Scopes = { DriveService.Scope.Drive };
        private static string _folderId;

        public override string Name => SERVICE_NAME;

        public GoogleDriveService(ICloudServiceSetting settings) 
            : base(settings)
        {
        }

        public override Control CreateControl()
        {
            return new ConnectControl(this);
        }
        
        public override async Task UploadFile(string file, CancellationToken cancellationToken = default(CancellationToken))
        {
            if(Settings?.Authentication == null)
                throw new SettingsPropertyNotFoundException("Cannot found authentication informations");

            var credential = await GenerateCredentials(cancellationToken);
            using (var service = CreateService(credential))
                await UploadFile(service, file, cancellationToken);
        }

        public override async Task UpdateFile(string file, CancellationToken cancellationToken = default(CancellationToken))
        {
            if(Settings?.Authentication == null)
                throw new SettingsPropertyNotFoundException("Cannot found authentication informations");

            var credential = await GenerateCredentials(cancellationToken);

            using(var service = CreateService(credential))
            {
                await EnsureAppFolderExists(service, cancellationToken);

                var fileId = await GetFileId(service, file, cancellationToken);

                if (!string.IsNullOrEmpty(fileId))
                {
                    var requestDelete = service.Files.Delete(fileId);
                    await requestDelete.ExecuteAsync(cancellationToken);
                }

                await UploadFile(service, file, cancellationToken);
            }
        }
        
        public override async Task<Stream> GetFile(string file, CancellationToken cancellationToken = default(CancellationToken))
        {
            if(Settings?.Authentication == null)
                throw new SettingsPropertyNotFoundException("Cannot found authentication informations");

            var credential = await GenerateCredentials(cancellationToken);

            using (var service = CreateService(credential))
            {
                await EnsureAppFolderExists(service, cancellationToken);

                var fileId = await GetFileId(service, file, cancellationToken);

                if (string.IsNullOrEmpty(fileId))
                    return null;

                var request = service.Files.Get(fileId);
                var stream = new MemoryStream();

                await request.DownloadAsync(stream, cancellationToken);

                return stream;
            }
        }

        private static async Task UploadFile(DriveService service, string file, CancellationToken cancellationToken)
        {
            await EnsureAppFolderExists(service, cancellationToken);

            var fileMetadata = GenerateFile(file);

            using (var stream = new FileStream(file, FileMode.Open))
            {
                var request = service.Files.Create(fileMetadata, stream, fileMetadata.MimeType);
                request.Fields = "id";
                await request.UploadAsync(cancellationToken);
            }
        }

        private static async Task<string> GetFileId(DriveService service, string file, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = service.Files.List();
            request.Q = $"name = '{GetFileName(file)}' and '{_folderId}' in parents";
            request.Spaces = "drive";
            request.Fields = "nextPageToken, files(id, name)";
            request.PageToken = null;
            var result = await request.ExecuteAsync(cancellationToken);
            return result.Files?.FirstOrDefault()?.Id;
        }

        private async Task<UserCredential> GenerateCredentials(CancellationToken cancellationToken)
        {
            return await GoogleWebAuthorizationBroker.AuthorizeAsync(Settings.Authentication.ToClientSecrets(),
                                                                      Scopes, "user", cancellationToken, new FileDataStore(TOKEN_FILE));
        }

        private DriveService CreateService(IConfigurableHttpClientInitializer credential)
        {
            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = Name.ToApplicationName()
            });
        }

        private static File GenerateFile(string file)
        {
            return new File
            {
                Name = GetFileName(file),
                MimeType = MimeTypeMap.GetMimeType(Path.GetExtension(file)),
                Properties = new Dictionary<string, string>
                {
                    {ORIGINAL_PATH_KEY, file}
                },
                Parents = new List<string>
                {
                    _folderId
                }
            };
        }

        private static async Task EnsureAppFolderExists(DriveService service, CancellationToken cancellationToken)
        {
            if(!string.IsNullOrEmpty(_folderId))
                return;

            var request = service.Files.List();
            request.Q = $"mimeType='{APPS_FOLDER_MIME}' and name='{service.ApplicationName}\'";
            request.Spaces = "drive";
            request.Fields = "nextPageToken, files(id, name)";
            request.PageToken = null;

            var result = await request.ExecuteAsync(cancellationToken);

            if (result.Files.Count > 0)
                _folderId = result.Files.FirstOrDefault(f => f.Name == service.ApplicationName)?.Id;

            if (string.IsNullOrEmpty(_folderId))
            {
                var folderMeta = new File
                {
                    Name = service.ApplicationName,
                    MimeType = APPS_FOLDER_MIME
                };

                var requestF = service.Files.Create(folderMeta);
                requestF.Fields = "id";

                var folder = await requestF.ExecuteAsync(cancellationToken);
                _folderId = folder.Id;
            }
        }

        private static string GetFileName(string file)
        {
            return Path.GetFileName(file) + "_" + Path.GetDirectoryName(file)?.Replace('\\', '_').Replace(':', '_');
        }
    }
}
