using System;

namespace Plus
{
    /// <summary>
    /// 该类用于模拟不做任何操作的一次性组件
    /// </summary>
    internal sealed class NullDisposable : IDisposable
    {
        public static NullDisposable Instance { get; } = new NullDisposable();


        private NullDisposable()
        {

        }

        public void Dispose()
        {

        }
    }
}