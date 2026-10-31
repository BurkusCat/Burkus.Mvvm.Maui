namespace Burkus.Mvvm.Maui.UnitTests.Utilities;

public class AutoSetPropertiesOnNavigationTests : IDisposable
{
    public void Dispose()
    {
        AutoSetPropertiesOptions.Enabled = false;
        AutoSetPropertiesOptions.PropertyNameMapper = null;
    }

    [Fact]
    public async Task TriggerOnNavigatedTo_WhenNotEnabled_DoesNotSetProperties()
    {
        // Arrange
        AutoSetPropertiesOptions.Enabled = false;
        var viewModel = new AutoSetViewModel();
        var navigationParameters = new NavigationParameters { { "TestParam", "hello" } };

        // Act
        await LifecycleEventUtility.TriggerOnNavigatedTo(viewModel, navigationParameters);

        // Assert
        Assert.Null(viewModel.TestParam);
    }

    [Fact]
    public async Task TriggerOnNavigatedTo_WhenEnabledWithoutMapper_SetsPropertyWithSameName()
    {
        // Arrange
        AutoSetPropertiesOptions.Enabled = true;
        var viewModel = new AutoSetViewModel();
        var navigationParameters = new NavigationParameters { { "TestParam", "hello" } };

        // Act
        await LifecycleEventUtility.TriggerOnNavigatedTo(viewModel, navigationParameters);

        // Assert
        Assert.Equal("hello", viewModel.TestParam);
    }

    [Fact]
    public async Task TriggerOnNavigatedTo_WhenEnabledWithMapper_SetsPropertyNamedByMapper()
    {
        // Arrange
        AutoSetPropertiesOptions.Enabled = true;
        AutoSetPropertiesOptions.PropertyNameMapper = key =>
            string.Concat(key.Split('_').Select(part => char.ToUpperInvariant(part[0]) + part[1..]));
        var viewModel = new AutoSetViewModel();
        var navigationParameters = new NavigationParameters { { "test_param", "hello" } };

        // Act
        await LifecycleEventUtility.TriggerOnNavigatedTo(viewModel, navigationParameters);

        // Assert
        Assert.Equal("hello", viewModel.TestParam);
    }

    [Fact]
    public async Task TriggerOnNavigatedTo_WhenValueIsStringAndPropertyIsInt_ConvertsValue()
    {
        // Arrange
        AutoSetPropertiesOptions.Enabled = true;
        var viewModel = new AutoSetViewModel();
        var navigationParameters = new NavigationParameters { { "Count", "5" } };

        // Act
        await LifecycleEventUtility.TriggerOnNavigatedTo(viewModel, navigationParameters);

        // Assert
        Assert.Equal(5, viewModel.Count);
    }

    [Fact]
    public async Task TriggerOnNavigatedTo_WhenValueCannotBeConverted_ThrowsBurkusMvvmException()
    {
        // Arrange
        AutoSetPropertiesOptions.Enabled = true;
        var viewModel = new AutoSetViewModel();
        var navigationParameters = new NavigationParameters { { "Count", "not-a-number" } };

        // Act
        // Assert
        await Assert.ThrowsAsync<BurkusMvvmException>(() =>
            LifecycleEventUtility.TriggerOnNavigatedTo(viewModel, navigationParameters));
    }

    [Fact]
    public async Task TriggerOnNavigatedTo_WhenKeyHasNoMatchingProperty_IgnoresKey()
    {
        // Arrange
        AutoSetPropertiesOptions.Enabled = true;
        var viewModel = new AutoSetViewModel();
        var navigationParameters = new NavigationParameters { { "DoesNotExist", "value" } };

        // Act
        await LifecycleEventUtility.TriggerOnNavigatedTo(viewModel, navigationParameters);

        // Assert
        Assert.Null(viewModel.TestParam);
    }

    [Fact]
    public async Task TriggerOnNavigatedTo_WhenPropertyIsReadOnly_DoesNotSetProperty()
    {
        // Arrange
        AutoSetPropertiesOptions.Enabled = true;
        var viewModel = new AutoSetViewModel();
        var navigationParameters = new NavigationParameters { { "ReadOnlyValue", "changed" } };

        // Act
        await LifecycleEventUtility.TriggerOnNavigatedTo(viewModel, navigationParameters);

        // Assert
        Assert.Equal("fixed", viewModel.ReadOnlyValue);
    }

    [Fact]
    public async Task TriggerOnNavigatedTo_WhenKeyIsMappedByAttribute_AttributeMappingTakesPrecedence()
    {
        // Arrange
        AutoSetPropertiesOptions.Enabled = true;
        var viewModel = new AttributeMappedViewModel();
        var navigationParameters = new NavigationParameters { { "TestParam", "hello" } };

        // Act
        await LifecycleEventUtility.TriggerOnNavigatedTo(viewModel, navigationParameters);

        // Assert
        Assert.Equal("hello", viewModel.Title);
        Assert.Null(viewModel.TestParam);
    }

    [Fact]
    public async Task TriggerOnNavigatedTo_WhenReservedNavigationParameterPassed_DoesNotSetProperty()
    {
        // Arrange
        AutoSetPropertiesOptions.Enabled = true;
        var viewModel = new AutoSetViewModel();
        var navigationParameters = new NavigationParameters { { ReservedNavigationParameters.UseModalNavigation, true } };

        // Act
        await LifecycleEventUtility.TriggerOnNavigatedTo(viewModel, navigationParameters);

        // Assert
        Assert.False(viewModel.UseModalNavigation);
    }

    private class AutoSetViewModel
    {
        public string? TestParam { get; set; }

        public int Count { get; set; }

        public bool UseModalNavigation { get; set; }

        public string ReadOnlyValue { get; } = "fixed";
    }

    [MapNavigationParameter(nameof(Title), "TestParam")]
    private class AttributeMappedViewModel
    {
        public string? Title { get; set; }

        public string? TestParam { get; set; }
    }
}
