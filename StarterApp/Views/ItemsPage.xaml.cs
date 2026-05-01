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
               Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Category = item.Category,
                Location = item.Location,
                DailyRate = item.DailyRate,
                IsAvailable = item.IsAvailable,
                CreatedAtUtc = item.CreatedAtUtc,
                OwnerUserId = item.OwnerUserId
            });
        }
    }
    //runs when a user taps an item in the list
    private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection.FirstOrDefault() as Item;

        //if nothing selected, do nothing
        if (selectedItem == null)
        return;

        //navigate to the detail page and pass selected item
        await Shell.Current.GoToAsync(nameof(ItemDetailPage), true, new Dictionary<string, object>
        {
            {"SelectedItem", selectedItem }
            
        });

        //clears selection so user can tap again
        ((CollectionView)sender).SelectedItem = null;
    }
    private async void OnAddItemClicked(object sender, EventArgs e) //naviagte to Add Item page
    {
        await Shell.Current.GoToAsync(nameof(AddItemPage));
    }
}
