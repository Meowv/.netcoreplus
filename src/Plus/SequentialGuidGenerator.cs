using System;

namespace Plus
{
    public class SequentialGuidGenerator : IGuidGenerator
    {
        public static SequentialGuidGenerator Instance { get; } = new SequentialGuidGenerator();

        public Guid Create()
        {
            byte[] array = Guid.NewGuid().ToByteArray();
            DateTime dateTime = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = new TimeSpan(now.Ticks - dateTime.Ticks);
            TimeSpan timeOfDay = now.TimeOfDay;
            byte[] bytes = BitConverter.GetBytes(timeSpan.Days);
            byte[] bytes2 = BitConverter.GetBytes((long)(timeOfDay.TotalMilliseconds / 3.333333));
            Array.Reverse(bytes);
            Array.Reverse(bytes2);
            Array.Copy(bytes, bytes.Length - 2, array, array.Length - 6, 2);
            Array.Copy(bytes2, bytes2.Length - 4, array, array.Length - 4, 4);
            return new Guid(array);
        }
    }
}