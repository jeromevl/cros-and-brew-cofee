using BrewCofeeWithWeather.Interfaces;
using BrewCofeeWithWeather.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BrewCofeeWithWeather.Tests
{
    public class ApiTests
    {
        [Fact]
        public async Task BrewCoffeeAsync_AprilFirst_Returns418()
        {
            // Arrange
            var mockCounterService = new Mock<ICounterService>();
            var mockWeatherService = new Mock<IWeatherService>();

            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            mockDateTimeProvider.Setup(m => m.Now).Returns(new DateTimeOffset(2024, 4, 1, 0, 0, 0, TimeSpan.Zero));

            var mockConfig = new Mock<IConfiguration>();

            // Act
            var result = await BrewCoffeeEndpoints.BrewCoffeeAsync(mockCounterService.Object, mockWeatherService.Object, mockDateTimeProvider.Object, mockConfig.Object);

            // Assert
            var statusResult = result as StatusCodeHttpResult;
            Assert.Equal(418, statusResult.StatusCode);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        public async Task BrewCoffeeAsync_MultipleOfFive_Returns503(int count)
        {
            // Arrange
            var mockCounterService = new Mock<ICounterService>();
            mockCounterService.Setup(m => m.Increment()).Returns(count);

            var mockWeatherService = new Mock<IWeatherService>();

            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            mockDateTimeProvider.Setup(m => m.Now).Returns(DateTimeOffset.Now);

            var mockConfig = new Mock<IConfiguration>();

            // Act
            var result = await BrewCoffeeEndpoints.BrewCoffeeAsync(mockCounterService.Object, mockWeatherService.Object, mockDateTimeProvider.Object, mockConfig.Object);

            // Assert
            var statusResult = result as StatusCodeHttpResult;
            Assert.Equal(503, statusResult.StatusCode);
        }

        [Fact]
        public async Task BrewCoffeeAsync_TemperatureAbove30_ReturnsIcedCoffeeMessage()
        {
            // Arrange
            var mockCounterService = new Mock<ICounterService>();
            mockCounterService.Setup(m => m.Increment()).Returns(1);

            var mockWeatherService = new Mock<IWeatherService>();
            mockWeatherService.Setup(m => m.GetCurrentTemperatureAsync(It.IsAny<string>())).ReturnsAsync(31);

            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            mockDateTimeProvider.Setup(m => m.Now).Returns(DateTimeOffset.Now);

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(m => m["OpenWeather:City"]).Returns("Manila");

            // Act
            var result = await BrewCoffeeEndpoints.BrewCoffeeAsync(mockCounterService.Object, mockWeatherService.Object, mockDateTimeProvider.Object, mockConfig.Object);

            // Assert
            var okResult = result as Ok<BrewCoffeeResponse>;
            Assert.Equal("Your refreshing iced coffee is ready", okResult.Value.Message);
        }
    }
}