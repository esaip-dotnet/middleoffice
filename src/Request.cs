using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MiddleOffice
{
    public class Request
    {
        [BsonId]
        public string id { get; set; }
        
        public List<Label> summary { get; set; }

        //[BsonRepresentation(BsonType.String)]
        [BsonSerializer(typeof(PayloadSerializer))]
        public object payload { get; set; }
        
        public string category { get; set; }
        
        public List<Answer> answers { get; set; }
        
        public Vote vote { get; set; }
    }
}
