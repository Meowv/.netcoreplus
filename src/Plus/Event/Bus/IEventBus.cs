using Plus.Event.Bus.Factories;
using Plus.Event.Bus.Handlers;
using System;
using System.Threading.Tasks;

namespace Plus.Event.Bus
{
    /// <summary>
    /// 定义事件接口
    /// </summary>
    public interface IEventBus
    {
        IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData;

        IDisposable Register<TEventData, THandler>() where TEventData : IEventData where THandler : IEventHandler<TEventData>, new();

        IDisposable Register(Type eventType, IEventHandler handler);

        IDisposable Register<TEventData>(IEventHandlerFactory handlerFactory) where TEventData : IEventData;

        IDisposable Register(Type eventType, IEventHandlerFactory handlerFactory);

        void Unregister<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData;

        void Unregister(Type eventType, IEventHandler handler);

        void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData;

        void Unregister(Type eventType, IEventHandlerFactory factory);

        void UnregisterAll<TEventData>() where TEventData : IEventData;

        void UnregisterAll(Type eventType);

        void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData;

        void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData;

        void Trigger(Type eventType, IEventData eventData);

        void Trigger(Type eventType, object eventSource, IEventData eventData);

        Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData;

        Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData;

        Task TriggerAsync(Type eventType, IEventData eventData);

        Task TriggerAsync(Type eventType, object eventSource, IEventData eventData);
    }
}