using DemoApp.Services;

namespace DemoApp.UnitTests.Services;

public class WeatherServiceTests
{
    [Fact]
    public void GetWeatherDescription_WhenCalledWithNoParameters_ReturnsRandomWeatherText()
    {
        // Arrange
        var service = new WeatherService();

        // Act
        var result = service.GetWeatherDescription();

        // Assert
        Assert.NotNull(result);
		Assert.NotEmpty(result);
	}
}