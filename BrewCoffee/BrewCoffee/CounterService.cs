namespace BrewCoffee
{
    public class CounterService
    {
        private static int _callCount = 0;

        public int Increment()
        {
            return ++_callCount;
        }
    }
}