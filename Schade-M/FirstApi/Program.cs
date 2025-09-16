using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
/*
multi-line comment

still a comment
*/

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", null, "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

/* 
HTTP Request Types
- Get - Read
- Post - Create
- Patch - Update(partial)
- Put - Update/Replace
- Delete - Delete
- Head - a get request without a body
- Options - returns the supported methods on an endpoint
*/

/*
HTTP GET v1.1
localhost:5001/weatherforecast
headers
{
    "Accept": "application/json"
    "Response-Type": "application/json"
}

Header
Body
*/

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

app.MapGet("/", () => "Hello World!");

app.MapGet("/number", () => 
{
    return 42;
});

app.MapGet("/add/{a}/{b}", (int a, int b) => 
{
    return new
    {
        operation = "add",
        inputa = a,
        inputb = b,
        sum = a + b
    };
});

app.MapGet("/subtract/{a}/{b}", (int a, int b) =>
{
    return new
    {
        operation = "subtract",
        result = a - b

    };
});

app.MapGet("/multiply/{a}/{b}", (int a, int b) =>
{
    return new
    {
        operation = "multiply",
        inputa = a,
        inputb = b,
        result = a * b

    };
});

app.MapGet("/divide/{a:int}/{b:int}", (int a, int b) =>
{
    if (b == 0)
        return Results.BadRequest(new { error = "Division by zero is not allowed" });

    return Results.Json(new { operation = "divide", result = (double)a / b });
});

app.MapGet("/text/reverse/{text}", (string text) =>
{
    string reversed = "";

    for(int i = text.Length-1; i>=0; i--){
            reversed += text[i];
        }

    return new {
        reversed
        };
});

app.MapGet("/text/count/{text}", (string text) =>
{
    return text.Length;
});

app.MapGet("/text/uppercase/{text}", (string text) =>
{
    return text.ToUpper();
});

app.MapGet("/text/lowercase/{text}", (string text) =>
{
    return text.ToLower();
});

// 3: Loops
app.MapGet("/numbers/fizzbuzz/{count:int}", (int count) =>
{
    List<String> fizzbuzz = new List<String>();
    fizzbuzz.Add("0");
    for (int i = 1; i < count; i++)
    {
        if (i % 3 == 0 && i % 5 == 0) {fizzbuzz.Add("fizzbuzz");}
        else if (i % 3 == 0){fizzbuzz.Add("fizz");}
        else if (i % 5 == 0){fizzbuzz.Add("buzz");}
        else{ fizzbuzz.Add(i.ToString());}
    }
    return fizzbuzz;
});

app.MapGet("/prime/{count}", (int count) =>
{
    if (count == 0 || count == 1)
    {
        return Results.Ok(false);
    }

    if (count == 2)
    {
        return Results.Ok(true);
    }

    for (int i = 2; i < count; i++)
    {
        if (count % i == 0)
        {
            return Results.Ok(false);
        }
    }
    return Results.Ok(true);
});



app.MapGet("/numbers/factors/{count}", (int count) =>
{
    var factors = new List<int>();
    for (int i = 1; i <= count; i++)
    {
        if (count % i == 0)
            factors.Add(i);
    }

    return new {Factors = factors };
});

app.MapGet("/fibonacci/{count:int}", (int count) =>
{
    var fibonacciList = new List<long>();

    int a = 0;
    int b = 1;

    for (int i = 0; i < count; i++)
    {
        fibonacciList.Add(a);
        int x = a + b;
        a = b;
        b = x;
    }

    return Results.Ok(fibonacciList);
});

// app.MapGet("/fibonacci/{count}", (int count) =>
// {
//     List<int> fibonacciArray = new List<int>();
//     int prevNum = 0;
//     int sum = 0;
//     int temp = 0;
//     for (int i = 0; i < count; i++)
//     {
//         fibonacciArray.Add(i);
//         sum = i;
//         prevNum = i;
//         temp = sum;
//         sum = prevNum + sum;
//         prevNum = temp;
//         fibonacciArray.Add(sum);
        
//     }
//     return fibonacciArray;
// });

// 4: Date and Time






app.MapGet("/testhello", () => "Hello from /testhello!");

//app.MapControllers();

app.Run();

