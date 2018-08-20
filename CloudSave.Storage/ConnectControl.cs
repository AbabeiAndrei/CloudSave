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

namespace CloudSave.Storage
{
    public partial class ConnectControl : UserControl
    {
        private readonly ICloudServiceSetting _settings;

        public ConnectControl(ICloudServiceSetting settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void ConnectControl_Load(object sender, EventArgs e)
        {
            if(_settings == null || string.IsNullOrEmpty(_settings.Endpoint))
                return;

            lblPath.Text = _settings.Endpoint;
            btnSelectPath.Text = "Remove";
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_settings.Endpoint))
                if (fbDialog.ShowDialog(ParentForm) == DialogResult.OK)
                    _settings.Endpoint = fbDialog.SelectedPath;
                else
                    return;
            else
                _settings.Endpoint = null;

            ConnectControl_Load(sender, e);
        }
    }
}
