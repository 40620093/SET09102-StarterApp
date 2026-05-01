using StarterApp.ViewModels;
using StarterApp.Views;

namespace StarterApp;

public partial class AppShell : Shell
{
	public AppShell(AppShellViewModel viewModel)
	{	
		BindingContext = viewModel;
		InitializeComponent();

		Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
		//Registers a navigation route for ItemsPage so it can be accessed using shell navigation
		Routing.RegisterRoute(nameof(AddItemPage), typeof(AddItemPage));
		//Registers a navigation route for AddItemPage so it can be accessed using shell navigation
		Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
		//Registers a navigation route for the ItemDetailPage so users can view item details when selected
	}
}
