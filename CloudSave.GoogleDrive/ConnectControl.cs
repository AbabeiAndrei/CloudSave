using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudSave.Connector;
using CloudSave.Services;

using ConnectionState = CloudSave.Connector.ConnectionState;

namespace CloudSave.GoogleDrive
{
    public partial class ConnectControl : UserControl
    {
        private readonly CloudService _service;

        public ConnectControl(CloudService service)
        {
            InitializeComponent();
            _service = service;
        }

        private void btnGoogleDriveConnect_Click(object sender, EventArgs e)
        {
            _service.Settings = new CloudServiceSetting("aaaa", new CloudServiceAuth("aa", "aaaa"));
            _service.State = ConnectionState.Connected;
        }
    }
}
