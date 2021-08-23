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

        /// <summary>
        /// 获取磁盘上的可用空闲空间
        /// </summary>
        /// <param name="directory">目录</param>
        /// <returns></returns>
        public static long GetAvailableFreeSpaceOnDisk(string directory)
        {
            try
            {
                var drive = new DriveInfo(directory);
                if (drive.IsReady)
                {
                    return drive.AvailableFreeSpace;
                }

                return 0L;
            }
            catch (ArgumentException)
            {
                return 0L;
            }
        }

        /// <summary>
        /// 如果空间不够
        /// </summary>
        /// <param name="actualNeededSize">实际需要的大小</param>
        /// <param name="directories">目录</param>
        public static void ThrowIfNotEnoughSpace(long actualNeededSize, params string[] directories)
        {
            if (directories != null)
            {
                foreach (string directory in directories)
                {
                    var availableFreeSpace = GetAvailableFreeSpaceOnDisk(directory);
                    if (availableFreeSpace > 0 && availableFreeSpace < actualNeededSize)
                    {
                        throw new IOException($"磁盘空间不足。处理步骤 `{directory}` 与 {availableFreeSpace} 字节");
                    }
                }
            }
        }

    }
}
