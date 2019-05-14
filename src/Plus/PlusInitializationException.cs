using System;
using System.Runtime.Serialization;

namespace Plus
{
    /// <summary>
    /// 如果初始化过程中出现问题,将引发此异常
    /// </summary>
    [Serializable]
    public class PlusInitializationException : PlusException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PlusInitializationException()
        {

        }

        /// <summary>
        /// 构造函数用于序列化
        /// </summary>
        public PlusInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        public PlusInitializationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public PlusInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}