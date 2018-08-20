using System;

using CloudSave.Connector;

namespace CloudSave.Services
{
    public enum UploadType : short
    {
        Upload = 0,
        Update = 1,
        Delete = 2
    }

    public class UploadEventArgs : IUploadEventArgs
    {
        public UploadType Type { get; set; }
        
        public ICloudService Provider { get; set; }

        public string FileName { get;set; }
        
        public UploadEventArgs(string fileName)
        {
            FileName = fileName;
        }
        
        public UploadEventArgs(string fileName, UploadType type)
        {
            FileName = fileName;
            Type = type;
        }
    }

    public class ErrorUploadEventArgs : UploadEventArgs, IErrorUploadEventArgs
    {
        #region Implementation of IErrorUploadEventArgs
        
        public Exception Exception { get; set; }
        
        public bool Handled { get; set; } = false;

        #endregion
        
        public ErrorUploadEventArgs(string fileName, Exception exception) : base(fileName)
        {
            Exception = exception;
        }
        
        public ErrorUploadEventArgs(string fileName, Exception exception, UploadType type) : base(fileName, type)
        {
            Exception = exception;
        }
    }
}