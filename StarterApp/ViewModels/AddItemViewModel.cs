using System.Windows.Input;
using StarterApp.Database.Data.Repositories;
using StarterApp.Models;

namespace StarterApp.ViewModels;

public class AddItemViewModel : BaseViewModel
{
    private readonly IItemRepository _itemRepository;

    public string Name { get; set; }

    public ICommand SaveCommand { get; }

    public AddItemViewModel(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
        SaveCommand = new Command(async () => await Save());
    }

    private async Task Save()
    {
        var item = new Item
        {
            Title = Name
        };

        await _itemRepository.AddItemAsync(item);
        await Shell.Current.GoToAsync("..");
    }
}