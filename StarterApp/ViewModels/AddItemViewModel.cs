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

    public decimal DailyRate { get; set; } //daily rental price entered by the user

    public ICommand SaveCommand { get; }

    public AddItemViewModel(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
        SaveCommand = new Command(async () => await Save());
    }

    private async Task Save()
    {
        var item = new Item //creates a new item using the values entered on AddItemPage
        {
            Title = Title,
            Description = Description,
            Category = Category,
            Location = Location,
            DailyRate = DailyRate
        };

        await _itemRepository.AddItemAsync(item);
        await Shell.Current.GoToAsync("..");
    }
}