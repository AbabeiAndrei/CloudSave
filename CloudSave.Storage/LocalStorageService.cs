using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

using CloudSave.Connector;

namespace CloudSave.Storage
{
    public class LocalStorageService : CloudService
    {

        public LocalStorageService(ICloudServiceSetting settings) 
                : base(settings)
        {
        }

        #region Overrides of CloudService
        
        public override string Name => "Local";
        
        public override Control CreateControl()
        {
            return new ConnectControl(Settings);
        }
        
        public override Task<Stream> GetFile(string file, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Settings == null || string.IsNullOrEmpty(Settings.Endpoint))
                return null;

            var path = Path.Combine(Settings.Endpoint, file);

            return File.Exists(path) 
                           ? Task.FromResult((Stream)File.OpenRead(path)) 
                           : Task.FromResult((Stream)null);
        }
        
        public override Task UploadFile(string file, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
        
        public override Task UpdateFile(string file, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    
}
