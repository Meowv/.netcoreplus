using System;

namespace Plus.Runtime.Validation
{
    /// <summary>
    /// DisableValidationAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property)]
    public class DisableValidationAttribute : Attribute
    {

    }
}