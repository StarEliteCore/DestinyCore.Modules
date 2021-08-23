using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DestinyCore.Storages
{
    /// <summary>
    /// 存储接口 
    /// </summary>
    public interface IStorage
    {

        /// <summary>
        /// 打开现有存储器进行读取
        /// </summary>
        Stream OpenRead();

        /// <summary>
        ///  异步地将一个字节序列写入当前存储。
        /// </summary>
        /// <param name="data">要写入数据的字节数组。</param>
        /// <param name="offset">数据中开始将字节复制到存储器的基于零的字节偏移量。</param>
        /// <param name="count">要写入的最大字节数。</param>
        Task WriteAsync(byte[] data, int offset, int count);

        /// <summary>
        ///     Release all resources used by the storage.
        /// </summary>
        void Clear();

        /// <summary>
        ///     Flush written data to storage.
        /// </summary>
        void Flush();

        /// <summary>
        ///     Closes the current stream and releases any resources (such as sockets and file handles)
        ///     associated with the current stream. Instead of calling this method, ensure that
        ///     the stream is properly disposed.
        /// </summary>
        void Close();

        /// <summary>
        ///     Gets the length in bytes of the storage.
        /// </summary>
        long GetLength();
    }
}
