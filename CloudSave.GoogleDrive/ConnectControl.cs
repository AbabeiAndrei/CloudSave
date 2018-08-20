using System;
using System.Windows.Forms;

using CloudSave.Connector;
using CloudSave.Connector.Auth;

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

        private void ConnectControl_Load(object sender, EventArgs e)
        {
            lblGoogleDriveState.Text = _service.State.ToString();
            btnGoogleDriveConnect.Text = _service.IsConnected ? "Disconect" : "Connect";
        }

        private void btnGoogleDriveConnect_Click(object sender, EventArgs e)
        {
            _service.State = ConnectionState.Connected;
            _service.Settings = new CloudServiceSetting("",
                                                        new CloudServiceAuth("842799249524-3iabd27qua7a50i3c9d0q5e6gmbnr2i6.apps.googleusercontent.com", 
                                                                             "AQ77WELCv5F1ya2H96CcU_3d"));
        }
    }
}
