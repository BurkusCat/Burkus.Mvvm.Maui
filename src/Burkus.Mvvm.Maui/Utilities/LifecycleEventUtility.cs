using System.Reflection;

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
        AutoMapNavigationParameters(bindingContext, navigationParameters);

        // trigger the OnNavigatedToEvent
        var navigatedToViewModel = bindingContext as INavigatedEvents;

        if (navigatedToViewModel != null)
        {
            await navigatedToViewModel.OnNavigatedTo(navigationParameters);
        }
    }

    private static void MapAttributeNavigationParameters(object? bindingContext, NavigationParameters navigationParameters)
    {
        if (bindingContext is null)
        {
            return;
        }

        // map properties specified in attributes
        var type = bindingContext.GetType();
        var attributes = type.GetCustomAttributes(typeof(MapNavigationParameterAttribute), true)
            as MapNavigationParameterAttribute[];

        if (attributes == null || attributes.Length == 0)
        {
            return;
        }

        foreach (MapNavigationParameterAttribute attribute in attributes)
        {
            if (navigationParameters.ContainsKey(attribute.NavigationParameterKey))
            {
                // get the property on the ViewModel
                var propertyInfo = type.GetProperty(attribute.PropertyName);

                // ensure property exists
                if (propertyInfo == null)
                {
                    throw new BurkusMvvmException($"The property \"{attribute.PropertyName}\" specified in the {nameof(MapNavigationParameterAttribute)} was not found on the ViewModel.");
                }

                // ensure property has a setter
                if (!propertyInfo.CanWrite)
                {
                    throw new BurkusMvvmException($"The property \"{attribute.PropertyName}\" specified in the {nameof(MapNavigationParameterAttribute)} does not have a valid setter.");
                }

                var matchingParameterValue = navigationParameters.GetUntypedValue(attribute.NavigationParameterKey);
                propertyInfo.SetValue(bindingContext, matchingParameterValue);
            }
            else if (attribute.Required)
            {
                // throw an exception if the attribute is required but not found
                throw new BurkusMvvmException($"The navigation parameter \"{attribute.PropertyName}\" is required but the key was not found.");
            }
        }
    }

    private static void AutoMapNavigationParameters(object? bindingContext, NavigationParameters navigationParameters)
    {
        if (bindingContext is null || !AutoSetPropertiesOptions.Enabled)
        {
            return;
        }

        var type = bindingContext.GetType();

        // keys already mapped by attributes take precedence
        var attributeKeys = type.GetCustomAttributes(typeof(MapNavigationParameterAttribute), true)
            .OfType<MapNavigationParameterAttribute>()
            .Select(attribute => attribute.NavigationParameterKey)
            .ToHashSet();

        foreach (var (key, value) in navigationParameters)
        {
            if (value is null || attributeKeys.Contains(key) || IsReservedNavigationParameter(key))
            {
                continue;
            }

            var propertyName = AutoSetPropertiesOptions.PropertyNameMapper?.Invoke(key) ?? key;
            var propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo == null || !propertyInfo.CanWrite)
            {
                continue;
            }

            propertyInfo.SetValue(bindingContext, ConvertToPropertyType(key, value, propertyInfo));
        }
    }

    private static bool IsReservedNavigationParameter(string key)
    {
        return key == ReservedNavigationParameters.UseModalNavigation
            || key == ReservedNavigationParameters.UseAnimatedNavigation
            || key == ReservedNavigationParameters.SelectTab;
    }

    private static object ConvertToPropertyType(string key, object value, PropertyInfo propertyInfo)
    {
        var targetType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

        if (targetType.IsInstanceOfType(value))
        {
            return value;
        }

        try
        {
            return targetType.IsEnum
                ? Enum.Parse(targetType, value.ToString()!)
                : Convert.ChangeType(value, targetType);
        }
        catch (Exception ex) when (ex is FormatException or InvalidCastException or OverflowException or ArgumentException)
        {
            throw new BurkusMvvmException($"The navigation parameter \"{key}\" could not be converted to the type {targetType} of the property \"{propertyInfo.Name}\".", ex);
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