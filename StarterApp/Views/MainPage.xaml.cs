using StarterApp.ViewModels;
using StarterApp.Views;

namespace StarterApp.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnToolsTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ItemsPage));
    }
}