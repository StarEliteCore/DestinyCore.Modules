using System.IO;

namespace DestinyCore.Extensions
{
    public  static partial class FileExtensions
    {

        /// <summary>
        /// 判断是否文件
        /// </summary>
        /// <param name="fileName">要判断文件名</param>
        /// <param name="fileExtension">要判断文件后缀名</param>
        /// <returns>如何是返回ture,则返回false</returns>
        public static bool IsFile(this string fileName,string fileExtension)
        {

            if (Path.GetExtension(fileName).ToLower() == fileExtension) //txt文件
            {

                return true;
            }
            return false;

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
    }
}
