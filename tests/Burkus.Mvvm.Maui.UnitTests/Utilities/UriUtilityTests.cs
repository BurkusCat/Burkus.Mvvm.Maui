using Burkus.Mvvm.Maui.UnitTests.TestHelpers;

namespace Burkus.Mvvm.Maui.UnitTests.Utilities;

public class UriUtilityTests
{
    [Theory]
    [InlineData("https://example.com/foo/bar?baz=qux", new[] { "https:", "example.com", "foo", "bar?baz=qux" })]
    [InlineData("/foo/bar", new[] { "foo", "bar" })]
    [InlineData("foo/bar/", new[] { "foo", "bar" })]
    [InlineData("foo//bar", new[] { "foo", "bar" })]
    public void GetUriSegments_WhenUriPassed_ReturnsSplitSegments(
        string uri,
        string[] expected)
    {
        // Arrange
        // Act
        var result = UriUtility.GetUriSegments(uri)
            .ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void GetUriSegments_WhenNullOrEmptyUri_ShowThrowBurkusMvvmException(
        string uri)
    {
        // Arrange
        // Act
        // Assert
        Assert.Throws<BurkusMvvmException>(() => UriUtility.GetUriSegments(uri));
    }

    [Theory]
    [InlineData("/")]
    [InlineData("//")]
    public void GetUriSegments_WhenNoSegmentsFound_ShowThrowBurkusMvvmException(
        string uri)
    {
        // Arrange
        // Act
        // Assert
        Assert.Throws<BurkusMvvmException>(() => UriUtility.GetUriSegments(uri));
    }

    [Theory]
    [InlineData("/", true)]
    [InlineData("/foo/bar", true)]
    [InlineData("foo", false)]
    [InlineData("foo/bar", false)]
    [InlineData("https://example.com/foo/bar", false)]
    public void IsUriAbsolute_WhenUriIsAbsolute_ShouldReturnTrue(
        string uri,
        bool expected)
    {
        // Arrange
        // Act
        var result = UriUtility.IsUriAbsolute(uri);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("MockYankeePage?foo=bar", typeof(MockYankeePage), "foo", "bar")]
    [InlineData("..", typeof(GoBackUriSegment), null, null)]
    [InlineData("MockZuluPage?person={\"name\":\"Ronan\",\"age\":28}", typeof(MockZuluPage), "person", "{\"name\":\"Ronan\",\"age\":28}")]
    public void ParseUriSegment_WhenValidSegment_ReturnsPageTypeAndQueryParameters(string segment, Type expectedPageType, string expectedKey, object expectedValue)
    {
        // Arrange
        // Act
        var result = UriUtility.ParseUriSegment(segment);

        // Assert
        Assert.Equal(expectedPageType, result.PageType);

        if (expectedKey != null)
        {
            Assert.True(result.QueryParameters.ContainsKey(expectedKey));
            Assert.Equal(expectedValue, result.QueryParameters[expectedKey].ToString());
        }
        else
        {
            Assert.Empty(result.QueryParameters);
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("invalid")]
    public void ParseUriSegment_Should_Throw_BurkusMvvmException_When_Segment_Is_Invalid(string segment)
    {
        // Arrange
        // Act
        // Assert
        Assert.Throws<BurkusMvvmException>(() => UriUtility.ParseUriSegment(segment));
    }
}
