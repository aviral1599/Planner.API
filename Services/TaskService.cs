using MongoDB.Driver;
using PlannerApp.API.Models;
using Microsoft.Extensions.Options;
using PlannerApp.API.Repositories;

namespace PlannerApp.API.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepo;

    public TaskService(ITaskRepository taskRepo)
    {
        _taskRepo = taskRepo;
    }

    public async Task<TaskItem> CreateTaskAsync(CreateTaskDto dto, Guid ownerId)
    {
        var now = DateTime.UtcNow;
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            SubTasks = dto.SubTasks ?? new List<SubTask>(),
            Category = dto.Category,
            Priority = dto.Priority.ToString().ToLower(),
            OwnerId = ownerId,
            CreatedAt = now,
            ModifiedAt = now,
            IsCompleted = false
        };

        DateTime dueDateTime = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(dto.DueDate) && !string.IsNullOrWhiteSpace(dto.DueTime))
        {
            var dateTimeStr = $"{dto.DueDate}T{dto.DueTime}";
            if (DateTime.TryParse(dateTimeStr, out var dt))
                dueDateTime = dt;
        }

        task.DueDateTime = dueDateTime;

        await _taskRepo.AddAsync(task);
        return task;
    }

    public async Task<List<TaskItem>> GetTasksForUserAsync(Guid ownerId) =>
        await _taskRepo.GetByOwnerIdAsync(ownerId);

    public async Task<TaskItem> GetByIdAsync(Guid id, Guid ownerId) =>
        await _taskRepo.GetByIdAsync(id, ownerId);
}
