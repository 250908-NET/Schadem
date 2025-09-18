
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<TaskService>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();


app.UseMiddleware<ErrorHandlingMiddleware>();

// Endpoints
//Get ----------------------------------------------------------------------------------
app.MapGet("/api/tasks", (TaskService service, bool? isCompleted, Priority? priority, DateTime? dueBefore) =>
{
    var tasks = service.GetAll(isCompleted, priority, dueBefore);

    return Results.Ok(new { success = true, data = tasks });
});

//Get By ID----------------------------------------------------------------------------------
app.MapGet("/api/tasks/{id}", (TaskService service, int id) =>
{
    var task = service.GetById(id);

    return task is null
        ? Results.NotFound(new { success = false, errors = new[] { "Task not found" } })
        : Results.Ok(new { success = true, data = task });
});

//Post ----------------------------------------------------------------------------------
app.MapPost("/api/tasks", (TaskItemDto dto, TaskService service) =>
{
    var (isValid, errors) = TaskValidator.Validate(dto);

    if (!isValid)
        return Results.BadRequest(new { success = false, errors });

    var created = service.Create(dto);

    return Results.Created($"/api/tasks/{created.Id}", new { success = true, data = created });
});

//Put ----------------------------------------------------------------------------------
app.MapPut("/api/tasks/{id}", (TaskService service, int id, TaskItemDto dto) =>
{
    var (isValid, errors) = TaskValidator.Validate(dto);

    if (!isValid)
        return Results.BadRequest(new { success = false, errors });

    var updated = service.Update(id, dto);
    return updated is null
        ? Results.NotFound(new { success = false, errors = new[] { "Task not found" } })
        : Results.Ok(new { success = true, data = updated });
});

//Delete----------------------------------------------------------------------------------
app.MapDelete("/api/tasks/{id}", (TaskService service, int id) =>
{
    var deleted = service.Delete(id);

    return !deleted
        ? Results.NotFound(new { success = false, errors = new[] { "Task not found" } })
        : Results.Ok(new { success = true, message = "Task deleted" });


        
});

app.Run();










