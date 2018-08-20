using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Security.Cryptography;

using CloudSave.Connector;

namespace CloudSave.Services
{
    public class FileChecker : IFileChecker
    {
        private static readonly object Lockobj = new object();

        private readonly IEnumerable<CloudService> _services;
        private readonly IFileMd5Service _filesMd5;
        private CancellationToken _token;
        private readonly Timer _timer;

        public int CheckTimeout => 5000;

        public FileChecker(IEnumerable<CloudService> services, IFileMd5Service filesMd5)
        {
            _services = services;
            _filesMd5 = filesMd5;
            _timer = new Timer(CallBackCheck, null, -1, CheckTimeout);
        }

        public void Run(CancellationToken token)
        {
            _token = token;
            _token.Register(Stop);
            _timer.Change(CheckTimeout, CheckTimeout);
        }

        public void Stop()
        {
            _timer.Change(-1, CheckTimeout);
        }
        
        public event UploadEventHandler StartUploading;
        
        public event UploadEventHandler FinishedUploading;
        
        public event ErrorUploadEventHandler ErrorUploading;

        private async void CallBackCheck(object state)
        {
            //if (!Monitor.TryEnter(Lockobj)) 
            //    return;

            try
            {
                Stop();
                // we got the lock, do your work
                foreach (var service in _services)
                {
                    foreach (var location in service.Settings.Locations)
                    {
                        if (!Directory.Exists(location))
                            continue;

                        var files = Directory.EnumerateFiles(location, "*.*", SearchOption.AllDirectories);

                        foreach (var file in files)
                        {
                            _token.ThrowIfCancellationRequested();
                            string fileCheckSum;
                        
                            using (var md5 = MD5.Create())
                            using (var stream = File.OpenRead(file))
                            {
                                var fileHash = md5.ComputeHash(stream);
                                fileCheckSum = BitConverter.ToString(fileHash).Replace("-", "").ToLowerInvariant();
                            }

                            var args = new UploadEventArgs(file)
                            {
                                Provider = service
                            };

                            try
                            {
                                if(_filesMd5.Contains(file) && !_filesMd5.EqualMd5(file, fileCheckSum))
                                {
                                    args.Type = UploadType.Upload;
                                    OnStartUploading(args);

                                    await service.UpdateFile(file, _token);
                                }
                                else if (!_filesMd5.Contains(file))
                                {
                                    args.Type = UploadType.Update;
                                    OnStartUploading(args);

                                    await service.UploadFile(file, _token);
                                }
                                else
                                    continue;

                                _filesMd5.Set(file, fileCheckSum);
                                OnFinishedUploading(args);
                            }
                            catch (Exception ex)
                            {
                                var errorArgs = new ErrorUploadEventArgs(args.FileName, ex, args.Type)
                                {
                                    Provider = args.Provider
                                };
                                OnErrorUploading(errorArgs);

                                if (!errorArgs.Handled)
                                    throw;
                            }
                        }
                    }
                }
            }
            finally
            {
                //Monitor.Exit(Lockobj);
                Run(_token);
            }
        }

        protected virtual void OnStartUploading(IUploadEventArgs args)
        {
            StartUploading?.Invoke(this, args);
        }

        protected virtual void OnFinishedUploading(IUploadEventArgs args)
        {
            FinishedUploading?.Invoke(this, args);
        }

        protected virtual void OnErrorUploading(IErrorUploadEventArgs args)
        {
            ErrorUploading?.Invoke(this, args);
        }

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            Stop();
            _timer?.Dispose();
        }

        #endregion
    }
}
