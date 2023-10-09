using Burkus.Mvvm.Maui.UnitTests.TestHelpers;

namespace Burkus.Mvvm.Maui.UnitTests.Models;

public class NavigationUriBuilderTests
{
    [Fact]
    public void Build_WithAbsoluteNavigation_ShouldReturnCorrectUriString()
    {
        // Arrange
        var builder = new NavigationUriBuilder()
            .UseAbsoluteNavigation()
            .AddSegment<MockVictorPage>();

        // Act
        var uri = builder.Build();

        // Assert
        Assert.Equal("/MockVictorPage/", uri);
    }

    [Fact]
    public void Build_WithNavigateSegment_ShouldReturnCorrectUriString()
    {
        // Arrange
        var builder = new NavigationUriBuilder()
            .AddSegment<MockYankeePage>();

        // Act
        var uri = builder.Build();

        // Assert
        Assert.Equal("MockYankeePage/", uri);
    }

    [Fact]
    public void Build_WithNavigateSegmentAndParameters_ShouldReturnCorrectUriString()
    {
        // Arrange
        var builder = new NavigationUriBuilder()
            .AddSegment<MockZuluPage>(new NavigationParameters
            {
                { "color", "blue" },
            });

        // Act
        var uri = builder.Build();

        // Assert
        Assert.Equal("MockZuluPage?color=blue/", uri);
    }

    [Fact]
    public void Build_WithGoBackSegment_ShouldReturnCorrectUriString()
    {
        // Arrange
        var builder = new NavigationUriBuilder()
            .AddGoBackSegment();

        // Act
        var uri = builder.Build();

        // Assert
        Assert.Equal("../", uri);
    }

    [Fact]
    public void Build_WithGoBackSegmentAndParameters_ShouldReturnCorrectUriString()
    {
        // Arrange
        var builder = new NavigationUriBuilder()
            .AddGoBackSegment(new NavigationParameters
            {
                { "europe", 19628 },
            });

        // Act
        var uri = builder.Build();

        // Assert
        Assert.Equal("..?europe=19628/", uri);
    }

    [Fact]
    public void Build_WithMultipleSegments_ShouldReturnCorrectUriString()
    {
        // Arrange
        var parameters = new NavigationParameters();
        parameters.UseModalNavigation = true;

        var builder = new NavigationUriBuilder()
            .AddGoBackSegment()
            .AddGoBackSegment(parameters)
            .AddSegment<MockVictorPage>()
            .AddSegment<MockYankeePage>();

        // Act
        var uri = builder.Build();

        // Assert
        Assert.Equal("../..?UseModalNavigation=True/MockVictorPage/MockYankeePage/", uri);
    }
}