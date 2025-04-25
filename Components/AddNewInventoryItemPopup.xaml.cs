using MAUI_BarcodeScanner.ViewModels;
using Mopups.Pages;

namespace MAUI_BarcodeScanner.Components;

public partial class AddNewInventoryItemPopup : PopupPage
{
	public AddNewInventoryItemPopup(AddNewInventoryItemPopupViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}