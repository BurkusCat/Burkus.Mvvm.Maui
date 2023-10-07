using DemoApp.Abstractions;
using DemoApp.Properties;

namespace DemoApp.Services;

public class WeatherService : IWeatherService
{
    private readonly Random random = new Random();

    public string GetWeatherDescription()
    {
        var randomChoice = random.Next(5);

        switch (randomChoice)
        {
            case 0:
                return Resources.WeatherDescription_Sunny;
            case 1:
                return Resources.WeatherDescription_Cloudy;
            case 2:
                return Resources.WeatherDescription_Rainy;
            case 3:
                return Resources.WeatherDescription_Stormy;
            case 4:
            default:
                return Resources.WeatherDescription_Lightning;
        }
    }
}
