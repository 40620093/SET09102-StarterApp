using System.Collections.ObjectModel;
using StarterApp.Models;

namespace StarterApp.Views;

public partial class ItemsPage : ContentPage
{
    public ObservableCollection<Item> Items { get; set;}
    public ItemsPage()
    {
        InitializeComponent();

        //first tool added
        Items = new ObservableCollection<Item>
        {
            new Item
            {
                Name = "Cordless Drill"
            }
        };

        BindingContext = this;
    }

    private async void OnAddItemClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddItemPage));
    }
}

public class Item
{
    public string Name { get; set;}
}