namespace Burkus.Mvvm.Maui;

public class DialogService : IDialogService
{
    public virtual Task<bool> DisplayAlert(string title, string message, string accept, string cancel, FlowDirection flowDirection = FlowDirection.MatchParent)
    {
        return Application.Current.MainPage
            .DisplayAlert(title, message, accept, cancel, flowDirection);
    }

    public virtual Task DisplayAlert(string title, string message, string cancel, FlowDirection flowDirection = FlowDirection.MatchParent)
    {
        return Application.Current.MainPage
            .DisplayAlert(title, message, cancel, flowDirection);
    }

    public virtual Task<string> DisplayActionSheet(string title, string cancel = default, string destruction = default, FlowDirection flowDirection = FlowDirection.MatchParent, params string[] buttons)
    {
        return Application.Current.MainPage
            .DisplayActionSheet(title, cancel, destruction, flowDirection, buttons);
    }

    public virtual Task<string> DisplayPrompt(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = default, int maxLength = -1, Keyboard keyboard = default, string initialValue = "")
    {
        return Application.Current.MainPage
            .DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, keyboard, initialValue);
    }
}