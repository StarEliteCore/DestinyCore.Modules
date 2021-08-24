using DestinyCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DestinyCore.Storages
{
    [Serializable]
    public class FileStorage : IStorage, IDisposable
    {
        [NonSerialized] private FileStream _stream;
        [NonSerialized] private SemaphoreSlim _streamSynchronizer;
        private string _fileName;
        public string FileName
        {
            get => _fileName ??= FileHelper.GetTempFile();
            set => _fileName = value;
        }

        public FileStorage() { }

        public FileStorage(string fileName)
        {
            if (File.Exists(fileName) == false)
            {
                var directory = Path.GetDirectoryName(fileName);
                var extension = Path.GetExtension(fileName);
                FileName = FileHelper.GetTempFile(directory, extension);
            }
            else
            {
                FileName = fileName;
            }
        }

        public FileStorage(string directory, string fileExtension)
        {
            FileName = FileHelper.GetTempFile(directory, fileExtension);
        }

        public Stream OpenRead()
        {
            Close();
            return File.Open(FileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Delete | FileShare.ReadWrite);
        }

        public async Task WriteAsync(byte[] data, int offset, int count)
        {
            try
            {
                await GetStreamSynchronizer().WaitAsync().ConfigureAwait(false);
                if (_stream?.CanWrite != true)
                {
                    _stream = new FileStream(FileName, FileMode.Append, FileAccess.Write,
                        FileShare.Delete | FileShare.ReadWrite);
                }
                await _stream.WriteAsync(data, offset, count).ConfigureAwait(false);
            }
            finally
            {
                GetStreamSynchronizer().Release();
            }
        }

        public void Clear()
        {
            Close();
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
        }

        public void Flush()
        {
            Close();
        }

        public void Close()
        {
            try
            {
                GetStreamSynchronizer().Wait();
                if (_stream?.CanWrite == true)
                {
                    _stream?.Flush();
                }
                _stream?.Dispose();
            }
            finally
            {
                GetStreamSynchronizer().Release();
            }
        }

        public long GetLength()
        {
            using var stream = OpenRead();
            return stream?.Length ?? 0;
        }

     

        private SemaphoreSlim GetStreamSynchronizer()
        {
            _streamSynchronizer ??= new SemaphoreSlim(1, 1);
            return _streamSynchronizer;
        }

        /// <summary>
        /// 显式调用Dispose方法 
        /// </summary>
        ~FileStorage()
        {
            //必须false
            Dispose(false);
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            //必须true
            Dispose(true);
            //不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

        protected bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                //告诉GC，不要调用析构函数
                GC.SuppressFinalize(this);

            }
            Close();
            _disposed = true;
        }
    }
}
