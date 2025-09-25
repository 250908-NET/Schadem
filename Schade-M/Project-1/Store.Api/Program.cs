using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;
using Store.Services;
using Store.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string CS = File.ReadAllText("../connection_string.env");

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(CS));

//Add Repository Layer
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//Add Service Layer
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger(); 
builder.Host.UseSerilog();

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

app.MapGet("/products", async (IProductService service) =>
{
    //Results.Ok(await service.GetAllAsync());

    var products = await service.GetAllAsync();
    return Results.Ok(products);
});



app.MapGet("/products/{id}", async (IProductService service, int id) =>
{
    var product = await service.GetByIdAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
    //return student is not null ? Results.Ok(student) : Results.NotFound();
});

app.Run();


