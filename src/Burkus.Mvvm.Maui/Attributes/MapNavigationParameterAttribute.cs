namespace Burkus.Mvvm.Maui;

/// <summary>
/// Used to indicate that a navigation parameter should automatically be mapped to a property
/// when the OnNavigatedTo event is firing.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class MapNavigationParameterAttribute : Attribute
{
    public string PropertyName { get; }

    public string NavigationParameterKey { get; }

    public bool Required { get; }

    /// <param name="propertyName">The property name on the viewmodel to map to</param>
    /// <param name="navigationParameterKey">The navigation parameter to map from</param>
    /// <param name="required">Throws an exception if required is set to false</param>
    public MapNavigationParameterAttribute(
        string propertyName,
        string navigationParameterKey,
        bool required = false)
    {
        PropertyName = propertyName;
        NavigationParameterKey = navigationParameterKey;
        Required = required;
    }
}