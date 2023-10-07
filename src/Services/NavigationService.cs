namespace Burkus.Mvvm.Maui;

internal class NavigationService : INavigationService
{
    #region Core navigation methods

    public async Task Push<T>() where T : Page
    {
        await Push<T>(new NavigationParameters());
    }

    public async Task Push<T>(NavigationParameters navigationParameters) where T : Page
    {
        await HandleNavigation<T>(async () =>
            {
                var pageToNavigateTo = ServiceResolver.Resolve<T>();

                if (navigationParameters.UseModalNavigation)
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                }
            },
            navigationParameters);
    }

    public async Task Pop()
    {
        await Pop(new NavigationParameters());
    }

    public async Task Pop(NavigationParameters navigationParameters)
    {
        await HandleNavigation<Page>(async () =>
            {
                if (navigationParameters.UseModalNavigation)
                {
                    _ = await Application.Current.MainPage.Navigation.PopModalAsync(navigationParameters.UseAnimatedNavigation);
                }
                else
                {
                    _ = await Application.Current.MainPage.Navigation.PopAsync(navigationParameters.UseAnimatedNavigation);
                }
            },
            navigationParameters);
    }

    public async Task PopToRoot()
    {
        await PopToRoot(new NavigationParameters());
    }

    public async Task PopToRoot(NavigationParameters navigationParameters)
    {
        await HandleNavigation<Page>(async () =>
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync(navigationParameters.UseAnimatedNavigation);
            },
            navigationParameters);
    }

    #endregion Core navigation methods

    #region Advanced navigation methods

    public async Task ReplaceTopPage<T>()
        where T : Page
    {
        await ReplaceTopPage<T>(new NavigationParameters());
    }

    public async Task ReplaceTopPage<T>(NavigationParameters navigationParameters)
        where T : Page
    {
        await HandleNavigation<Page>(async () =>
            {
                var navigation = Application.Current.MainPage.Navigation;
                var pageToNavigateTo = ServiceResolver.Resolve<T>();

                navigation.InsertPageBefore(pageToNavigateTo, navigation.NavigationStack.Last());
                await navigation.PopAsync(navigationParameters.UseAnimatedNavigation);
            },
            navigationParameters);
    }

    public async Task ResetStackAndPush<T>()
        where T : Page
    {
        await ResetStackAndPush<T>(new NavigationParameters());
    }

    public async Task ResetStackAndPush<T>(NavigationParameters navigationParameters)
        where T : Page
    {
        await HandleNavigation<Page>(async () =>
            {
                var navigation = Application.Current.MainPage.Navigation;
                var pageToNavigateTo = ServiceResolver.Resolve<T>();

                if (navigation.NavigationStack.Count > 0)
                {
                    // insert page as the new root page
                    navigation.InsertPageBefore(pageToNavigateTo, navigation.NavigationStack.Last());
                }
                else
                {
                    // the stack was already empty
                    await navigation.PushAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                }

                await navigation.PopToRootAsync(navigationParameters.UseAnimatedNavigation);
            },
            navigationParameters);
    }

    #endregion Advanced navigation methods

    #region URI navigation methods

    public async Task Navigate(string uri)
    {
        await Navigate(uri, new NavigationParameters());
    }

    public async Task Navigate(string uri, NavigationParameters navigationParameters)
    {
        // todo: use navigation parameters and combine with query parameters. query parameters take precendence?

        var segments = UriUtility.GetUriSegments(uri);
        var instructions = segments.Select(UriUtility.ParseUriSegment)
            .ToList();

        // process last instruction first since it will be the one visible to the user
        if (UriUtility.IsUriAbsolute(uri) && instructions.Count() == 1)
        {
            // reset stack and push for the first navigation
            await ResetStackAndPushWithType(instructions.Single().PageType, instructions.Single().QueryParameters);

            // TODO: Do I need a ResetStack method to call first here? then do normal Push and insert pages behind?
        }
        else if (instructions.Last().PageType == typeof(GoBackUriSegment))
        {
            // remove all other pages first
            if (instructions.Count() > 1)
            {
                var navigation = Application.Current.MainPage.Navigation;

                // remove all but one page
                for (int i = 1; i < instructions.Count(); i++)
                {
                    var pageToRemove = navigation.NavigationStack[^(i + 1)];
                    navigation.RemovePage(pageToRemove);
                }
            }

            // then do the final pop
            await Pop(instructions.Last().QueryParameters);
        }
        else
        {
            // standard relative push onto the stack
            await PushWithType(instructions.Last().PageType, instructions.Last().QueryParameters);

            if (instructions.Count() > 1)
            {
                var navigation = Application.Current.MainPage.Navigation;

                for (int i = instructions.Count() - 2; i >= 0; i--)
                {
                    if (instructions[i].PageType == typeof(GoBackUriSegment))
                    {
                        // go back
                        var pageToRemove = navigation.NavigationStack[^i];
                        navigation.RemovePage(pageToRemove);
                    }
                    else
                    {
                        // insert pages behind the top page
                        var pageToNavigateTo = ServiceResolver.Resolve(instructions[i].PageType) as Page;
                        var pagesBeforeEnd = (instructions.Count() - 1 - i);
                        navigation.InsertPageBefore(pageToNavigateTo, navigation.NavigationStack[^pagesBeforeEnd]);
                    }
                }
            }
        }
    }

    #endregion URI navigation methods

    #region Internal implementation

    /// <summary>
    /// This method allows the <see cref="ResetStackAndPushWithType{T}(NavigationParameters)"/> to be called with reflection.
    /// </summary>
    private async Task ResetStackAndPushWithType(Type pageType, NavigationParameters navigationParameters)
    {
        var resetStackAndPushMethod = GetType()
            .GetMethod("ResetStackAndPush", new Type[] { typeof(NavigationParameters) })
            .MakeGenericMethod(pageType);
        await (Task)resetStackAndPushMethod.Invoke(this, new object[] { navigationParameters });
    }

    /// <summary>
    /// This method allows the <see cref="Push{T}(NavigationParameters)"/> to be called with reflection.
    /// </summary>
    private async Task PushWithType(Type pageType, NavigationParameters navigationParameters)
    {
        var pushMethod = GetType()
            .GetMethod("Push", new Type[] { typeof(NavigationParameters) })
            .MakeGenericMethod(pageType);
        await (Task)pushMethod.Invoke(this, new object[] { navigationParameters });
    }

    private async Task HandleNavigation<T>(Func<Task> navigationAction, NavigationParameters navigationParameters)
        where T : Page
    {
        var fromBindingContext = MauiPageUtility.GetTopPageBindingContext();
        var navigatingFromViewModel = fromBindingContext as INavigatingEvents;
        var navigatedFromViewModel = fromBindingContext as INavigatedEvents;

        if (navigatingFromViewModel != null)
        {
            await navigatingFromViewModel.OnNavigatingFrom(navigationParameters);
        }
        
        await navigationAction.Invoke();

        if (navigatedFromViewModel != null)
        {
            await navigatedFromViewModel.OnNavigatedFrom(navigationParameters);
        }

        var toBindingContext = MauiPageUtility.GetTopPageBindingContext();
        var navigatedToViewModel = toBindingContext as INavigatedEvents;

        if (navigatedToViewModel != null)
        {
            await navigatedToViewModel.OnNavigatedTo(navigationParameters);
        }
    }


    #endregion Internal implementation

    #region Tab navigation methods

    public void SelectTab<T>() where T : Page
    {
        var tabbedPage = MauiPageUtility.GetTopPage() as TabbedPage;

        if (tabbedPage == null)
        {
            // todo: warn about this in https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/17 ?
            return;
        }

        foreach (var child in tabbedPage.Children)
        {
            if (child.GetType() == typeof(T))
            {
                tabbedPage.CurrentPage = child;
                return;
            }

            if (child is NavigationPage)
            {
                if (((NavigationPage)child).CurrentPage.GetType() == typeof(T))
                {
                    tabbedPage.CurrentPage = child;
                    return;
                }
            }
        }
    }

    #endregion Tab navigation methods
}
