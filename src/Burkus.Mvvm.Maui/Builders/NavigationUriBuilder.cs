using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace Burkus.Mvvm.Maui;

/// <summary>
/// Builder class for creation of URIs to be used with <see cref="INavigationService.Navigate(string)"/> method.
/// Defaults to using <see cref="UriKind.Relative"/> navigation.
/// </summary>
public class NavigationUriBuilder
{
    #region Fields

    private UriKind uriKind = UriKind.Relative;

    private List<(Type, NavigationParameters)> instructions = new List<(Type, NavigationParameters)>();

    #endregion Fields

    /// <summary>
    /// Changes the URI to use <see cref="UriKind.Absolute"/> navigation.
    /// </summary>
    /// <returns>This navigation builder</returns>
    public NavigationUriBuilder UseAbsoluteNavigation()
    {
        uriKind = UriKind.Absolute;
        return this;
    }

    /// <summary>
    /// Adds a page navigation segment.
    /// </summary>
    /// <typeparam name="T">Page to navigation to</typeparam>
    /// <returns>This navigation builder</returns>
    public NavigationUriBuilder AddSegment<T>()
        where T : Page
    {
        instructions.Add((typeof(T), new NavigationParameters()));

        return this;
    }

    /// <summary>
    /// Adds a page navigation segment with query parameters.
    /// </summary>
    /// <typeparam name="T">Page to navigation to</typeparam>
    /// <param name="navigationParameters">Navigation parameters to pass as query parameters</param>
    /// <returns>This navigation builder</returns>
    public NavigationUriBuilder AddSegment<T>(NavigationParameters navigationParameters)
        where T : Page
    {
        instructions.Add((typeof(T), navigationParameters));

        return this;
    }

    /// <summary>
    /// Adds a "go back" navigation segment.
    /// </summary>
    /// <returns>This navigation builder</returns>
    public NavigationUriBuilder AddGoBackSegment()
    {
        instructions.Add((typeof(GoBackUriSegment), new NavigationParameters()));

        return this;
    }

    /// <summary>
    /// Adds a "go back" navigation segment with query parameters.
    /// </summary>
    /// <param name="navigationParameters">Navigation parameters to pass as query parameters</param>
    /// <returns>This navigation builder</returns>
    public NavigationUriBuilder AddGoBackSegment(NavigationParameters navigationParameters)
    {
        instructions.Add((typeof(GoBackUriSegment), navigationParameters));

        return this;
    }

    /// <summary>
    /// Builds the URI to be used with <see cref="INavigationService.Navigate(string)"/>.
    /// </summary>
    /// <returns>URI navigation string</returns>
    public string Build()
    {
        var stringBuilder = new StringBuilder();

        if (uriKind == UriKind.Absolute)
        {
            stringBuilder.Append(Constants.UriSeparator);
        }

        foreach (var instruction in instructions)
        {
            if (instruction.Item1 == typeof(GoBackUriSegment))
            {
                // handle go back special case
                stringBuilder.Append(Constants.GoBackUriSegment);
            }
            else
            {
                stringBuilder.Append(instruction.Item1.Name);
            }

            if (instruction.Item2.Any())
            {
                stringBuilder.Append(instruction.Item2.ToQueryString());
            }

            stringBuilder.Append(Constants.UriSeparator);
        }

        return stringBuilder.ToString();
    }

    public override string ToString()
    {
        return Build();
    }
}