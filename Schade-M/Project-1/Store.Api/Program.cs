using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;
//using School.Services;
//using School.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string CS = File.ReadAllText("../connection_string.env");

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(CS));

// builder.Services.AddScoped<IStudentRepository, StudentRepository>();
// builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
// builder.Services.AddScoped<ICourseRepository, CourseRepository>();

// builder.Services.AddScoped<IStudentService, StudentService>();
// builder.Services.AddScoped<IInstructorService, InstructorService>();
// builder.Services.AddScoped<ICourseService, CourseService>();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/", () =>{
    return "Hello World";
});

app.Run();


