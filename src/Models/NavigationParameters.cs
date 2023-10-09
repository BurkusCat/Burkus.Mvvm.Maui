using System.Web;

namespace Burkus.Mvvm.Maui;

public class NavigationParameters : Dictionary<string, object>
{

    /// <summary>
    /// Gets or sets the value of the "UseAnimatedNavigation" parameter. Defaults to 'false'.
    /// </summary>
    public bool UseAnimatedNavigation
    {
        get => GetBoolParameter(ReservedNavigationParameters.UseAnimatedNavigation, true);
        set => this[ReservedNavigationParameters.UseAnimatedNavigation] = value;
    }

    /// <summary>
    /// Gets or sets the value of the "UseModalNavigation" parameter. Defaults to 'false'.
    /// </summary>
    public bool UseModalNavigation
    {
        get => GetBoolParameter(ReservedNavigationParameters.UseModalNavigation, false);
        set => this[ReservedNavigationParameters.UseModalNavigation] = value;
    }

    /// <summary>
    /// Gets or sets the value of a boolean parameter.
    /// Defaults to the specified default value if the parameter is not set.
    /// </summary>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <param name="defaultValue">The default value to return if the parameter is not set.</param>
    /// <returns>The value of the parameter or the default value.</returns>
    private bool GetBoolParameter(string parameterName, bool defaultValue)
    {
        return ContainsKey(parameterName)
            ? GetValue<bool>(parameterName)
            : defaultValue;
    }

    /// <summary>
    /// Gets the value associated with the specified parameter name and converts it to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to which the parameter value should be converted.</typeparam>
    /// <param name="parameterName">The name of the parameter whose value is to be retrieved.</param>
    /// <returns>
    /// The value associated with the specified parameter name, converted to the specified type.
    /// If the parameter does not exist or if conversion fails, the default value for the type <typeparamref name="T"/> is returned.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the parameter value cannot be converted to the specified type.
    /// </exception>
    public T GetValue<T>(string parameterName)
    {
        if (!ContainsKey(parameterName))
        {
            // return default for keys that don't exist
            return default;
        }

        var value = this[parameterName];

        try
        {
            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), value.ToString());
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Cannot convert parameter value to type {typeof(T)}", ex);
        }
    }

    public string ToQueryString()
    {
        var keyValuePairs = new List<string>();

        foreach (var kvp in this)
        {
            var key = HttpUtility.UrlEncode(kvp.Key);

            // TODO: the .ToString() won't work for many parameter types
            var value = HttpUtility.UrlEncode(kvp.Value.ToString());
            keyValuePairs.Add($"{key}={value}");
        }

        if (keyValuePairs.Count > 0)
        {
            return "?" + string.Join("&", keyValuePairs);
        }
        else
        {
            return string.Empty;
        }
    }
}
