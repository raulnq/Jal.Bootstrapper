using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jal.Bootstrapper.Serilog.Sinks.Splunk
{
    public class SpecificTypeConverter<T> : JsonConverter
    {
        private readonly Func<JObject, T> _read;

        public SpecificTypeConverter(Func<JObject, T> read = null)
        {
            _read = read;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanRead
        {
            get { return _read != null; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var j = JObject.Load(reader);

            return Read(j);
        }

        private T Read(JObject j)
        {
            if (_read != null)
                return _read(j);

            return default(T);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }
    }
}