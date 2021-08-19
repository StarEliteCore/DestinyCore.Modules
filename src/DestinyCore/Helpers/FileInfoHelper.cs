using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DestinyCore.Helpers
{
    /// <summary>
    /// 文件帮助
    /// </summary>
    public class FileInfoHelper
    {


        /// <summary>
        /// 读取全部文本
        /// </summary>
        /// <param name="fileInfo">文件信息接口</param>
        /// <returns></returns>
        public static string ReadAllText(IFileInfo fileInfo)
        {
            byte[] buffer;
            using var stream = fileInfo.CreateReadStream();
            buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return Encoding.Default.GetString(buffer).Trim();
        }


        /// <summary>
        /// 异步读取全部文本
        /// </summary>
        /// <param name="fileInfo">文件信息接口</param>
        ///<param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ValueTask<string> ReadAllTextAsync(IFileInfo fileInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            byte[] buffer;
            using var stream = fileInfo.CreateReadStream();
            buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
            unsafe
            {
                fixed (byte* buffer2 = buffer)
                {
                    int pDblBuff = (int)buffer2;
                    return Encoding.Default.GetString(buffer2, pDblBuff).Trim();
                }
            }
          
        }
    }
}
