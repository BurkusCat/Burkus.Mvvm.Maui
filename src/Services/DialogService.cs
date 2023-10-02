namespace Burkus.Mvvm.Maui;

public class DialogService : IDialogService
{
    /// <summary>
    /// Presents an alert dialog to the application user with an accept and a cancel button.
    /// </summary>
    /// <param name="title">The title of the alert dialog.</param>
    /// <param name="message">The body text of the alert dialog.</param>
    /// <param name="accept">Text to be displayed on the 'Accept' button.</param>
    /// <param name="cancel">Text to be displayed on the 'Cancel' button.</param>
    /// <param name="flowDirection">Enumerates values that control the layout direction for views.</param>
    /// <returns>A task that contains the user's choice as a Boolean value. true indicates that the user accepted the alert. false indicates that the user cancelled the alert.</returns>
    public virtual Task<bool> DisplayAlert(string title, string message, string accept, string cancel, FlowDirection flowDirection = FlowDirection.MatchParent)
    {
        return Application.Current.MainPage
            .DisplayAlert(title, message, accept, cancel, flowDirection);
    }

    /// <summary>
    /// Presents an alert dialog to the application user with a single cancel button.
    /// </summary>
    /// <param name="title">The title of the alert dialog.</param>
    /// <param name="message">The body text of the alert dialog.</param>
    /// <param name="cancel">Text to be displayed on the 'Cancel' button.</param>
    /// <param name="flowDirection">Enumerates values that control the layout direction for views.</param>
    /// <returns>Task</returns>
    public virtual Task DisplayAlert(string title, string message, string cancel, FlowDirection flowDirection = FlowDirection.MatchParent)
    {
        return Application.Current.MainPage
            .DisplayAlert(title, message, cancel, flowDirection);
    }

    /// <summary>
    /// Displays a native platform action sheet, allowing the application user to choose from several buttons.
    /// </summary>
    /// <param name="title">Title of the displayed action sheet. Must not be null.</param>
    /// <param name="cancel">Text to be displayed in the 'Cancel' button. Can be null to hide the cancel action.</param>
    /// <param name="destruction">Text to be displayed in the 'Destruct' button. Can be null to hide the destructive option.</param>
    /// <param name="flowDirection">Enumerates values that control the layout direction for views.</param>
    /// <param name="buttons">Text labels for additional buttons. Must not be null.</param>
    /// <returns>An awaitable Task that displays an action sheet and returns the Text of the button pressed by the user.</returns>
    /// <remarks>
    /// Developers should be aware that Windows' line endings, CR-LF, only work on Windows systems, and are incompatible with iOS and Android. A particular consequence of this is that characters that appear after a CR-LF, (For example, in the title.) may not be displayed on non-Windows platforms. Developers must use the correct line endings for each of the targeted systems.
    /// </remarks>
    public virtual Task<string> DisplayActionSheet(string title, string cancel = default, string destruction = default, FlowDirection flowDirection = FlowDirection.MatchParent, params string[] buttons)
    {
        return Application.Current.MainPage
            .DisplayActionSheet(title, cancel, destruction, flowDirection, buttons);
    }

    /// <summary>
    /// Displays a native prompt, allowing the application user to enter keyboard input.
    /// </summary>
    /// <param name="title">The title of the prompt dialog.</param>
    /// <param name="message">The body text of the prompt dialog.</param>
    /// <param name="accept">Text to be displayed on the 'Accept' button.</param>
    /// <param name="cancel">Text to be displayed on the 'Cancel' button.</param>
    /// <param name="placeholder">The placeholder text for the prompt dialog.</param>
    /// <param name="maxLength">The maximum length of text allowed to be entered.</param>
    /// <param name="keyboard">The type of keyboard to be used.</param>
    /// <param name="initialValue">The initial value of the text in the prompt dialog.</param>
    /// <returns></returns>
    public virtual Task<string> DisplayPrompt(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = default, int maxLength = -1, Keyboard keyboard = default, string initialValue = "")
    {
        return Application.Current.MainPage
            .DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, keyboard, initialValue);
    }
}