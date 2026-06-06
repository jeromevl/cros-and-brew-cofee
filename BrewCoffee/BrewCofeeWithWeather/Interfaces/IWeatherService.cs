namespace BrewCofeeWithWeather.Interfaces
{
    public interface IWeatherService
    {
        Task<double> GetCurrentTemperatureAsync(string city);
    }
}