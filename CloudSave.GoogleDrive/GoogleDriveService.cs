using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudSave.Connector;

namespace CloudSave.GoogleDrive
{
    public class GoogleDriveService : CloudService
    {
        public override string Name => "GoogleDrive";

        public GoogleDriveService(ICloudServiceSetting settings) 
            : base(settings)
        {
        }

        public override Control CreateControl()
        {
            return new ConnectControl(this);
        }
    }
}
