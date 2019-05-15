using System;

namespace Plus.Runtime.Validation
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property)]
    public class DisableValidationAttribute : Attribute
    {

    }
}