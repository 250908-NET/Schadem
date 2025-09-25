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

// Products Endpoints ---------------------------------------------------------
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

app.MapPost("/products", async (IProductService service, Product product) =>
{
    await service.CreateAsync(product);
    return Results.Created($"/products/{product.ProductId}", product);
});

app.MapDelete("/products/{id}", async (IProductService service, int id) =>
{
    var product = await service.GetByIdAsync(id);
    if (product == null) return Results.NotFound();

    await service.DeleteAsync(product);
    return Results.NoContent(); // 204 No Content
});
//---------------------------------------------------------------

// Order Endpoints -------------------------------------------------
app.MapGet("/orders", async (IOrderService service) =>
{
    //Results.Ok(await service.GetAllAsync());

    var orders = await service.GetAllAsync();
    return Results.Ok(orders);
});

app.MapPost("/orders", async (IOrderService service, Order order) =>
{
    await service.CreateAsync(order);
    return Results.Created($"/orders/{order.OrderId}", order);
});

app.MapGet("/orders/{id}", async (IOrderService service, int id) =>
{
    var order = await service.GetByIdAsync(id);
    return order is not null ? Results.Ok(order) : Results.NotFound();
    //return student is not null ? Results.Ok(student) : Results.NotFound();
});

app.MapDelete("/orders/{id}", async (IOrderService service, int id) =>
{
    var order = await service.GetByIdAsync(id);
    if (order == null) return Results.NotFound();

    await service.DeleteAsync(order);
    return Results.NoContent(); // 204 No Content
});
//------------------------------------------------------------------

// Customer Endpoints --------------------------------------------

app.MapGet("/customers", async (ICustomerService service) =>
{
    //Results.Ok(await service.GetAllAsync());

    var customers = await service.GetAllAsync();
    return Results.Ok(customers);
});

app.MapPost("/customers", async (ICustomerService service, Customer customer) =>
{
    await service.CreateAsync(customer);
    return Results.Created($"/customers/{customer.Id}", customer);

});

app.MapGet("/customers/{id}", async (ICustomerService service, int id) =>
{
   var customer = await service.GetByIdAsync(id);
   return customer is not null ? Results.Ok(customer) : Results.NotFound();
   //return student is not null ? Results.Ok(student) : Results.NotFound();
});

// app.MapDelete("/customers/{id}", async (IOrderService service, int id) =>
// {
//     var order = await service.GetByIdAsync(id);
//     if (order == null) return Results.NotFound();

//     await service.DeleteAsync(order);
//     return Results.NoContent(); // 204 No Content
// });
//---------------------------------------------------------------

app.Run();


