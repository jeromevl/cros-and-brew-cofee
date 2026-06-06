namespace BrewCofeeWithWeather.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTimeOffset Now { get; }
    }
}