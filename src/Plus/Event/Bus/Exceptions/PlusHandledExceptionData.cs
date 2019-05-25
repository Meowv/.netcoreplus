using System;

namespace Plus.Event.Bus.Exceptions
{
    public class PlusHandledExceptionData : ExceptionData
    {
        public PlusHandledExceptionData(Exception exception) : base(exception)
        {
        }
    }
}