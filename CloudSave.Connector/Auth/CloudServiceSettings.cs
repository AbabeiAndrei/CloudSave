using System.Collections.Generic;

namespace CloudSave.Connector.Auth
{
    public class CloudServiceSettings : ICloudServiceSettings
    {
        #region Properties

        /// <inheritdoc />
        public IDictionary<string, ICloudServiceSetting> Settings { get; set; }

        public ICloudServiceSetting this[string key]
        {
            get => Settings.ContainsKey(key)
                           ? Settings[key]
                           : null;
            set
            {
                if (Settings.ContainsKey(key))
                    Settings[key] = value;
                else
                    Settings.Add(key, value);
            }
        }

        #endregion

        #region Constructors

        public CloudServiceSettings()
        {
            Settings = new Dictionary<string, ICloudServiceSetting>();
        }

        #endregion
    }

    public class CloudServiceSetting : ICloudServiceSetting
    {
        private string _endpoint;

        #region Properties

        public virtual string Endpoint
        {
            get => _endpoint;
            set => _endpoint = value;
        }

        public virtual ICloudServiceAuth Authentication { get; }
        
        public IList<string> Locations { get; set; }

        #endregion

        #region Constructors

        public CloudServiceSetting(string endpoint, ICloudServiceAuth authentication)
        {
            _endpoint = endpoint;
            Authentication = authentication;
        }

        #endregion

        #region Equality members

        protected bool Equals(CloudServiceSetting other)
        {
            return string.Equals(Endpoint, other.Endpoint) && Equals(Authentication, other.Authentication);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) 
                return false;

            if (ReferenceEquals(this, obj)) 
                return true;

            if (obj.GetType() != GetType()) 
                return false;

            return Equals((CloudServiceSetting) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Endpoint != null ? Endpoint.GetHashCode() : 0) * 397) ^ (Authentication != null ? Authentication.GetHashCode() : 0);
            }
        }

        #endregion
    }
}
