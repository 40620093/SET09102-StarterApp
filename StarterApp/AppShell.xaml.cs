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
		//Registers a navigation route fpr ItemsPage so it can be accessed using shell navigation
	}
}
