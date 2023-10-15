using DemoApp.Models;

namespace DemoApp.Views;

public partial class FlyoutMenuPage : ContentPage
{
    private readonly INavigationService navigationService;

    public FlyoutMenuPage()
    {
        navigationService = ServiceResolver.Resolve<INavigationService>();

        InitializeComponent();
        collectionView.SelectionChanged += OnSelectionChanged;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = collectionView.SelectedItem as FlyoutPageItem;

        if (item != null)
        {
            navigationService.SwitchFlyoutDetail(item.TargetType);
            collectionView.SelectedItem = null;
        }
    }
}