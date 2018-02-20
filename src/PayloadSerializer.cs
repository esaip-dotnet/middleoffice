using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System;

namespace MiddleOffice
{
    public class PayloadSerializer : IBsonSerializer
    {

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args, object value) => JsonConvert.BsonDeserializationObject(context.Reader.ReadString());

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value) => context.Writer.WriteString(JsonConvert.SerializeObject(value));

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            throw new NotImplementedException();
        }

        public Type ValueType => typeof(object);
        
    }
}
