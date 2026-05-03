using System.Windows.Input;
using StarterApp.Database.Data.Repositories;
using StarterApp.Models;

namespace StarterApp.ViewModels;

public class AddItemViewModel : BaseViewModel
{
    private readonly IItemRepository _itemRepository;
    
    public string Title { get; set; } //title entered by the user

    public string Description { get; set; } //description entered by the user

    public string Category { get; set; } //category entered by the user

    public string Location { get; set; } //Location entered by the user

    public string DailyRate { get; set; } //rental price entered by the user

    public ICommand SaveCommand { get; }

    public AddItemViewModel(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
        SaveCommand = new Command(async () => await Save());
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
        
        if (!decimal.TryParse(DailyRate, out var rate)) // validate that DailyRate entered by the user can be converted to a decimal 
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
        
        await _itemRepository.AddItemAsync(item);
        await Shell.Current.GoToAsync("..");
    }
}