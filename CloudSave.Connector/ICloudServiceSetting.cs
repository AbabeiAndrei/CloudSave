using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSave.Connector.Auth;

namespace CloudSave.Connector
{
    public interface ICloudServiceSetting
    {
        string Endpoint { get; set; }
        ICloudServiceAuth Authentication { get; }
        IList<string> Locations { get; set; }
    }
}
