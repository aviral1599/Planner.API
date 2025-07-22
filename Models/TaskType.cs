using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PlannerApp.API.Models
{
    public class TaskType
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("label")]
        public string Label { get; set; } = string.Empty;
    }
}
