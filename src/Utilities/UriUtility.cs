using System.Text.Json;

namespace Burkus.Mvvm.Maui;

internal static class UriUtility
{
    internal static IEnumerable<string> GetUriSegments(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new BurkusMvvmException("URI cannot be null or empty");
        }

        // split the URI by '/' and remove empty segments
        var segments = uri.Split('/')
            .Where(s => !string.IsNullOrWhiteSpace(s));

        if (!segments.Any())
        {
            throw new BurkusMvvmException("No URI segments were found");
        }

        return segments;
    }

    internal static bool IsUriAbsolute(string uri)
    {
        // determine if the URI is using absolute navigation
        return uri.StartsWith("/", StringComparison.OrdinalIgnoreCase);
    }

    internal static (Type PageType, NavigationParameters QueryParameters) ParseUriSegment(string segment)
    {
        // Split the segment into the page name and query parameters
        var parts = segment.Split('?');

        // Get the page type from the page name
        var pageType = FindPageType(parts[0]);

        // The second part (if it exists) contains query parameters
        var queryString = parts.Length > 1 ? parts[1] : string.Empty;
        var queryParameters = ParseQueryParameters(queryString);

        return (pageType, queryParameters);
    }

    private static NavigationParameters ParseQueryParameters(string queryString)
    {
        var parameters = new NavigationParameters();

        if (!string.IsNullOrWhiteSpace(queryString))
        {
            var keyValuePairs = queryString.Split('&');

            foreach (var keyValuePair in keyValuePairs)
            {
                var kvpParts = keyValuePair.Split('=');

                if (kvpParts.Length == 2)
                {
                    var key = kvpParts[0];
                    var valueString = kvpParts[1];

                    object deserializedValue = DeserializeValue(valueString);
                    parameters[key] = deserializedValue;
                }
            }
        }

        return parameters;
    }

    private static object DeserializeValue(string valueString)
    {
        if (valueString.StartsWith("{") && valueString.EndsWith("}"))
        {
            try
            {
                // assuming it's JSON, you can use a JSON library to deserialize it
                return JsonSerializer.Deserialize<object>(valueString);
            }
            catch (Exception ex)
            {
                // handle deserialization errors as needed
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
        }

        // if it's not a recognized format, return it as a string
        return valueString;
    }

    private static Type FindPageType(string pageName)
    {
        if (pageName == Constants.GoBackUriSegment)
        {
            // handle special case of going back one page
            return typeof(GoBackUriSegment);
        }

        // search for page type in all assemblies
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            var pageType = assembly.GetTypes().FirstOrDefault(t => t.Name == pageName);
            if (pageType != null)
            {
                return pageType;
            }
        }

        // could not find the type
        throw new BurkusMvvmException($"Could not find a type in assemblies for page name: {pageName}");
    }
}
