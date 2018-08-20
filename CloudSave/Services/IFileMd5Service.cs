namespace CloudSave.Services
{
    public interface IFileMd5Service
    {
        void Set(string file, string md5);
        bool Contains(string file);
        bool EqualMd5(string file, string md5);
    }
}