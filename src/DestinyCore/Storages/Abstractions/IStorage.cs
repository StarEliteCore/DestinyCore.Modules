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
        /// 释放存储所使用的所有资源。
        /// </summary>
        void Clear();

        /// <summary>
        /// 将写入的数据刷新到存储器中。   
        /// </summary>
        void Flush();

        /// <summary>
        ///  关闭当前流并释放任何资源(如套接字和文件句柄)
        ///  与当前流关联的。而不是调用这个方法，确保
        ///  流被适当地配置。
        /// </summary>
        void Close();

        /// <summary>
        /// 获取以字节为单位的存储长度。
        /// </summary>
        long GetLength();
    }
}
