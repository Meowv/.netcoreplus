using System;
using System.Runtime.Serialization;

namespace Plus.Domain.Uow
{
    /// <summary>
    /// PlusDbConcurrencyException
    /// </summary>
    [Serializable]
    public class PlusDbConcurrencyException : PlusException
    {
        public PlusDbConcurrencyException()
        {
        }

        public PlusDbConcurrencyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        public PlusDbConcurrencyException(string message)
            : base(message)
        {
        }

        public PlusDbConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}