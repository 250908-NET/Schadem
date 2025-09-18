using System.Collections.Concurrent;

public class TaskService
{
    private readonly ConcurrentDictionary<int, TaskItem> _tasks = new();
    private int _nextId = 1;

    public IEnumerable<TaskItem> GetAll(bool? isCompleted, Priority? priority, DateTime? dueBefore)
    {
        return _tasks.Values.Where(t =>
            (!isCompleted.HasValue || t.IsCompleted == isCompleted.Value) &&
            (!priority.HasValue || t.Priority == priority.Value) &&
            (!dueBefore.HasValue || (t.DueDate.HasValue && t.DueDate.Value < dueBefore.Value))
        );
    }

    public TaskItem? GetById(int id) => _tasks.GetValueOrDefault(id);

    public TaskItem Create(TaskItemDto dto)
    {
        var task = new TaskItem
        {
            Id = _nextId++,
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = dto.IsCompleted,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _tasks[task.Id] = task;
        return task;
    }

    public TaskItem? Update(int id, TaskItemDto dto)
    {
        if (!_tasks.ContainsKey(id)) return null;

        var existing = _tasks[id];
        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.IsCompleted = dto.IsCompleted;
        existing.Priority = dto.Priority;
        existing.DueDate = dto.DueDate;
        existing.UpdatedAt = DateTime.UtcNow;

        _tasks[id] = existing;
        return existing;
    }

    public bool Delete(int id) => _tasks.TryRemove(id, out _);
}

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new
            {
                success = false,
                errors = new[] { ex.Message },
                message = "An unexpected error occurred."
            });
        }
    }
}
