using System;

namespace Plus.Event.Bus
{
    /// <summary>
    /// 实现 <see cref="IEventData"/> 接口
    /// </summary>
    [Serializable]
    public class EventData : IEventData
    {
        public DateTime EventTime { get; set; }

        public object EventSource { get; set; }

        protected EventData()
        {
            EventTime = DateTime.Now;
        }
    }
}