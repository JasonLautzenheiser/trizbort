using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Trizbort.Domain.Elements;

namespace Trizbort.Domain.SerializeHelpers
{
  public class ElementConverter : JsonConverter
  {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      JObject jo = JObject.Load(reader);

      JToken token;
      jo.TryGetValue("Owner", out token);

      return jo.ToObject<Room>(serializer);
    }

    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(Element);
    }

    public override bool CanWrite => false;
  }
}