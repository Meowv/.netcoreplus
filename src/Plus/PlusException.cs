using System;
using System.Runtime.Serialization;

namespace Plus
{
    /// <summary>
    /// 异常基类
    /// </summary>
    public class PlusException : Exception
    {
        /// <summary>
        /// 创建一个新的 <see cref="PlusException"/> 对象
        /// </summary>
        public PlusException()
        {

        }

        /// <summary>
        /// 创建一个新的 <see cref="PlusException"/> 对象
        /// </summary>
        public PlusException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// 创建一个新的 <see cref="PlusException"/> 对象
        /// </summary>
        /// <param name="message">异常消息</param>
        public PlusException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 创建一个新的 <see cref="PlusException"/> 对象
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public PlusException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}