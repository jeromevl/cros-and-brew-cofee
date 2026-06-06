using BrewCofeeWithWeather.Interfaces;

namespace BrewCofeeWithWeather.Concrete
{
    public class CounterService : ICounterService
    {
        private static int _callCount = 0;

        public int Increment()
        {
            return ++_callCount;
        }
    }
}