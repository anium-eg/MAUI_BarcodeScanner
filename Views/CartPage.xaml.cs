
using MAUI_BarcodeScanner.ViewModels;

namespace MAUI_BarcodeScanner.Views;

public partial class ItemsPage : ContentPage
{
    readonly CartViewModel viewModel;
    
    public ItemsPage(CartViewModel _viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel;
        viewModel = _viewModel;
	}

    protected override void OnAppearing()
    {
        viewModel.RefreshListCommand.Execute(this);
        base.OnAppearing();
    }

}