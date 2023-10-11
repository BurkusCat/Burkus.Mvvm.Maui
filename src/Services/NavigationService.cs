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
        // todo: how to consider modal navigation/animations/parameters etc. at each segment of the navigation
        // todo: need to consider what query parameter should and shouldn't be.
        // should they be only for passing simple variables (strings, bools etc.) rather than complex json objects
        var navigation = Application.Current.MainPage.Navigation;

        var segments = UriUtility.GetUriSegments(uri);
        var instructions = segments.Select(UriUtility.ParseUriSegment)
            .ToList();

        var pagesToRemove = new List<Page>();

        if (UriUtility.IsUriAbsolute(uri))
        {
            if (instructions.Any(instruction => instruction.PageType == typeof(GoBackUriSegment)))
            {
                throw new Exception("You can't perform 'go back' actions during absolute URI navigation.");
            }

            // add every page as a page to be removed
            pagesToRemove.AddRange(navigation.NavigationStack);

            foreach (var page in navigation.NavigationStack)
            {
                await LifecycleEventUtility.TriggerOnNavigatingFrom(page?.BindingContext, navigationParameters);
            }
        }

        if (instructions.Count > 1)
        {
            // handle all but the last instruction
            for (int i = 0; i < instructions.Count() - 1; i++)
            {
                if (instructions[i].PageType == typeof(GoBackUriSegment))
                {
                    // handle "Go Back" URI segments
                    var pageToRemove = navigation.NavigationStack[^(i + 1)];
                    pagesToRemove.Add(pageToRemove);

                    await LifecycleEventUtility.TriggerOnNavigatingFrom(pageToRemove?.BindingContext, navigationParameters);
                }
                else
                {
                    // push pages relatively onto the stack
                    var pageToNavigateTo = ServiceResolver.Resolve(instructions[i].PageType) as Page;

                    if (navigationParameters.UseModalNavigation)
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                    }

                    await LifecycleEventUtility.TriggerOnNavigatedTo(pageToNavigateTo?.BindingContext, navigationParameters);
                }
            }
        }

        // handle final instruction
        var lastInstruction = instructions.Last();

        if (lastInstruction.PageType == typeof(GoBackUriSegment))
        {
            // remove all the pages that needed removed
            foreach (var pageToRemove in pagesToRemove)
            {
                navigation.RemovePage(pageToRemove);
                await LifecycleEventUtility.TriggerOnNavigatedFrom(pageToRemove?.BindingContext, navigationParameters);
            }

            var pageToPop = navigation.NavigationStack.Last();

            // pop final page
            if (navigationParameters.UseModalNavigation)
            {
                _ = await Application.Current.MainPage.Navigation.PopModalAsync(navigationParameters.UseAnimatedNavigation);
            }
            else
            {
                _ = await Application.Current.MainPage.Navigation.PopAsync(navigationParameters.UseAnimatedNavigation);
            }

            await LifecycleEventUtility.TriggerOnNavigatedFrom(pageToPop?.BindingContext, navigationParameters);
        }
        else
        {
            // push page relatively onto the stack
            var pageToNavigateTo = ServiceResolver.Resolve(lastInstruction.PageType) as Page;

            if (navigationParameters.UseModalNavigation)
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
            }

            // remove all the pages that needed removed
            foreach (var pageToRemove in pagesToRemove)
            {
                navigation.RemovePage(pageToRemove);
                await LifecycleEventUtility.TriggerOnNavigatedFrom(pageToRemove?.BindingContext, navigationParameters);
            }
        }

        var toBindingContext = MauiPageUtility.GetTopPageBindingContext();
        await LifecycleEventUtility.TriggerOnNavigatedTo(toBindingContext, navigationParameters);
    }

    #endregion URI navigation methods

    #region Internal implementation

    private async Task HandleNavigation<T>(Func<Task> navigationAction, NavigationParameters navigationParameters)
        where T : Page
    {
        var fromBindingContext = MauiPageUtility.GetTopPageBindingContext();

        await LifecycleEventUtility.TriggerOnNavigatingFrom(fromBindingContext, navigationParameters);
        
        await navigationAction.Invoke();

        await LifecycleEventUtility.TriggerOnNavigatingFrom(fromBindingContext, navigationParameters);

        var toBindingContext = MauiPageUtility.GetTopPageBindingContext();
        await LifecycleEventUtility.TriggerOnNavigatedTo(toBindingContext, navigationParameters);
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
