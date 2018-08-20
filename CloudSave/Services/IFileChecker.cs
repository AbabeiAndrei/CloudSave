using System;
using System.Threading;

namespace CloudSave.Services
{
    public interface IFileChecker : IDisposable
    {
        int CheckTimeout { get; }

        void Run(CancellationToken token);
        void Stop();

        event UploadEventHandler StartUploading;
        event UploadEventHandler FinishedUploading;
        event ErrorUploadEventHandler ErrorUploading;
    }

    public delegate void UploadEventHandler(object sender, IUploadEventArgs args);
    public delegate void ErrorUploadEventHandler(object sender, IErrorUploadEventArgs args);
}