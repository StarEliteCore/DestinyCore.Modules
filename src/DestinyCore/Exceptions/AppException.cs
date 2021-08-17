using System;

namespace DestinyCore.Exceptions
{
    /// <summary>
    /// 程序异常
    /// </summary>
    public class AppException : Exception
    {



        public AppException()
        {

        }
        public AppException(string message) : base(message)
        {

        }
        public AppException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public AppException ThrowIf(bool flag)
        {
            if (flag)
            {
                throw this;
            }
            return this;
        }
    }
}
