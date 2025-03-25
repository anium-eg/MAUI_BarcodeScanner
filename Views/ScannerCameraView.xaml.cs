
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;
using MAUI_BarcodeScanner.ViewModels;

namespace MAUI_BarcodeScanner.Views;

public partial class ScannerCameraView : ContentPage
{

    ScannerCameraViewModel viewModel;
    public ScannerCameraView(ScannerCameraViewModel _viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel;
        viewModel = _viewModel;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.HasFiredAlready = false;
    }



}


