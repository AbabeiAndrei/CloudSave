using System;

using CloudSave.Connector;

namespace CloudSave.Services
{
    public interface IUploadEventArgs
    {
        string FileName { get; set; }
        UploadType Type { get; set; }
        ICloudService Provider { get; set; }
    }

    public interface IErrorUploadEventArgs : IUploadEventArgs
    {
        Exception Exception { get; set; }
        bool Handled { get; set; }
    }
}