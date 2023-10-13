namespace Burkus.Mvvm.Maui;

public class ResolveBindingContext<T> : IMarkupExtension<T>
    where T : class
{
    public object ProvideValue(IServiceProvider serviceProvider)
    {
        return ServiceResolver.Resolve<T>();
    }

    T IMarkupExtension<T>.ProvideValue(IServiceProvider serviceProvider)
    {
        return ServiceResolver.Resolve<T>();
    }
}