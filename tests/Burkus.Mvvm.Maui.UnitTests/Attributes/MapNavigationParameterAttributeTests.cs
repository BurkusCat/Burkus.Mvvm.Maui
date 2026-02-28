namespace Burkus.Mvvm.Maui.UnitTests.Attributes;

public class MapNavigationParameterAttributeTests
{
    [Fact]
    public void Constructor_WithParameters_SetsPropertyValues()
    {
        // Arrange

        // Act
        var attribute = new MapNavigationParameterAttribute(
            "PropertyName",
            "navigation_key",
            true);

        // Assert
        Assert.Equal("PropertyName", attribute.PropertyName);
        Assert.Equal("navigation_key", attribute.NavigationParameterKey);
        Assert.True(attribute.Required);
    }

    [Fact]
    public void Constructor_WithSamePropertyNameAndNavigationKey_SetsPropertyValues()
    {
        // Arrange

        // Act
        var attribute = new MapNavigationParameterAttribute(
            "PropertyName",
            true);

        // Assert
        Assert.Equal("PropertyName", attribute.PropertyName);
        Assert.Equal("PropertyName", attribute.NavigationParameterKey);
        Assert.True(attribute.Required);
    }
}