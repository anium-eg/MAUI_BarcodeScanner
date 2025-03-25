
using MAUI_BarcodeScanner.ViewModels;

namespace MAUI_BarcodeScanner.Views;

public partial class ItemsPage : ContentPage
{
    public ItemsPage(ItemsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}