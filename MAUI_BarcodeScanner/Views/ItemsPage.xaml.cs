
using MAUI_BarcodeScanner.ViewModels;

namespace MAUI_BarcodeScanner.Views;

public partial class ItemsPage : ContentPage
{
    ItemsViewModel _viewModel;
    public ItemsPage()
	{
		InitializeComponent();
		BindingContext = _viewModel = new ItemsViewModel();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.OnAppearing();
    }
}