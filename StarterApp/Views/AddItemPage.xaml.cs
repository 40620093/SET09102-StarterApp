using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class AddItemPage : ContentPage
{
    public AddItemPage(AddItemViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}