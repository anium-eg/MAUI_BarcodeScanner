
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;
using MAUI_BarcodeScanner.ViewModels;

namespace MAUI_BarcodeScanner.Views;

public partial class ScannerCameraView : ContentPage
{
    readonly Inventory inventory;
    readonly Datastore dataStore;

    private bool hasFiredAlready = false;
    public ScannerCameraView(ScannerCameraViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;




        //inventory = DependencyService.Get<Inventory>();
        //dataStore = DependencyService.Get<Datastore>();
    }

    //private async void CameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    //{
    //    barcodeReaderComponent.IsDetecting = false;

    //    var result = e.Results.FirstOrDefault();

    //    if (result != null && !hasFiredAlready)
    //    {
    //        hasFiredAlready = true;
    //        await Dispatcher.DispatchAsync(async () =>
    //        {

    //            InventoryItem scannedItem = inventory.Items.Find(item => item.SKUId == result.Value); ;
    //            if (scannedItem != null)
    //            {
    //                await dataStore.AddItemAsync(new Models.Item
    //                {
    //                    SKUId = scannedItem.SKUId,
    //                    ProductName = scannedItem.ProductName
    //                });

    //                await Shell.Current.Navigation.PopToRootAsync();
    //                await Shell.Current.GoToAsync("//ItemsPage");


    //            }
    //            else
    //            {
    //                await Shell.Current.DisplayAlert("Invalid Item!", $"Item with SKU Id:{result.Value} is not in the inventory.", "Ok");
    //            }
    //        });

    //        hasFiredAlready = false;
    //        barcodeReaderComponent.IsDetecting = true;
    //    }



    //}
    //protected override void OnDisappearing()
    //{
    //    barcodeReaderComponent.BarcodesDetected -= CameraBarcodeReaderView_BarcodesDetected;
    //    base.OnDisappearing();
    //}

    //protected override bool OnBackButtonPressed()
    //{

    //    barcodeReaderComponent.BarcodesDetected -= CameraBarcodeReaderView_BarcodesDetected;
    //    return base.OnBackButtonPressed();

    //}

    //protected override void OnAppearing()
    //{

    //    barcodeReaderComponent.BarcodesDetected += CameraBarcodeReaderView_BarcodesDetected;
    //    base.OnAppearing();
    //}

}


