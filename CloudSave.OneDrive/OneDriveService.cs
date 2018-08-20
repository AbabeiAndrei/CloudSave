using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using CloudSave.Connector;
using CloudSave.GeneralLibrary.Extensions;

namespace CloudSave.OneDrive
{
    public class OneDriveService : CloudService
    {
        /// <inheritdoc />
        public OneDriveService(ICloudServiceSetting settings) : base(settings)
        {
            settings.Endpoint.ToApplicationName();

        }

        #region Overrides of CloudService

        /// <inheritdoc />
        public override string Name => "OneDrive";

        /// <inheritdoc />
        public override Control CreateControl()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override Task<Stream> GetFile(string file, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override Task UploadFile(string file, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override Task UpdateFile(string file, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
