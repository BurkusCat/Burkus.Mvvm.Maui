namespace Burkus.Mvvm.Maui;

/// <summary>
/// Wire up the events for <see cref="Page.OnAppearing"/> and <see cref="Page.OnDisappearing"/> when
/// this behavior is added to a page. Events are unsubscribed when behavior detaches.
/// </summary>
internal class PageVisibilityEventBehavior : Behavior<Page>
{
    protected override void OnAttachedTo(Page bindable)
    {
        base.OnAttachedTo(bindable);
        RegisterEvents(bindable);
    }

    protected override void OnDetachingFrom(Page bindable)
    {
        UnregisterEvents(bindable);
        base.OnDetachingFrom(bindable);
    }

    void RegisterEvents(Page bindable)
    {
        UnregisterEvents(bindable);

        bindable.Appearing += Page_Appearing;
        bindable.Disappearing += Page_Disappearing;
    }

    void UnregisterEvents(Page bindable)
    {
        bindable.Appearing -= Page_Appearing;
        bindable.Disappearing -= Page_Disappearing;
    }

    internal static void Page_Appearing(object? sender, EventArgs e)
    {
        var onAppearingViewModel = GetPageVisibilityEventsViewModel(sender);

        if (onAppearingViewModel != null)
        {
            onAppearingViewModel.OnAppearing();
        }
    }

    internal static void Page_Disappearing(object? sender, EventArgs e)
    {
        var onDisappearingViewModel = GetPageVisibilityEventsViewModel(sender);

        if (onDisappearingViewModel != null)
        {
            onDisappearingViewModel.OnDisappearing();
        }
    }

    internal static IPageVisibilityEvents GetPageVisibilityEventsViewModel(object? sender)
    {
        var page = sender as Page;
        return page?.BindingContext as IPageVisibilityEvents;
    }
}