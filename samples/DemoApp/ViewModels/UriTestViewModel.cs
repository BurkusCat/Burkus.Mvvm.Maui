using System.Text;
using CommunityToolkit.Mvvm.Input;

namespace DemoApp.ViewModels;

public partial class UriTestViewModel : BaseViewModel
{
    #region Constructors

    public UriTestViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
    }

    #endregion Constructors

    #region Commands

    /// <summary>
    /// Go back multiple times
    /// </summary>
    [RelayCommand]
    private async Task GoBackMultipleTimes(int backTimes)
    {
        var uriBuilder = new StringBuilder();

        for (int i = 0; i < backTimes; i++)
        {
            uriBuilder.Append("../");
        }

        await navigationService.Navigate(uriBuilder.ToString());
    }

    #endregion Commands
}
