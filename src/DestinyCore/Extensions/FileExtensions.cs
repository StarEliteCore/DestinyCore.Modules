using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DestinyCore.Extensions
{
    public static partial class FileExtensions
    {

        /// <summary>
        /// 判断是否文件
        /// </summary>
        /// <param name="fileName">要判断文件名</param>
        /// <param name="fileExtension">要判断文件后缀名</param>
        /// <returns>如何是返回ture,则返回false</returns>
        public static bool IsFile(this string fileName, string fileExtension)
        {
            return Path.GetExtension(fileName).ToLower() == fileExtension ? true : false;
        }

        /// <summary>
        /// 是否Txt文件
        /// </summary>
        /// <param name="fileName">要判断名字</param>
        /// <returns>是true/否false</returns>
        public static bool IsTxtFile(this string fileName)
        {
            return fileName.IsFile(".txt");
        }

        /// <summary>
        /// 异步将内存流转储成文件
        /// </summary>
        /// <param name="ms">内存流</param>
        /// <param name="fileName">文件名</param>
        /// <param name="cancellationToken">文件名</param>
        public static async Task SaveFileAsync(this MemoryStream ms, string fileName, CancellationToken cancellationToken = default(CancellationToken))
        {
            fileName.NotNullOrEmpty(nameof(fileName));
            using var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            byte[] buffer = ms.ToArray(); // 转化为byte格式存储
            await fs.WriteAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
            await fs.FlushAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
