namespace Burkus.Mvvm.Maui;

internal static class LifecycleEventUtility
{
    #region INavigatingEvents

    internal static async Task TriggerOnNavigatingFrom(object? bindingContext, NavigationParameters navigationParameters)
    {
        var navigatingFromViewModel = bindingContext as INavigatingEvents;

        if (navigatingFromViewModel != null)
        {
            await navigatingFromViewModel.OnNavigatingFrom(navigationParameters);
        }
    }

    #endregion INavigatingEvents

    #region INavigatedEvents

    internal static async Task TriggerOnNavigatedFrom(object? bindingContext, NavigationParameters navigationParameters)
    {
        var navigatedFromViewModel = bindingContext as INavigatedEvents;

        if (navigatedFromViewModel != null)
        {
            await navigatedFromViewModel.OnNavigatedFrom(navigationParameters);
        }
    }

    internal static async Task TriggerOnNavigatedTo(object? bindingContext, NavigationParameters navigationParameters)
    {
        MapAttributeNavigationParameters(bindingContext, navigationParameters);

        // trigger the OnNavigatedToEvent
        var navigatedToViewModel = bindingContext as INavigatedEvents;

        if (navigatedToViewModel != null)
        {
            await navigatedToViewModel.OnNavigatedTo(navigationParameters);
        }
    }

    private static void MapAttributeNavigationParameters(object? bindingContext, NavigationParameters navigationParameters)
    {
        // map properties specified in attributes
        var type = bindingContext?.GetType();
        var attributes = type?.GetCustomAttributes(typeof(MapNavigationParameterAttribute), true);

        if (attributes == null)
        {
            return;
        }

        foreach (MapNavigationParameterAttribute attribute in attributes)
        {
            // throw an exception if the attribute is required but not found
            if (attribute.Required && !navigationParameters.ContainsKey(attribute.NavigationParameterKey))
            {
                throw new BurkusMvvmException($"The navigation parameter \"{attribute.PropertyName}\" is required but the key was not found.");
            }

            // get the property on the ViewModel
            var propertyInfo = type?.GetProperty(attribute.PropertyName);

            var matchingParameterValue = navigationParameters.GetUntypedValue(attribute.NavigationParameterKey);
            propertyInfo?.SetValue(bindingContext, matchingParameterValue);
        }
    }

    #endregion INavigatedEvents

    #region IPageVisibilityEvents

    internal static void SubscribeToPageVisibilityEvents(Page page)
    {
        page.Behaviors.Add(new PageVisibilityEventBehavior());
    }

    #endregion IPageVisibilityEvents
}