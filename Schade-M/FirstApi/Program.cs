using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Collections.Generic;
using System.Text;





var builder = WebApplication.CreateBuilder(args);


/*
multi-line comment

still a comment
*/

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//builder.Services.AddOpenApi(); ---------------

var app = builder.Build();

var forecasts = new List<WeatherForecast>();

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

// 4: Date and Time

// ## Challenge 6: Temperature Converter
// **Goal**: Practice calculations and different data formats
// - Create `/temp/celsius-to-fahrenheit/{temp}` 
// - Add `/temp/fahrenheit-to-celsius/{temp}`
// - Create `/temp/kelvin-to-celsius/{temp}` and reverse
// - Add `/temp/compare/{temp1}/{unit1}/{temp2}/{unit2}` - compares temperatures

//Cel to fahr//████████████████████████████████████████████████████████████████████████████████████████████████
app.MapGet("/temp/celsius-to-fahrenheit/{temp}", (double temp) =>
{
    double fahrenheit = (temp * 9 / 5) + 32;
    return Results.Json(new { celsius = temp, fahrenheit });
});

//fahr to cel//████████████████████████████████████████████████████████████████████████████████████████████████
app.MapGet("/temp/fahrenheit-to-celsius/{temp}", (double temp) =>
{
    double celsius = (temp - 32) * 5 / 9;
    return Results.Json(new { fahrenheit = temp, celsius });
});

//Kel to Cel//████████████████████████████████████████████████████████████████████████████████████████████████
app.MapGet("/temp/kelvin-to-celsius/{temp}", (double temp) =>
{
    double celsius = temp - 273.15;
    return Results.Json(new { kelvin = temp, celsius });
});

//Cels to Kel//████████████████████████████████████████████████████████████████████████████████████████████████
app.MapGet("/temp/celsius-to-kelvin/{temp}", (double temp) =>
{
    double kelvin = temp + 273.15;
    return Results.Json(new { celsius = temp, kelvin });
});

//Compare Temps//████████████████████████████████████████████████████████████████████████████████████████████████
app.MapGet("/temp/compare/{temp1}/{unit1}/{temp2}/{unit2}", (double temp1, string unit1, double temp2, string unit2) =>
{
    // Helper to convert any unit to Celsius
    double ToCelsius(double temp, string unit) => unit.ToLower() switch
    {
        "c" or "celsius" => temp,
        "f" or "fahrenheit" => (temp - 32) * 5 / 9,
        "k" or "kelvin" => temp - 273.15,
        _ => throw new ArgumentException($"Unknown unit: {unit}")
    };

    try
    {
        double t1 = ToCelsius(temp1, unit1);
        double t2 = ToCelsius(temp2, unit2);

        string comparison = t1 == t2 ? "equal" :
                            t1 > t2 ? "temp1 is higher" :
                                      "temp2 is higher";

        return Results.Json(new
        {
            temp1, unit1,
            temp2, unit2,
            comparison
        });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});
//████████████████████████████████████████████████████████████████████████████████████████████████

//
// ## Challenge 7: Password Generator
// **Goal**: Work with random generation and string building
// - Create `/password/simple/{length}` - generates random letters/numbers
// - Add `/password/complex/{length}` - includes special characters
// - Create `/password/memorable/{words}` - generates passphrase with N words
// - Add `/password/strength/{password}` - rates password strength


var random = new Random();

app.MapGet("/password/simple/{length:int}", (int length) =>
{
    const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    var sb = new StringBuilder();
    for (int i = 0; i < length; i++)
        sb.Append(chars[random.Next(chars.Length)]);

    return Results.Json(new { password = sb.ToString() });
});


// Complex Password -----------------------------------------
app.MapGet("/password/complex/{length:int}", (int length) =>
{
    const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+[]{};:,.<>?";
    var sb = new StringBuilder();
    for (int i = 0; i < length; i++)
        sb.Append(chars[random.Next(chars.Length)]);

    return Results.Json(new { password = sb.ToString() });
});


// Memorable Password -----------------------------------------------
string[] wordList = { "apple", "tree", "river", "cloud", "stone", "light", "sky", "storm", "wind", "fire", "code", "cat", "dog", "house" };

app.MapGet("/password/memorable/{words:int}", (int words) =>
{
    var selected = new List<string>();
    for (int i = 0; i < words; i++)
        selected.Add(wordList[random.Next(wordList.Length)]);

    string passphrase = string.Join("-", selected);
    return Results.Json(new { passphrase });
});

//Strength Checker //////////////////////////////////////////////////////////////////////
app.MapGet("/password/strength/{password}", (string password) =>
{
    int score = 0;

    if (password.Length >= 8) score++;
    if (password.Length >= 12) score++;
    if (System.Text.RegularExpressions.Regex.IsMatch(password, "[A-Z]")) score++;
    if (System.Text.RegularExpressions.Regex.IsMatch(password, "[a-z]")) score++;
    if (System.Text.RegularExpressions.Regex.IsMatch(password, "[0-9]")) score++;
    if (System.Text.RegularExpressions.Regex.IsMatch(password, "[!@#$%^&*()\\-_=+\\[\\]{};:,.<>?]")) score++;

    string strength;
    if (score <= 1)
        strength = "Very Weak";
    else if (score == 2)
        strength = "Weak";
    else if (score == 3)
        strength = "Medium";
    else if (score == 4)
        strength = "Strong";
    else
        strength = "Very Strong";

    return Results.Json(new { password, strength, score });
});

// ## Challenge 9: Unit Converter
// **Goal**: Work with different measurement systems
// - Create `/convert/length/{value}/{fromUnit}/{toUnit}` (meters, feet, inches)
// - Add `/convert/weight/{value}/{fromUnit}/{toUnit}` (kg, lbs, ounces)
// - Create `/convert/volume/{value}/{fromUnit}/{toUnit}` (liters, gallons, cups)
// - Add `/convert/list-units/{type}` - returns available units for each type


var lengthUnits = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
{
    { "meters", 1.0 },
    { "feet", 0.3048 },
    { "inches", 0.0254 }
};


//Convert Length///////////////////////////////////////////////////////////////////////////////////////////////////
app.MapGet("/convert/length/{value}/{fromUnit}/{toUnit}", (double value, string fromUnit, string toUnit) =>
{
    if (!lengthUnits.ContainsKey(fromUnit) || !lengthUnits.ContainsKey(toUnit))
        return Results.BadRequest("Invalid length unit. Available: meters, feet, inches");

    double meters = value * lengthUnits[fromUnit];
    double converted = meters / lengthUnits[toUnit];

    return Results.Json(new { value, fromUnit, toUnit, result = converted });
});


//Weight Units//////////////////////////////////////////////////////////////////////////////////
var weightUnits = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
{
    { "kg", 1.0 },
    { "lbs", 0.45359237 },
    { "ounces", 0.0283495 }
};


//Convert Weight//////////////////////////////////////////////////////////////////////////////////////////////////
app.MapGet("/convert/weight/{value}/{fromUnit}/{toUnit}", (double value, string fromUnit, string toUnit) =>
{
    if (!weightUnits.ContainsKey(fromUnit) || !weightUnits.ContainsKey(toUnit))
        return Results.BadRequest("Invalid weight unit. Available: kg, lbs, ounces");

    double kg = value * weightUnits[fromUnit];
    double converted = kg / weightUnits[toUnit];

    return Results.Json(new { value, fromUnit, toUnit, result = converted });
});

//Volume Units///////////////////////////////////////////////////////////////////////////////
var volumeUnits = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
{
    { "liters", 1.0 },
    { "gallons", 3.78541 },
    { "cups", 0.24 }
};

//Convert Volume///////////////////////////////////////////////////////////////////////////////////
app.MapGet("/convert/volume/{value}/{fromUnit}/{toUnit}", (double value, string fromUnit, string toUnit) =>
{
    if (!volumeUnits.ContainsKey(fromUnit) || !volumeUnits.ContainsKey(toUnit))
        return Results.BadRequest("Invalid volume unit. Available: liters, gallons, cups");

    double liters = value * volumeUnits[fromUnit];
    double converted = liters / volumeUnits[toUnit];

    return Results.Json(new { value, fromUnit, toUnit, result = converted });
});

//Lists Units//////////////////////////////////////////////////////////////////////////////////////
app.MapGet("/convert/list-units/{type}", (string type) =>
{
    return Results.Json(type.ToLower() switch
    {
        "length" => lengthUnits.Keys,
        "weight" => weightUnits.Keys,
        "volume" => volumeUnits.Keys,
        _ => new string[] { }
    });
});
////////////////////////////////////////////////////////////////////////////////////////////////////

//10: Weather///////////////////////////////////////////////////////////////////////////////////////
// ## Challenge 10: Weather History
// **Goal**: Add persistence and CRUD operations
// - Create a simple in-memory list to store weather forecasts
// - Add POST endpoint to save a weather forecast
// - Modify GET to return saved forecasts instead of random ones
// - Add DELETE endpoint to remove forecasts by date


// GET all forecasts______________________________________________________________
app.MapGet("/weather", () =>
{
    return Results.Json(forecasts);
});

//Post new forcast__________________________________________________________________
app.MapPost("/weather", (string date, string temperatureC, string summary) =>
{
    if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, 
                                DateTimeStyles.None, out var forecastDate))
    {
        return Results.BadRequest("Invalid date format. Use yyyy-MM-dd.");
    }

    if (!int.TryParse(temperatureC, NumberStyles.Integer, CultureInfo.InvariantCulture, out int tempC))
    {
        return Results.BadRequest("Invalid temperature. Must be an integer.");
    }

    
    if (forecasts.Exists(f => f.Date == forecastDate))
        return Results.Conflict("Forecast for this date already exists.");

    var forecast = new WeatherForecast
    {
        Date = forecastDate,
        TemperatureC = tempC,
        Summary = summary
    };

    forecasts.Add(forecast);
    return Results.Json(forecast);
});

//Delete_______________________________________________________________________________
app.MapDelete("/weather", (string date) =>
{
    if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out var forecastDate))
    {
        return Results.BadRequest("Invalid date format. Use yyyy-MM-dd.");
    }

    var removed = forecasts.RemoveAll(f => f.Date == forecastDate);
    if (removed == 0)
        return Results.NotFound("No forecast found for this date.");

    return Results.Ok($"Removed {removed} forecast(s) for {date}");
});

// ## Challenge 11: Simple Games
// **Goal**: Combine multiple concepts in mini-games
// - Create `/game/guess-number` (POST) - number guessing game with session
// - Add `/game/rock-paper-scissors/{choice}` - play against computer
// - Create `/game/dice/{sides}/{count}` - roll N dice with X sides
// - Add `/game/coin-flip/{count}` - flip coins and return results

//Rock Paper Scissors_________________________________________________________________
app.MapGet("/game/rock-paper-scissors/{choice}", (string choice) =>
{
    var options = new[] { "rock", "paper", "scissors" };
    var random = new Random();
    string computer = options[random.Next(options.Length)];

    string result = choice.ToLower() == computer ? "Tie" :
                    (choice.ToLower(), computer) switch
                    {
                        ("rock", "scissors") => "You win",
                        ("scissors", "paper") => "You win",
                        ("paper", "rock") => "You win",
                        _ => "Computer wins"
                    };

    return Results.Json(new { player = choice, computer, result });
});

// Dice Roll________________________________________________________________________
app.MapGet("/game/dice/{sides:int}/{count:int}", (int sides, int count) =>
{
    var random = new Random();
    var rolls = new int[count];
    for (int i = 0; i < count; i++)
        rolls[i] = random.Next(1, sides + 1);

    return Results.Json(new { sides, count, rolls });
});


// Coin Flip________________________________________________________________________
app.MapGet("/game/coin-flip/{count:int}", (int count) =>
{
    var random = new Random();
    var flips = new string[count];
    for (int i = 0; i < count; i++)
        flips[i] = random.Next(2) == 0 ? "Heads" : "Tails";

    return Results.Json(new { count, flips });
});

//Guessing Game_____________________________________________________________________
app.MapGet("/game/guess-number", (string guess) =>
{
    // Parse guess
    if (!int.TryParse(guess, NumberStyles.Integer, CultureInfo.InvariantCulture, out int playerGuess))
    {
        return Results.BadRequest("Invalid guess. Please provide a number.");
    }

    
    var random = new Random();
    int target = random.Next(1, 10);

    string result = playerGuess == target ? "Correct!" :
                    playerGuess < target ? "Too low!" : "Too high!";

    return Results.Json(new { guess = playerGuess, target, result });
});
//___________________________________________________________________________________


app.MapGet("/testhello", () => "Hello from /testhello!");

//app.MapControllers();

app.Run();

public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }
}
