﻿using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading;

using Newtonsoft.Json;

using CloudSave.Services;
using CloudSave.Connector;
using CloudSave.Connector.Auth;
using CloudSave.Properties;
using CloudSave.GeneralLibrary;
using CloudSave.GeneralLibrary.Extensions;

namespace CloudSave
{
    public sealed partial class SettingsForm : Form
    {
        #region Fields

        private readonly NotifyIcon  _trayIcon;
        private readonly ContextMenu _trayMenu;
        private ICloudServiceSettings _settings;
        private List<CloudService> _listServices;
        private IFileMd5Service _fileMd5Map;
        private readonly NotifyIcon _notifyIcon;

        public IFileChecker Runner { get; private set; }

        #endregion

        #region Constructors

        public SettingsForm()
        {
            InitializeComponent();

            _trayMenu = new ContextMenu();
            _trayMenu.MenuItems.Add("Show", ShowForm);
            _trayMenu.MenuItems.Add("-");
            _trayMenu.MenuItems.Add("Exit", ExitApplication);
            _trayIcon = new NotifyIcon
            {
                Text = Text,
                Icon = new Icon(SystemIcons.Application, 40, 40),
                ContextMenu = _trayMenu,
                Visible = true
            };
            _trayIcon.DoubleClick += ShowForm;
            
            _notifyIcon = new NotifyIcon
            {
                Visible = true
            };
        }

        #endregion

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            const string md5File = "Services\\md5.map";

            SetFormVisibility(false);
            LoadServices();
            LoadSettings();
            
            _fileMd5Map = new FileMd5Service(md5File);
            Runner = new FileChecker(_listServices, _fileMd5Map);
            Runner.StartUploading += (sender, args) => ShowNotification(args.Type.ToString(), args.FileName, args.Provider?.Icon);
            Runner.FinishedUploading += (sender, args) => ShowNotification($"Finished {args.Type}ing", args.FileName, args.Provider?.Icon);
            Runner.ErrorUploading += (sender, args) => ShowNotification($"Error {args.Type}ing", args.FileName + Environment.NewLine + args.Exception.Message, args?.Provider?.Icon);

            Runner.Run(CancellationToken.None);

            base.OnLoad(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            SetFormVisibility(false);
            base.OnClosing(e);
        }
        
        #endregion

        #region Event handlers

        private void ShowForm(object sender, EventArgs e)
        {
            SetFormVisibility(true);
        }

        private static void ExitApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void CloudServiceSettingChanged(object sender, PropertyChangedEventArgs e)
        {
            if(!(sender is CloudService service))
                return;
            _settings[service.Name] = service.Settings;
            Settings.Default.CloudServiceSettings = JsonConvert.SerializeObject(_settings);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (var service in _listServices)
                service.Settings.Locations = new List<string>();

            foreach (DataGridViewRow row in dgvLocations.Rows)
            {
                if(row.Cells[0].Value == null)
                    continue;
                
                var location = row.Cells[0].Value.ToString();

                foreach (var service in _listServices)
                {
                    if((bool)row.Cells[service.Name].Value)
                        service.Settings.Locations.Add(location);
                }
            }

            Settings.Default.LaunchAtStartup = chkStartOnStartup.Checked;
            Settings.Default.ShowNotifications = chkShowNotifications.Checked;
            Settings.Default.CloudServiceSettings = JsonConvert.SerializeObject(_settings);

            Settings.Default.Save();

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Settings.Default.Reload();
            LoadSettings();
            LoadServices();
        }

        #endregion

        #region Private methods

        private void SetFormVisibility(bool visible)
        {
            Visible = visible;
            ShowInTaskbar = visible;
        }

        private void LoadServices()
        {
            const string servicesFilePath = "Services\\Services.json";

            if(!File.Exists(servicesFilePath))
                return;

            var servicesJson = File.ReadAllText(servicesFilePath);
            var services = JsonConvert.DeserializeObject<ServiceConfig>(servicesJson);

            if(services == null)
                return;

            var settingsSerialized = Settings.Default.CloudServiceSettings;

            var converters = new JsonConverter[]
            {
                new GenericJsonConverter<CloudServiceSettings>(),
                new GenericJsonConverter<CloudServiceSetting>(),
                new GenericJsonConverter<CloudServiceAuth>()
            };

            if(_settings == null)
                _settings = !string.IsNullOrEmpty(settingsSerialized) 
                                    ? JsonConvert.DeserializeObject<ICloudServiceSettings>(settingsSerialized, converters) 
                                    : new CloudServiceSettings();

            try
            {
                flwServices.SuspendLayout();
                dgvLocations.SuspendLayout();

                foreach (var disposable in flwServices.Controls.OfType<IDisposable>())
                    disposable.Dispose();

                flwServices.Controls.Clear();

                _listServices = new List<CloudService>();
                    
                foreach (var service in services.Services)
                {
                    var cloudServices = CreateServiceControl(service, _settings).ToList();

                    _listServices.AddRange(cloudServices);

                    var controls = cloudServices.Foreach(cs =>
                                                         {
                                                             cs.PropertyChanged += CloudServiceSettingChanged;
                                                             cs.ConnectionChanged += (s, e) =>
                                                             {
                                                                 LoadServices();
                                                                 LoadSettings();
                                                             };
                                                         })
                                                .Select(cs => cs.CreateControl());

                    flwServices.Controls.AddRange(controls.ToArray());
                }

                CreateLocationColumns(_listServices);

                if(_settings.Settings.Count <= 0)
                    ShowForm(this, EventArgs.Empty);
            }
            finally
            {
                dgvLocations.ResumeLayout();
                flwServices.ResumeLayout();
            }

        }

        private void CreateLocationColumns(IEnumerable<CloudService> listServices)
        {
            if (listServices == null) 
                throw new ArgumentNullException(nameof(listServices));

            dgvLocations.Columns.Clear();
            dgvLocations.Columns.AddRange(listServices.Where(cs => cs.IsConnected)
                                                      .Select(CreateLocationsServiceColumn)
                                                      .Insert(CreateLocationsPathColumn())
                                                      .ToArray());
        }

        private static DataGridViewColumn CreateLocationsPathColumn()
        {
            return new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                HeaderText = Resources.SettingsForm_CreateLocationsPathColumn_Location
            };
        }

        private static DataGridViewColumn CreateLocationsServiceColumn(CloudService arg)
        {
            return new DataGridViewCheckBoxColumn
            {
                HeaderText = arg.Name,
                Name = arg.Name
            };
        }

        private void LoadSettings()
        {
            chkStartOnStartup.Checked = Settings.Default.LaunchAtStartup;
            chkShowNotifications.Checked = Settings.Default.ShowNotifications;
            LoadLocations();
        }

        private void LoadLocations()
        {
            try
            {
                dgvLocations.SuspendLayout();

                dgvLocations.Rows.Clear();

                
                if(_settings == null)
                    return;

                var items = _settings.Settings.Where(kvp => kvp.Value.Locations?.Any() ?? false)
                                     .SelectMany(kvp => kvp.Value.Locations)
                                     .Select(localtion => 
                                     (
                                        Location: localtion, 
                                        Services: _settings.Settings.Where(kvp => kvp.Value.Locations.Contains(localtion))
                                                           .Select(kvp => kvp.Key)
                                     ));

                foreach (var item in items)
                    CreateLocationRow(item);
            }
            finally
            {
                dgvLocations.ResumeLayout();

            }
        }

        private void CreateLocationRow((string Location, IEnumerable<string> Services) setting)
        {
            var values = new List<object>
            {
                setting.Location
            };

            for (var i = 1; i < dgvLocations.ColumnCount; i++)
                values.Add(setting.Services.Contains(dgvLocations.Columns[i].Name));

            dgvLocations.Rows.Add(values.ToArray());
        }

        private static IEnumerable<CloudService> CreateServiceControl(Service service, ICloudServiceSettings settings)
        {
            var cloudServiceType = typeof(CloudService);

            var dll = Assembly.LoadFile(service.File);
            return dll.GetExportedTypes().Where(t => t.IsSubclassOf(cloudServiceType))
                      .Select(t => (CloudService)Activator.CreateInstance(t, settings[service.Name]));
        }

        private void ShowNotification(string title, string body, Icon icon = null)
        {
            if(!Settings.Default.ShowNotifications)
                return;

            if (title != null)
                _notifyIcon.BalloonTipTitle = title;

            if (body != null)
                _notifyIcon.BalloonTipText = body;

            _notifyIcon.Icon = icon ?? SystemIcons.Application;
                
            _notifyIcon.ShowBalloonTip(3000);
        }

        private void DisposeComponents(bool isDisposing)
        {
            if (!isDisposing) 
                return;

            _trayIcon.Dispose();
            _trayMenu.Dispose();
            _notifyIcon.Dispose();
            Runner.Dispose();
        }

        #endregion
    }
}

