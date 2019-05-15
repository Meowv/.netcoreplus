using Plus.Dependency;
using System;
using System.Threading.Tasks;

namespace Plus.Event.Bus.Handlers.Internals
{
    /// <summary>
    /// 这个事件处理程序是一个适配器，能够使用一个操作作为 <see cref="IAsyncEventHandler{TEventData}"/> 实现
    /// </summary>
    /// <typeparam name="TEventData">事件类型</typeparam>
    internal class AsyncActionEventHandler<TEventData> :
        IAsyncEventHandler<TEventData>,
        ITransientDependency
    {
        public Func<TEventData, Task> Action { get; private set; }

        /// <summary>
        /// 创建一个新的 <see cref="AsyncActionEventHandler{TEventData}"/> 实例
        /// </summary>
        /// <param name="handler"></param>
        public AsyncActionEventHandler(Func<TEventData, Task> handler)
        {
            Action = handler;
        }

        /// <summary>
        /// 处理事件的操作
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(TEventData eventData)
        {
            await Action(eventData);
        }
    }
}