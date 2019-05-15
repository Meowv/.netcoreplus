using System;

namespace Plus.Event.Bus
{
    /// <summary>
    /// 为所有事件定义接口。
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// 事件发生的时间
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        /// 触发事件的对象
        /// </summary>
        object EventSource { get; set; }
    }
}