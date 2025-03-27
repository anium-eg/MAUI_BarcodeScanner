using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.ViewModels;
using The49.Maui.BottomSheet;

namespace MAUI_BarcodeScanner.Components;

public partial class ItemDetailSheet : BottomSheet
{
	public ItemDetailsViewModel viewModel;
	public ItemDetailSheet(ItemDetailsViewModel _viewModel)
	{
        InitializeComponent();
		BindingContext = _viewModel;
		viewModel = _viewModel;
	}
}