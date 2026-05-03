using StarterApp.ViewModels;
using StarterApp.Models;

namespace StarterApp.Views;

//allows this page to receive an item object via navigation (this is used for editing)
[QueryProperty(nameof(SelectedItem), "SelectedItem")]

public partial class AddItemPage : ContentPage
{
    public AddItemPage(AddItemViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm; //sets the view model as the binding context for this page
    }

    //property that is triggered when an item is passed to this page
    public Item SelectedItem
    {
        set
        {
            //ensures the BindingContext is the correct ViewModel before using it
            if (BindingContext is AddItemViewModel viewModel)

            //loads the selected item into the ViewModel to pre fill form fields
            viewModel.LoadItemForEdit(value);
        
        }
    }


}