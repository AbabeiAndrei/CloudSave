using System.Collections.Generic;

using CloudSave.Connector;

namespace CloudSave.Services
{
    public interface ICloudServiceSettings
    {
        ICloudServiceSetting this[string key] { get; set; }

        IDictionary<string, ICloudServiceSetting> Settings { get; set; }
    }
}