using Plus.Dependency;
using System;

namespace Plus.Event.Bus.Handlers.Internals
{
    /// <summary>
    /// 这个事件处理程序是一个适配器，能够使用一个操作作为 <see cref="IEventHandler{TEventData}"/> 实现
    /// </summary>
    /// <typeparam name="TEventData">事件类型</typeparam>
    internal class ActionEventHandler<TEventData> :
        IEventHandler<TEventData>,
        ITransientDependency
    {
        public Action<TEventData> Action { get; private set; }

        /// <summary>
        /// 创建一个新的 <see cref="ActionEventHandler{TEventData}"/> 实例
        /// </summary>
        /// <param name="handler"></param>
        public ActionEventHandler(Action<TEventData> handler)
        {
            Action = handler;
        }

        /// <summary>
        /// 处理事件的操作
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(TEventData eventData)
        {
            Action(eventData);
        }
    }
}