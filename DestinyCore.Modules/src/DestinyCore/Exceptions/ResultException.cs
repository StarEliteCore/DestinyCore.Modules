using System;
using System.Collections.Generic;
using System.Text;

namespace DestinyCore.Exceptions
{
    /// <summary>
    /// 该异常可以公开
    /// </summary>
    public  class ResultException : Exception
    {
    
        internal ResultException(string message) : base(message)
        {

        }
        internal ResultException(string message, Exception innerException) : base(message, innerException)
        {

        }


        public int Code { set; get; }

        /// <summary>
        /// 抛 <c>ResultException</c> 异常
        /// </summary>
        /// <param name="message">消息</param>
        public static void Throw(string message) => throw new ResultException(message);

        /// <summary>
        /// 抛 <c>ResultException</c> 异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="code">编码</param>
        public static void Throw(string message, int code) => throw new ResultException(message) { Code = code };


        /// <summary>
        /// 抛 <c>ResultException</c> 异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Throw(string message, Exception ex) => throw new ResultException(message,ex);


        /// <summary>
        /// 抛 <c>ResultException</c> 异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="code">编码</param>
        public static void Throw(string message, Exception ex,int code) => throw new ResultException(message, ex) { Code=code};

        /// <summary>
        /// 是否 抛 <c>ResultException</c> 异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="flag"></param>
        public static void ThrowIf(string message, bool flag)
        {

            if (flag)
            {
                Throw(message);
            }
        }

        /// <summary>
        /// 是否 抛 <c>ResultException</c> 异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="flag"></param>

        public static void ThrowIf(string message, int code, bool flag)
        {

            if (flag)
            {
                Throw(message, code);
            }
        }

        /// <summary>
        /// 是否 抛 <c>ResultException</c> 异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="flag"></param>
        public static void ThrowIf(string message, Exception ex, bool flag)
        {

            if (flag)
            {
                Throw(message, ex);
            }
        }

        /// <summary>
        /// 是否 抛 <c>ResultException</c> 异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="code"></param>
        /// <param name="flag"></param>
        public static void ThrowIf(string message, Exception ex, int code, bool flag)
        {

            if (flag)
            {
                Throw(message, ex,code);
            }
        }

    }
}
