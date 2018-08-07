using System;
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
        private ConnectionState _state;

        #endregion

        #region Properties

        public abstract string Name { get; }

        public virtual ICloudServiceSetting Settings
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

        public virtual bool IsConnected => Settings != null && Settings.Authentication.HaveValues();

        public virtual ConnectionState State
        {
            get => _state;
            set
            {
                if (_state == value) 
                    return;

                _state = value;
                OnConnectionChanged();
            }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler ConnectionChanged;

        #endregion

        #region Constructors

        protected CloudService(ICloudServiceSetting settings)
        {
            _settings = settings;
            _state = ConnectionState.Disconected;
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

        protected virtual void OnConnectionChanged()
        {
            ConnectionChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
