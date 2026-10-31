namespace Burkus.Mvvm.Maui;

internal static class AutoSetPropertiesOptions
{
    internal static bool Enabled { get; set; }

    internal static Func<string, string>? PropertyNameMapper { get; set; }
}
