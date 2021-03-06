﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

using CloudSave.Connector.Annotations;

namespace CloudSave.Connector.Auth
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
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
