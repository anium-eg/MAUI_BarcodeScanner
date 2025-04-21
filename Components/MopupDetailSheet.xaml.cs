using System.Diagnostics;
using System.Reflection.Metadata;
using MAUI_BarcodeScanner.ViewModels;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Microsoft.Maui.Controls.PlatformConfiguration;
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