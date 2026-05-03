using StarterApp.Models;
using StarterApp.Database.Data.Repositories;

namespace StarterApp.Views;

//this allows navigation to pass the selected item into this page when navigating
[QueryProperty(nameof(SelectedItem), "SelectedItem")]
public partial class ItemDetailPage : ContentPage
{
    private Item _selectedItem;
    private readonly IItemRepository _itemRepository; //repository used to delete items from the database

    // property that receives the selected item passed from the previous page
    public Item SelectedItem
    {
        get=> _selectedItem;
        set
        {
            _selectedItem = value; //stores the incoming value

            BindingContext = _selectedItem;
        }
    }

    //constructor for the page
    public ItemDetailPage(IItemRepository itemRepository)
    {

    //store the repository so this page can access database actions
    _itemRepository = itemRepository;
    
        //loads the xaml for this page
        InitializeComponent();
    }
//event handler for when the Delete Item button is clicked
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
     //safety check if no item is loaded somehow, do nothing
     if (_selectedItem == null)
     return;

     //asking user to confirm deletion
     bool confirm = await DisplayAlertAsync(
        "Delete Item",
        $"Are you sure you want to delete? '{_selectedItem.Title}'?",
        "Yes",
        "No"
     );

     //if user cancels, stop here
     if (!confirm)
     return;

     //delete item from database using repository
     await _itemRepository.DeleteItemAsync(_selectedItem.Id);

     //go back to previous page after deletion
     await Shell.Current.GoToAsync("..");
    }

    //event handler for when the Edit Item button is clicked
    private async void OnEditClicked(object sender, EventArgs e)
    {
        //safety check to ensure an item has loaded before editing
        if (_selectedItem == null)
        {
            await DisplayAlertAsync("Error", "No item selected", "OK");
        
        return;
        }

        //navigate to AddItemPage and pass the selected item so the form is pre filled
        await Shell.Current.GoToAsync("AddItemPage", true, new Dictionary<string, object>
        {
            { "SelectedItem", _selectedItem }
        });
    }
}