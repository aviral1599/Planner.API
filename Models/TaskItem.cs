using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace PlannerApp.API.Models;

public class TaskItem
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    [BsonElement("ownerId")]
    [BsonRepresentation(BsonType.String)]
    public Guid OwnerId { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("subTasks")]
    public List<SubTask> SubTasks { get; set; } = new();

    [BsonElement("category")]
    public string Category { get; set; } = string.Empty;

    [BsonElement("status")]
    public string Status { get; set; } = string.Empty;

    [BsonElement("priority")]
    public string Priority { get; set; } = string.Empty;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("modifiedAt")]
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("dueDateTime")]
    public DateTime DueDateTime { get; set; } = DateTime.UtcNow;

    [BsonElement("isCompleted")]
    public bool IsCompleted { get; set; }
}

public class CreateTaskDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<SubTask> SubTasks { get; set; } = new();
    public string Category { get; set; } = string.Empty;
    [Required]
    public string Priority { get; set; } = string.Empty;
    public string? DueDate { get; set; }

    public string? DueTime { get; set; }
}
