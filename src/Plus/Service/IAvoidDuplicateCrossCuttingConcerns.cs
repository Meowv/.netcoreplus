using System.Collections.Generic;

namespace Plus.Service
{
    /// <summary>
    /// IAvoidDuplicateCrossCuttingConcerns
    /// </summary>
    public interface IAvoidDuplicateCrossCuttingConcerns
    {
        List<string> AppliedCrossCuttingConcerns { get; }
    }
}