using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using CloudSave.Annotations;

using Newtonsoft.Json;

using CloudSave.Services;
using CloudSave.Connector;
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
        }

        #endregion

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            SetFormVisibility(false);
            LoadSettings();
            LoadServices();

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
            Settings.Default.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Settings.Default.Reload();
            LoadServices();
            LoadSettings();
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

                var listServices = new List<CloudService>();
                    
                foreach (var service in services.Services)
                {
                    var cloudServices = CreateServiceControl(service, _settings).ToList();

                    listServices.AddRange(cloudServices);

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

                CreateLocationColumns(listServices);

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

        private void DisposeComponents(bool isDisposing)
        {
            if (!isDisposing) 
                return;

            _trayIcon.Dispose();
            _trayMenu.Dispose();
        }

        #endregion
    }
}

