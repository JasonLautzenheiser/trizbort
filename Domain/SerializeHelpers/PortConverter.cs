using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Misc;

namespace Trizbort.Domain.SerializeHelpers; 

public class PortConverter : JsonConverter {
  public override bool CanWrite => false;

  public override bool CanConvert(Type objectType) {
    return objectType == typeof(Port);
  }

  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
    var jo = JObject.Load(reader);
    return jo.ToObject<Connection.VertexPort>(serializer);
  }

  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
    throw new NotImplementedException();
  }
}