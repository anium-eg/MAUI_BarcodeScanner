using MAUI_BarcodeScanner.ViewModels;
using Mopups.Pages;

namespace MAUI_BarcodeScanner.Components;

public partial class MopupDetailSheet : PopupPage
{
	public MopupDetailSheet(MopupDetailSheetViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}