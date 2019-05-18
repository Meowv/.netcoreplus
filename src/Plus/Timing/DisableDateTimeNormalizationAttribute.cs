using System;

namespace Plus.Timing
{
    /// <summary>
    /// DisableDateTimeNormalizationAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class DisableDateTimeNormalizationAttribute : Attribute
    {
    }
}