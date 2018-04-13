using System.ComponentModel;
using System.Runtime.CompilerServices;

using CloudSave.Annotations;
using CloudSave.Connector.Auth;

namespace CloudSave.Services
{
    public class CloudServiceAuth : ICloudServiceAuth, INotifyPropertyChanged
    {
        private string _clientId;
        private string _clientSecret;

        public virtual string ClientId
        {
            get => _clientId;
            set
            {
                if (value == _clientId) 
                    return;

                _clientId = value;
                OnPropertyChanged(nameof(ClientId));
            }
        }

        public virtual string ClientSecret
        {
            get => _clientSecret;
            set
            {
                if (value == _clientSecret) 
                    return;

                _clientSecret = value;
                OnPropertyChanged(nameof(ClientSecret));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CloudServiceAuth()
        {
        }

        public CloudServiceAuth(string clientId, string clientSecret)
            : this()
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
