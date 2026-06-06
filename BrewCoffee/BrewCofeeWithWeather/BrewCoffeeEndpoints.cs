using BrewCofeeWithWeather.Interfaces;
using BrewCofeeWithWeather.Models;

namespace BrewCofeeWithWeather
{
    public static class BrewCoffeeEndpoints
    {
        public static void MapCoffeeEndpoints(this WebApplication app)
        {
            app.MapGet("/brew-coffee", BrewCoffeeAsync);
        }

        public static async Task<IResult> BrewCoffeeAsync(ICounterService counterService, IWeatherService weatherService, IDateTimeProvider dateTimeProvider, IConfiguration config)
        {
            var now = dateTimeProvider.Now;
            if (now.Month == 4 && now.Day == 1)
                return Results.StatusCode(418);

            var count = counterService.Increment();
            if (count % 5 == 0)
                return Results.StatusCode(503);

            var temperature = await weatherService.GetCurrentTemperatureAsync(config["OpenWeather:City"]);
            var message = temperature > 30
                ? "Your refreshing iced coffee is ready"
                : "Your piping hot coffee is ready";

            return TypedResults.Ok(new BrewCoffeeResponse
            {
                Message = message,
                Prepared = now.ToString("yyyy-MM-ddTHH:mm:sszzz")
            });
        }
    }
}