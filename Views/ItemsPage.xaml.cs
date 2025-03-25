
using MAUI_BarcodeScanner.ViewModels;

namespace MAUI_BarcodeScanner.Views;

public partial class ItemsPage : ContentPage
{
    readonly ItemsViewModel viewModel;
    
    public ItemsPage(ItemsViewModel _viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel;
        viewModel = _viewModel;
	}

    protected override void OnAppearing()
    {
        viewModel.RefreshListCommand.Execute(null);
        base.OnAppearing();
    }

}