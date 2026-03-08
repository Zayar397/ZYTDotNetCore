using System.Text.Json.Serialization;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/snakes", () =>
{
    string folderPath = "Data\\Snakes.json";
    string snakeJson = File.ReadAllText(folderPath);
    var snakeObj = JsonConvert.DeserializeObject<SnakeResponseModel>(snakeJson);
    return Results.Ok(snakeObj.Tbl_Snake);
});
app.MapGet("/snakes/{id}", (int snakeId) =>
{
    string folderPath = "Data\\Snakes.json";
    string snakeJson = File.ReadAllText(folderPath);
    var snakeObj = JsonConvert.DeserializeObject<SnakeResponseModel>(snakeJson);
    var snakeObjById = snakeObj.Tbl_Snake.FirstOrDefault(x=>x.Id == snakeId);
    if (snakeObjById is null)
    {
        return Results.BadRequest("Data not found.");
    }
    return Results.Ok(snakeObjById);
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class SnakeResponseModel
{
    public SnakeModel[] Tbl_Snake { get; set; }
}

public class SnakeModel
{
    public int Id { get; set; }
    public string MMName { get; set; }
    public string EngName { get; set; }
    public string Detail { get; set; }
    public string IsPoison { get; set; }
    public string IsDanger { get; set; }
}

