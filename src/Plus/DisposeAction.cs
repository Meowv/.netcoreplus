using System;
using System.Threading;

namespace Plus
{
    /// <summary>
    /// 这个类可用于在调用Dipose方法时提供一个操作
    /// </summary>
    public class DisposeAction : IDisposable
    {
        public static readonly DisposeAction Empty = new DisposeAction(null);

        private Action _action;

        /// <summary>
        /// 创建一个新的 <see cref="DisposeAction"/> 对象。
        /// </summary>
        /// <param name="action"></param>
        public DisposeAction(Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            Interlocked.Exchange(ref _action, null)?.Invoke();
        }
    }
}