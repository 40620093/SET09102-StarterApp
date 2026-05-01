using System.Collections.ObjectModel; //for dynamic ui collection 
using StarterApp.Models; //item model
using StarterApp.Database.Data.Repositories; //repository access

namespace StarterApp.Views;

public partial class ItemsPage : ContentPage //page responsible for displaying all items
{
    private readonly IItemRepository _itemRepository; //repository used to access stored data
    public ObservableCollection<Item> Items { get; set;} //collection bound to ui 
    public ItemsPage(IItemRepository itemRepository) //constructor with dependency injection
    {
        InitializeComponent(); //this loads xaml ui 

        _itemRepository = itemRepository; //assign repository

    
        Items = new ObservableCollection<Item>(); //start with empty list
        
        
         BindingContext = this;
    }

    //runs every time page appears
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        
        Items.Clear(); //clear existing items to avoid duplicates when reloading 

        var itemsFromDb = await _itemRepository.GetAllItemsAsync(); // get items from database

        foreach (var item in itemsFromDb) // add each item to ui collection
        {
            Items.Add(new Item
            {
                Name = item.Title
            });
        }
    }

    private async void OnAddItemClicked(object sender, EventArgs e) //naviagte to Add Item page
    {
        await Shell.Current.GoToAsync(nameof(AddItemPage));
    }
}

public class Item
{
    public string Name { get; set;}
}