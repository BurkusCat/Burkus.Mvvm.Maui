namespace Burkus.Mvvm.Maui.UnitTests.Utilities;

public class LifecycleEventUtilityTests
{
    [Fact]
    public async Task TriggerOnNavigatingFrom_WhenBindingContextIsNavigatingEvents_CallsOnNavigatingFrom()
    {
        // Arrange
        var mockNavigatingEvents = Substitute.For<INavigatingEvents>();
        var mockNavigationParameters = Substitute.For<NavigationParameters>();
        var navigationParameters = mockNavigationParameters;

        // Act
        await LifecycleEventUtility.TriggerOnNavigatingFrom(mockNavigatingEvents, navigationParameters);

        // Assert
        mockNavigatingEvents.Received().OnNavigatingFrom(navigationParameters);
    }

    [Fact]
    public async Task TriggerOnNavigatingFrom_WhenBindingContextIsNotNavigatingEvents_DoesNothing()
    {
        // Arrange
        var mockBindingContext = Substitute.For<object>();
        var mockNavigationParameters = Substitute.For<NavigationParameters>();
        var navigationParameters = mockNavigationParameters;

        // Act
        await LifecycleEventUtility.TriggerOnNavigatingFrom(mockBindingContext, navigationParameters);

        // Assert
        mockBindingContext.DidNotReceiveWithAnyArgs(); 
    }

    [Fact]
    public async Task TriggerOnNavigatedFrom_WhenBindingContextIsNavigatedEvents_CallsOnNavigatedFrom()
    {
        // Arrange
        var mockNavigatedEvents = Substitute.For<INavigatedEvents>();
        var mockNavigationParameters = Substitute.For<NavigationParameters>();
        var navigationParameters = mockNavigationParameters;

        // Act
        await LifecycleEventUtility.TriggerOnNavigatedFrom(mockNavigatedEvents, navigationParameters);

        // Assert
        mockNavigatedEvents.Received().OnNavigatedFrom(navigationParameters);
    }

    [Fact]
    public async Task TriggerOnNavigatedFrom_WhenBindingContextIsNotNavigatedEvents_DoesNothing()
    {
        // Arrange
        var mockBindingContext = Substitute.For<object>();
        var mockNavigationParameters = Substitute.For<NavigationParameters>();
        var navigationParameters = mockNavigationParameters;

        // Act
        await LifecycleEventUtility.TriggerOnNavigatedFrom(mockBindingContext, navigationParameters);

        // Assert
        mockBindingContext.DidNotReceiveWithAnyArgs();
    }

    [Fact]
    public async Task TriggerOnNavigatedTo_WhenBindingContextIsNavigatedEvents_CallsOnNavigatedTo()
    {
        // Arrange
        var mockNavigatedEvents = Substitute.For<INavigatedEvents>();
        var mockNavigationParameters = Substitute.For<NavigationParameters>();
        var navigationParameters = mockNavigationParameters;

        // Act
        await LifecycleEventUtility.TriggerOnNavigatedTo(mockNavigatedEvents, navigationParameters);

        // Assert
        mockNavigatedEvents.Received().OnNavigatedTo(navigationParameters);
    }

    [Fact]
    public async Task TriggerOnNavigatedTo_WhenBindingContextIsNotNavigatedEvents_DoesNothing()
    {
        // Arrange
        var mockBindingContext = Substitute.For<object>();
        var mockNavigationParameters = Substitute.For<NavigationParameters>();
        var navigationParameters = mockNavigationParameters;

        // Act
        await LifecycleEventUtility.TriggerOnNavigatedTo(mockBindingContext, navigationParameters);

        // Assert
        mockBindingContext.DidNotReceiveWithAnyArgs();
    }
}