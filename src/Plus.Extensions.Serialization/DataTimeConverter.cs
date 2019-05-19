using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Plus.Extensions.Serialization
{
    public class DataTimeConverter : IsoDateTimeConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DateTime? dateTime = ReadJson(reader, objectType, existingValue, serializer) as DateTime?;
            if (dateTime.HasValue)
            {
                return dateTime.Value;
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateTime? dateTime = value as DateTime?;
            WriteJson(writer, dateTime ?? value, serializer);
        }

        public DataTimeConverter()
        {

        }
    }
}