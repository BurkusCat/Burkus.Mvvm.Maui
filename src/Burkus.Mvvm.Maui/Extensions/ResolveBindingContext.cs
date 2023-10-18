namespace Burkus.Mvvm.Maui;

/// <summary>
/// This is a dummy MarkupExtension to workaround bug https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/13.
/// Please don't use this class.
/// </summary>
public class ResolveBindingContext : IMarkupExtension
{
    public object TypeArguments { get; set; }

    public object ProvideValue(IServiceProvider serviceProvider)
    {
        return null;
    }
}