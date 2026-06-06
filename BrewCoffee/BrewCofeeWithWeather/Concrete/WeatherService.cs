using BrewCofeeWithWeather.Interfaces;
using System.Text.Json;

namespace BrewCofeeWithWeather.Concrete
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClient = httpClientFactory.CreateClient("openweather");
            _apiKey = config["OpenWeather:ApiKey"];
        }

        public async Task<double> GetCurrentTemperatureAsync(string city)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={_apiKey}";
            var response = await _httpClient.GetFromJsonAsync<JsonElement>(url);
            return response.GetProperty("main").GetProperty("temp").GetDouble();
        }
    }
}