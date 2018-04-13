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
    }
}
