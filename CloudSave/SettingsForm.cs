using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudSave
{
    public partial class SettingsForm : Form
    {
        private NotifyIcon  trayIcon;
        private ContextMenu trayMenu;
 
        public SettingsForm()
        {
            InitializeComponent();
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Show", ShowForm);
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Exit", OnExit);
            trayIcon = new NotifyIcon
            {
                Text = Text,
                Icon = new Icon(SystemIcons.Application, 40, 40),
                ContextMenu = trayMenu,
                Visible = true
            };
            trayIcon.DoubleClick += ShowForm;
        }

        protected override void OnLoad(EventArgs e)
        {
            SetFormVisibility(false);

            base.OnLoad(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            SetFormVisibility(false);
            base.OnClosing(e);
        }

        private void ShowForm(object sender, EventArgs e)
        {
            SetFormVisibility(true);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SetFormVisibility(bool visible)
        {
            Visible = visible;
            ShowInTaskbar = visible;
        }
 
        protected void DisposeComponents(bool isDisposing)
        {
            if (isDisposing)
            {
                // Release the icon resource.
                trayIcon.Dispose();
                trayMenu.Dispose();
            }
        }
    }
}
