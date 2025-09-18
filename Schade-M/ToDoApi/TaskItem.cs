

public enum Priority
{
    Low,
    Medium,
    High,
    Critical
}

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = false;
    public Priority Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class TaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public Priority Priority { get; set; }
    public DateTime? DueDate { get; set; }
}

public static class TaskValidator
{
    public static (bool isValid, List<string> errors) Validate(TaskItemDto dto)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Title))
            errors.Add("Title is required.");
        else if (dto.Title.Length > 100)
            errors.Add("Title cannot exceed 100 characters.");

        if (!string.IsNullOrEmpty(dto.Description) && dto.Description.Length > 500)
            errors.Add("Description cannot exceed 500 characters.");

        return (errors.Count == 0, errors);
    }
}
