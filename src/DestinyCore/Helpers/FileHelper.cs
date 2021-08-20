using DestinyCore.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DestinyCore.Helpers
{
    /// <summary>
    /// 文件帮助
    /// </summary>
    public static  class FileHelper
    {

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>返回创建流</returns>

        public static Stream CreateFile(string filename)
        {
            string directory = Path.GetDirectoryName(filename);
            if (directory.IsNullOrWhiteSpace())
            {
                return Stream.Null;
            }

            if (Directory.Exists(directory) == false)
            {
                Directory.CreateDirectory(directory);
            }

            return new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite | FileShare.Delete);
        }

        /// <summary>
        /// 得到临时文件
        /// </summary>
        /// <returns>返回临时文件路径</returns>

        public static string GetTempFile()
        {
            return GetTempFile(Path.GetTempPath(), string.Empty);
        }


        /// <summary>
        /// 得到临时文件
        /// </summary>
        /// <param name="baseDirectory">基目录</param>
        /// <param name="fileExtension">文件扩展名</param>
        /// <returns>返回临时文件路径</returns>

        public static string GetTempFile(string baseDirectory, string fileExtension)
        {
            if (baseDirectory.IsNullOrWhiteSpace())
            {
                baseDirectory = Path.GetTempPath();
            }

            string filename = Path.Combine(baseDirectory, $"{Guid.NewGuid().ToString("N")}{fileExtension}");
            CreateFile(filename).Dispose();

            return filename;
        }

    }
}
