using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

namespace CloudSave.Services
{
    public class FileMd5Service : IFileMd5Service
    {
        private readonly string _file;

        private readonly IDictionary<string, string> _md5S;

        public FileMd5Service(string file)
        {
            _file = file;

            _md5S = File.Exists(file) 
                        ? JsonConvert.DeserializeObject<IDictionary<string, string>>(File.ReadAllText(file))
                        : new Dictionary<string, string>();
        }

        public virtual void Set(string file, string md5)
        {
            if (_md5S.ContainsKey(file))
                _md5S[file] = md5;
            else
                _md5S.Add(file, md5);

            File.WriteAllText(_file, JsonConvert.SerializeObject(_md5S));
        }

        public virtual bool Contains(string file) => _md5S.ContainsKey(file);
        
        public virtual bool EqualMd5(string file, string md5) => Contains(file) && _md5S[file] == md5;
    }
}
