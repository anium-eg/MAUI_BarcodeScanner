using System.Diagnostics;
using MAUI_BarcodeScanner.ViewModels;

namespace MAUI_BarcodeScanner.Views;

public partial class SearchPage : ContentPage
{
	SearchViewModel _viewModel;
	public SearchPage(SearchViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

    protected override void OnAppearing()
    {
		_viewModel.RefreshListCommand.Execute(this);
        base.OnAppearing();
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
		 _viewModel.Search(e.NewTextValue);
    }
}