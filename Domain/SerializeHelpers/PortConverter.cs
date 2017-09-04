using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Trizbort.Domain.SerializeHelpers
{
  public class PortConverter : JsonConverter
  {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      JObject jo = JObject.Load(reader);
      return jo.ToObject<Connection.VertexPort>(serializer);
    }

    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(Port);
    }

    public override bool CanWrite => false;
  }
}