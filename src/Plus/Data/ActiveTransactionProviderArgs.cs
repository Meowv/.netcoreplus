using System.Collections.Generic;

namespace Plus.Data
{
    /// <summary>
    /// ActiveTransactionProviderArgs
    /// </summary>
    public class ActiveTransactionProviderArgs : Dictionary<string, object>
    {
        public static ActiveTransactionProviderArgs Empty { get; } = new ActiveTransactionProviderArgs();
    }
}