using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CloudSave.Services
{
    public class ServiceConfig
    {
        public IEnumerable<Service> Services { get; set; }
    }

    public class Service
    {
        [JsonProperty("ServiceName")]
        public string Name { get;set; }

        [JsonProperty("ServicePlugin")]
        public string File { get; set; }
    }
}
