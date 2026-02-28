using System.Web;

namespace Burkus.Mvvm.Maui;

public class NavigationParameters : Dictionary<string, object>
{
    /// <summary>
    /// Gets or sets the value of the "UseAnimatedNavigation" parameter. Defaults to 'true'.
    /// </summary>
    public bool UseAnimatedNavigation
    {
        get => GetParameter(ReservedNavigationParameters.UseAnimatedNavigation, true);
        set => this[ReservedNavigationParameters.UseAnimatedNavigation] = value;
    }

    /// <summary>
    /// Gets or sets the value of the "UseModalNavigation" parameter. Defaults to 'false'.
    /// </summary>
    public bool UseModalNavigation
    {
        get => GetParameter(ReservedNavigationParameters.UseModalNavigation, false);
        set => this[ReservedNavigationParameters.UseModalNavigation] = value;
    }

    /// <summary>
    /// Gets or sets the value of the "SelectTab" parameter.
    /// </summary>
    public string SelectTab
    {
        get => GetParameter<string>(ReservedNavigationParameters.SelectTab, null);
        set => this[ReservedNavigationParameters.SelectTab] = value;
    }

    public NavigationParameters()
    {
    }

    internal NavigationParameters(IDictionary<string, object> dictionary) : base(dictionary)
    {
    }

    /// <summary>
    /// Gets or sets the value of a parameter.
    /// Defaults to the specified default value if the parameter is not set.
    /// </summary>
    /// <typeparam name="T">Type of parameter to return</typeparam>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <param name="defaultValue">The default value to return if the parameter is not set.</param>
    /// <returns>The value of the parameter or the default value.</returns>
    private T? GetParameter<T>(string parameterName, T defaultValue)
    {
        return ContainsKey(parameterName)
            ? GetValue<T>(parameterName)
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
    public T? GetValue<T>(string parameterName)
    {
        if (!ContainsKey(parameterName))
        {
            // return default for keys that don't exist
            return default;
        }

        var value = this[parameterName];

        return ConvertUntypedParameterToTypedParameter<T>(value);
    }

    internal object? GetUntypedValue(string parameterName)
    {
        if (!ContainsKey(parameterName))
        {
            // return default for keys that don't exist
            return default;
        }

        return this[parameterName];
    }

    internal static T? ConvertUntypedParameterToTypedParameter<T>(object value)
    {
        try
        {
            if (value == null)
            {
                return default;
            }

            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), value.ToString());
            }
            else
            {
                // get the non-nullable type of T
                var underlyingType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

                // convert the value to the non-nullable type
                var convertedValue = Convert.ChangeType(value, underlyingType);

                // cast the converted value to T
                return (T)convertedValue;
            }
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Cannot convert parameter value to type {typeof(T)}", ex);
        }
    }

    /// <summary>
    /// Merge this NavigationParameter object with one or more NavigationParameters.
    /// </summary>
    /// <remarks> This object is the primary NavigationParameter and will override values with duplicate keys present
    /// in the other NavigationParameter objects. The earlier in the array the NavigationParameter is, the more overriding
    /// precendence it has over the later NavigationParameter</remarks>
    /// <param name="navigationParameters">One or more NavigationParameters</param>
    /// <returns>A new NavigationParameter with merged keys and values</returns>
    public NavigationParameters MergeNavigationParameters(params NavigationParameters[] navigationParameters)
    {
        // add *this* NavigationParameter to the array of NavigationParameters
        var allNavigationParameters = new NavigationParameters[] { this }.Concat(navigationParameters).ToArray();

        var dictionary = allNavigationParameters
            .SelectMany(dict => dict)
            .GroupBy(kvp => kvp.Key)
            .ToDictionary(g => g.Key, g => g.First().Value);
        return new NavigationParameters(dictionary);
    }

    /// <summary>
    /// Formats all navigation parameter keys and values as a URI query string.
    /// </summary>
    /// <returns>A URI query string</returns>
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
