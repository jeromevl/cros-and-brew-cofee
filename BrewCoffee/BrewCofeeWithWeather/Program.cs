using BrewCofeeWithWeather;
using BrewCofeeWithWeather.Concrete;
using BrewCofeeWithWeather.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient("openweather", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["OpenWeather:BaseUri"]);
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<ICounterService, CounterService>();
builder.Services.AddSingleton<IWeatherService, WeatherService>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapCoffeeEndpoints();

app.Run();
