using BrewCoffee;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<CounterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/brew-coffee", (CounterService counterService) =>
{
    var now = DateTimeOffset.Now;
    if (now.Month == 4 && now.Day == 1)
        return Results.StatusCode(418);

    var count = counterService.Increment();
    if (count % 5 == 0)
        return Results.StatusCode(503);

    return Results.Ok(new
    {
        message = "Your piping hot coffee is ready",
        prepared = now.ToString("yyyy-MM-ddTHH:mm:sszzz")
    });
});

app.Run();
