using System.Windows.Input;
using StarterApp.Database.Data.Repositories;
using StarterApp.Models;

namespace StarterApp.ViewModels;

public class AddItemViewModel : BaseViewModel 
{
    private readonly IItemRepository _itemRepository; //repository used to interact with database

    //tracks if we are editing an exisiting item
    private bool _isEditMode = false;

    //stores the ID of the item being edited
    private int _editingItemID;
    
    public string Title { get; set; } //title entered by the user

    public string Description { get; set; } //description entered by the user

    public string Category { get; set; } //category entered by the user

    public string Location { get; set; } //Location entered by the user

    public string DailyRate { get; set; } //rental price entered by the user

    public ICommand SaveCommand { get; } //command bound to the save button

    public AddItemViewModel(IItemRepository itemRepository) //constructor, injects repository and sets up save command
    {
        _itemRepository = itemRepository;
        SaveCommand = new Command(async () => await Save()); //when button is clicked, run Save()
    }

    //loads an existing item into the form for editing
    public void LoadItemForEdit(Item item)
    {
        //switch to edit mode
        _isEditMode = true;

        //store the ID so we update the correct item later
        _editingItemID = item.Id;

        //populate the form fields with exisiting data 
        Title = item.Title;
        Description = item.Description;
        Category = item.Category;
        Location = item.Location;
        DailyRate = item.DailyRate.ToString(System.Globalization.CultureInfo.InvariantCulture);

        //refresh the ui so the form fields show the loaded values
        OnPropertyChanged(nameof(Title));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(Category));
        OnPropertyChanged(nameof(Location));
        OnPropertyChanged(nameof(DailyRate));

    }

    private async Task Save()
    {
        //validates that required fields are not empty
    if (string.IsNullOrWhiteSpace(Title) ||
        string.IsNullOrWhiteSpace(Description) ||
        string.IsNullOrWhiteSpace(Category) ||
        string.IsNullOrWhiteSpace(Location) ||
        string.IsNullOrWhiteSpace(DailyRate))
        {
            await Shell.Current.DisplayAlertAsync(
                "Missing Information",
                "Please fill in all fields",
                "OK"
            );
            return;
        }
        // validate that DailyRate entered by the user can be converted to a decimal 
        if (!decimal.TryParse(DailyRate, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var rate))
        {
            await Shell.Current.DisplayAlertAsync(
                "Invalid Price",
                "Please enter a valid daily rate (e.g. 10 or 10.50)",
                "OK"
            );
            return;
        }
        var item = new Item //creates a new item using the values entered on AddItemPage
        {
            Title = Title,
            Description = Description,
            Category = Category,
            Location = Location,
            DailyRate = rate
            
        };
        //decide if we are adding a new item or updating an already existing one
        if (_isEditMode)
        {
        // keep the same ID so the correct item is updated
        item.Id = _editingItemID;

        //update existing item
        await _itemRepository.UpdateItemAsync(item);
        }
        else
        {
            //add new item
            await _itemRepository.AddItemAsync(item);
        }
        
        //navigate back after saving
        if (_isEditMode)
        {
            await Shell.Current.GoToAsync("../..");
        }
        else
        {
            await Shell.Current.GoToAsync("..");
        }
        
    }
}