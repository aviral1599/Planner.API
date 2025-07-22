using MongoDB.Bson.Serialization.Attributes;

namespace PlannerApp.API.Models
{
    public class RegisterDto
    {
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("password")]
        public string Password { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;
    }
}
