using System.Collections.Generic;

namespace Plus.Services
{
    /// <summary>
    /// IAvoidDuplicateCrossCuttingConcerns
    /// </summary>
    public interface IAvoidDuplicateCrossCuttingConcerns
    {
        List<string> AppliedCrossCuttingConcerns { get; }
    }
}