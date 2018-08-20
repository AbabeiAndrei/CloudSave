using System.Collections.Generic;

namespace CloudSave.Connector.Auth
{
    public interface ICloudServiceSettings
    {
        ICloudServiceSetting this[string key] { get; set; }

        IDictionary<string, ICloudServiceSetting> Settings { get; set; }
    }
}