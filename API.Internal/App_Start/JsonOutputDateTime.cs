using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace API.Internal.App_Start
{
    internal class JsonOutputDateTime : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTime.Parse(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                writer.WriteValue(((DateTime)value).ToString("yyyy-MM-ddTHH:mm:ss", new System.Globalization.CultureInfo("en-US")));
            }
            catch
            {
                writer.WriteValue(((DateTime)value).ToString("o"));
            }
        }
    }
}