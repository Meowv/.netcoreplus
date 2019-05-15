using Castle.Core.Logging;
using Plus.Event.Bus.Factories;
using Plus.Event.Bus.Factories.Internals;
using Plus.Event.Bus.Handlers;
using Plus.Event.Bus.Handlers.Internals;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Plus.Event.Bus
{
    /// <summary>
    /// 以单例模式实现 EventBus
    /// </summary>
    public class EventBus : IEventBus
    {
        private class EventTypeWithEventHandlerFactories
        {
            public Type EventType { get; }

            public List<IEventHandlerFactory> EventHandlerFactories { get; }

            public EventTypeWithEventHandlerFactories(Type eventType, List<IEventHandlerFactory> eventHandlerFactories)
            {
                EventType = eventType;
                EventHandlerFactories = eventHandlerFactories;
            }
        }

        private readonly ConcurrentDictionary<Type, List<IEventHandlerFactory>> _handlerFactories;

        public static EventBus Default { get; } = new EventBus();

        public ILogger Logger { get; set; }

        public EventBus()
        {
            _handlerFactories = new ConcurrentDictionary<Type, List<IEventHandlerFactory>>();
            Logger = NullLogger.Instance;
        }

        public IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            return Register(typeof(TEventData), new ActionEventHandler<TEventData>(action));
        }

        public IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            return Register(typeof(TEventData), handler);
        }

        public IDisposable Register(Type eventType, IEventHandler handler)
        {
            return Register(eventType, new SingleInstanceHandlerFactory(handler));
        }

        public IDisposable Register<TEventData>(IEventHandlerFactory handlerFactory) where TEventData : IEventData
        {
            return Register(typeof(TEventData), handlerFactory);
        }

        public IDisposable Register(Type eventType, IEventHandlerFactory handlerFactory)
        {
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Add(handlerFactory));

            return new FactoryUnregistrar(this, eventType, handlerFactory);
        }

        public IDisposable Register<TEventData, THandler>()
            where TEventData : IEventData
            where THandler : IEventHandler, new()
        {
            return Register(typeof(TEventData), new TransientEventHandlerFactory<THandler>());
        }

        public IDisposable AsyncRegister<TEventData>(Func<TEventData, Task> action) where TEventData : IEventData
        {
            return Register(typeof(TEventData), new AsyncActionEventHandler<TEventData>(action));
        }

        public IDisposable AsyncRegister<TEventData>(IAsyncEventHandler<TEventData> handler) where TEventData : IEventData
        {
            return Register(typeof(TEventData), handler);
        }

        public void Unregister<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            GetOrCreateHandlerFactories(typeof(TEventData))
                .Locking(factories =>
                {
                    factories.RemoveAll(
                        factory =>
                        {
                            if (!(factory is SingleInstanceHandlerFactory singleInstanceFactory))
                            {
                                return false;
                            }

                            if (!(singleInstanceFactory.HandlerInstance is ActionEventHandler<TEventData> actionHandler))
                            {
                                return false;
                            }

                            return actionHandler.Action == action;
                        });
                });
        }

        public void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            Unregister(typeof(TEventData), handler);
        }

        public void Unregister(Type eventType, IEventHandler handler)
        {
            GetOrCreateHandlerFactories(eventType)
                .Locking(factories =>
                {
                    factories.RemoveAll(
                        factory =>
                            factory is SingleInstanceHandlerFactory &&
                            (factory as SingleInstanceHandlerFactory).HandlerInstance == handler
                        );
                });
        }

        public void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData
        {
            Unregister(typeof(TEventData), factory);
        }

        public void Unregister(Type eventType, IEventHandlerFactory factory)
        {
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Remove(factory));
        }

        public void AsyncUnregister<TEventData>(Func<TEventData, Task> action) where TEventData : IEventData
        {
            GetOrCreateHandlerFactories(typeof(TEventData))
                .Locking(factories =>
                {
                    factories.RemoveAll(
                        factory =>
                        {
                            var singleInstanceFactory = factory as SingleInstanceHandlerFactory;
                            if (singleInstanceFactory == null)
                            {
                                return false;
                            }

                            var actionHandler = singleInstanceFactory.HandlerInstance as AsyncActionEventHandler<TEventData>;
                            if (actionHandler == null)
                            {
                                return false;
                            }

                            return actionHandler.Action == action;
                        });
                });
        }

        public void AsyncUnregister<TEventData>(IAsyncEventHandler<TEventData> handler) where TEventData : IEventData
        {
            Unregister(typeof(TEventData), handler);
        }

        public void UnregisterAll<TEventData>() where TEventData : IEventData
        {
            UnregisterAll(typeof(TEventData));
        }

        public void UnregisterAll(Type eventType)
        {
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Clear());
        }

        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            Trigger((object)null, eventData);
        }

        public void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData
        {
            Trigger(typeof(TEventData), eventSource, eventData);
        }

        public void Trigger(Type eventType, IEventData eventData)
        {
            Trigger(eventType, null, eventData);
        }

        public void Trigger(Type eventType, object eventSource, IEventData eventData)
        {
            List<Exception> list = new List<Exception>();
            TriggerHandlingException(eventType, eventSource, eventData, list);
            if (list.Any())
            {
                if (list.Count == 1)
                {
                    list[0].ReThrow();
                }
                throw new AggregateException("More than one error has occurred while triggering the event: " + eventType, list);
            }
        }

        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            return TriggerAsync((object)null, eventData);
        }

        public Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData
        {
            ExecutionContext.SuppressFlow();
            Task result = Task.Factory.StartNew(delegate
            {
                try
                {
                    Trigger(eventSource, eventData);
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex.ToString(), ex);
                }
            });
            ExecutionContext.RestoreFlow();
            return result;
        }

        public Task TriggerAsync(Type eventType, IEventData eventData)
        {
            return TriggerAsync(eventType, null, eventData);
        }

        public Task TriggerAsync(Type eventType, object eventSource, IEventData eventData)
        {
            ExecutionContext.SuppressFlow();
            Task result = Task.Factory.StartNew(delegate
            {
                try
                {
                    Trigger(eventType, eventSource, eventData);
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex.ToString(), ex);
                }
            });
            ExecutionContext.RestoreFlow();
            return result;
        }

        private void TriggerHandlingException(Type eventType, object eventSource, IEventData eventData, List<Exception> exceptions)
        {
            eventData.EventSource = eventSource;
            foreach (EventTypeWithEventHandlerFactories handlerFactory in GetHandlerFactories(eventType))
            {
                foreach (IEventHandlerFactory eventHandlerFactory in handlerFactory.EventHandlerFactories)
                {
                    IEventHandler handler = eventHandlerFactory.GetHandler();
                    try
                    {
                        if (handler == null)
                        {
                            throw new Exception("Registered event handler for event type " + handlerFactory.EventType.Name + " does not implement IEventHandler<" + handlerFactory.EventType.Name + "> interface!");
                        }
                        Type type = typeof(IEventHandler<>).MakeGenericType(handlerFactory.EventType);
                        MethodInfo method = type.GetMethod("HandleEvent", new Type[1]
                        {
                            handlerFactory.EventType
                        });
                        method.Invoke(handler, new object[1]
                        {
                            eventData
                        });
                    }
                    catch (TargetInvocationException ex)
                    {
                        exceptions.Add(ex.InnerException);
                    }
                    catch (Exception item)
                    {
                        exceptions.Add(item);
                    }
                    finally
                    {
                        eventHandlerFactory.ReleaseHandler(handler);
                    }
                }
            }
            if (eventType.GetTypeInfo().IsGenericType && eventType.GetGenericArguments().Length == 1 && typeof(IEventDataWithInheritableGenericArgument).IsAssignableFrom(eventType))
            {
                Type type2 = eventType.GetGenericArguments()[0];
                Type baseType = type2.GetTypeInfo().BaseType;
                if (baseType != null)
                {
                    Type type3 = eventType.GetGenericTypeDefinition().MakeGenericType(baseType);
                    object[] constructorArgs = ((IEventDataWithInheritableGenericArgument)eventData).GetConstructorArgs();
                    IEventData eventData2 = (IEventData)Activator.CreateInstance(type3, constructorArgs);
                    eventData2.EventTime = eventData.EventTime;
                    Trigger(type3, eventData.EventSource, eventData2);
                }
            }
        }

        private IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType)
        {
            var list = new List<EventTypeWithEventHandlerFactories>();
            foreach (KeyValuePair<Type, List<IEventHandlerFactory>> item in from hf in _handlerFactories
                                                                            where ShouldTriggerEventForHandler(eventType, hf.Key)
                                                                            select hf)
            {
                list.Add(new EventTypeWithEventHandlerFactories(item.Key, item.Value));
            }
            return list.ToArray();
        }

        private static bool ShouldTriggerEventForHandler(Type eventType, Type handlerType)
        {
            if (handlerType == eventType)
            {
                return true;
            }
            if (handlerType.IsAssignableFrom(eventType))
            {
                return true;
            }
            return false;
        }

        private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType)
        {
            return _handlerFactories.GetOrAdd(eventType, (Type type) => new List<IEventHandlerFactory>());
        }
    }
}