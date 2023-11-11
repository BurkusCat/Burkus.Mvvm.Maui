namespace Burkus.Mvvm.Maui;

public interface INavigationService
{
    #region Core navigation methods

    /// <summary>
    /// Push a new page onto the navigation stack.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    /// <returns>A completed task</returns>
    Task Push<T>()
        where T : Page;

    /// <summary>
    /// Push a new page onto the navigation stack.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    /// <param name="navigationParameters">Navigation parameters to pass</param>
    /// <returns>A completed task</returns>
    Task Push<T>(NavigationParameters navigationParameters)
        where T : Page;

    /// <summary>
    /// Pop the top page of the navigation stack.
    /// </summary>
    /// <returns>A completed task</returns>
    Task Pop();

    /// <summary>
    /// Pop the top page of the navigation stack.
    /// </summary>
    /// <param name="navigationParameters">Navigation parameters to pass</param>
    /// <returns>A completed task</returns>
    Task Pop(NavigationParameters navigationParameters);

    /// <summary>
    /// Pop to the root of the navigation stack.
    /// </summary>
    /// <returns>A completed task</returns>
    Task PopToRoot();

    /// <summary>
    /// Pop to the root of the navigation stack.
    /// </summary>
    /// <param name="navigationParameters">Navigation parameters to pass</param>
    /// <returns>A completed task</returns>
    Task PopToRoot(NavigationParameters navigationParameters);

    #endregion Core navigation methods

    #region Advanced navigation methods

    /// <summary>
    /// Pops the top page of the navigation stack or closes the app if it is the last page.
    /// </summary>
    /// <returns>A completed task</returns>
    Task GoBack();

    /// <summary>
    /// Pops the top page of the navigation stack or closes the app if it is the last page.
    /// </summary>
    /// <param name="navigationParameters">Navigation parameters to pass</param>
    /// <returns>A completed task</returns>
    Task GoBack(NavigationParameters navigationParameters);

    /// <summary>
    /// Replace the top page of the stack with a new page.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    /// <returns>A completed task</returns>
    Task ReplaceTopPage<T>()
        where T : Page;

    /// <summary>
    /// Replace the top page of the stack with a new page.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    /// <param name="navigationParameters">Navigation parameters to pass</param>
    /// <returns>A completed task</returns>
    Task ReplaceTopPage<T>(NavigationParameters navigationParameters)
        where T : Page;

    /// <summary>
    /// Reset the navigation stack and push a new page.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    /// <returns>A completed task</returns>
    Task ResetStackAndPush<T>()
        where T : Page;

    /// <summary>
    /// Reset the navigation stack and push a new page.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    /// <param name="navigationParameters">Navigation parameters to pass</param>
    /// <returns>A completed task</returns>
    Task ResetStackAndPush<T>(NavigationParameters navigationParameters)
        where T : Page;

    #endregion Advanced navigation methods

    #region URI navigation methods

    /// <summary>
    /// Navigate using a string URI.
    /// </summary>
    /// <example>
    /// <code>
    /// // use absolute navigation to go to the LoginPage
    /// navigationService.Navigate("/LoginPage");
    /// 
    /// // push multiple pages relatively onto the stack
    /// navigationService.Navigate("RegisterPage/DemoTabsPage/RegisterPage");
    /// 
    /// // go back one page
    /// navigationService.Navigate("..");
    /// </code>
    /// </example>
    /// <param name="uri">The URI to navigate to</param>
    /// <remarks>This method is *very* experimental and likely to change.</remarks>
    /// <returns>A completed task</returns>
    Task Navigate(string uri);

    /// <summary>
    /// Navigate using a string URI.
    /// </summary>
    /// <example>
    /// <code>
    /// // use absolute navigation to go to the LoginPage
    /// navigationService.Navigate("/LoginPage");
    /// 
    /// // push multiple pages relatively onto the stack
    /// navigationService.Navigate("RegisterPage/DemoTabsPage/RegisterPage");
    /// 
    /// // go back one page
    /// navigationService.Navigate("..");
    /// </code>
    /// </example>
    /// <param name="uri">The URI to navigate to</param>
    /// <param name="navigationParameters">Navigation parameters to pass</param>
    /// <remarks>This method is *very* experimental and likely to change.</remarks>
    /// <returns>A completed task</returns>
    Task Navigate(string uri, NavigationParameters navigationParameters);

    #endregion URI navigation methods

    #region Tab navigation methods

    /// <summary>
    /// When within a <see cref="TabbedPage"/>, use this method to select a tab.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    void SelectTab<T>()
        where T : Page;

    #endregion Tab navigation methods

    #region Flyout navigation methods

    /// <summary>
    /// When within a <see cref="FlyoutPage"/>, use this method to switch out the current detail page.
    /// </summary>
    /// <param name="detailType"></param>
    void SwitchFlyoutDetail(Type detailType);

    /// <summary>
    /// When within a <see cref="FlyoutPage"/>, use this method to switch out the current detail page.
    /// </summary>
    /// <typeparam name="T">Type of Page</typeparam>
    void SwitchFlyoutDetail<T>()
        where T : Page;

    #endregion Flyout navigation methods
}
