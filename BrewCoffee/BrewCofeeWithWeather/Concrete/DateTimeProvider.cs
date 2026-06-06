using BrewCofeeWithWeather.Interfaces;

namespace BrewCofeeWithWeather.Concrete
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}