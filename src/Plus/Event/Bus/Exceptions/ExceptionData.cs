using System;

namespace Plus.Event.Bus.Exceptions
{
    public class ExceptionData : EventData
    {
        public Exception Exception
        {
            get;
            private set;
        }

        public ExceptionData(Exception exception)
        {
            Exception = exception;
        }
    }
}