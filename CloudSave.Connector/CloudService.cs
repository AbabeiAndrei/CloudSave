using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using CloudSave.Connector.Annotations;
using CloudSave.Connector.Auth;

namespace CloudSave.Connector
{
    public abstract class CloudService : INotifyPropertyChanged
    {
        #region Fields

        private ICloudServiceSetting _settings;

        #endregion

        #region Properties

        public abstract string Name { get; }

        public ICloudServiceSetting Settings
        {
            get => _settings;
            set
            {
                if (Equals(_settings, value)) 
                    return;

                _settings = value;
                OnPropertyChanged();
            }
        }

        public bool IsConnected => Settings != null && Settings.Authentication.HaveValues();

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        protected CloudService(ICloudServiceSetting settings)
        {
            Settings = settings;
        }

        #endregion

        #region Public methods

        public abstract Control CreateControl();

        #endregion

        #region Protected methods

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
