using MongoDB.Bson.Serialization;
using System;
using Newtonsoft.Json;

namespace MiddleOffice
{
    public class PayloadSerializer : IBsonSerializer
    {
        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            string desc = context.Reader.ReadString();
            return JsonConvert.DeserializeObject(desc);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            context.Writer.WriteString(JsonConvert.SerializeObject(value));
        }

        public Type ValueType => typeof(object);
    }
}