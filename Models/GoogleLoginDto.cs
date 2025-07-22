using MongoDB.Bson.Serialization.Attributes;

namespace PlannerApp.API.Models
{
    public class GoogleLoginDto
    {
        [BsonElement("idToken")]
        public string IdToken { get; set; } = string.Empty;
    }
}
