
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;

namespace MAUI_BarcodeScanner.Views;

public partial class ScannerCameraView : ContentPage
{
    readonly Inventory inventory;
    readonly IDataStore<Item> dataStore;

    private bool hasFiredAlready = false;
    public ScannerCameraView()
    {
        InitializeComponent();
        barcodeReaderComponent.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            TryHarder = true,
            AutoRotate = true
        };



        inventory = DependencyService.Get<Inventory>();
        dataStore = DependencyService.Get<IDataStore<Item>>();
    }

    private async void CameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        barcodeReaderComponent.IsDetecting = false;

        var result = e.Results.FirstOrDefault();

        if (result != null && !hasFiredAlready)
        {
            hasFiredAlready = true;
            await Dispatcher.DispatchAsync(async () =>
            {

                InventoryItem scannedItem = inventory.Items.Find(item => item.SKUId == result.Value); ;
                if (scannedItem != null)
                {
                    await dataStore.AddItemAsync(new Models.Item
                    {
                        SKUId = scannedItem.SKUId,
                        ProductName = scannedItem.ProductName
                    });

                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                    await Shell.Current.GoToAsync("//ItemsPage");


                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Invalid Item!", $"Item with SKU Id:{result.Value} is not in the inventory.", "Ok");
                }
            });

            hasFiredAlready = false;
            barcodeReaderComponent.IsDetecting = true;
        }



    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        barcodeReaderComponent.BarcodesDetected -= CameraBarcodeReaderView_BarcodesDetected;
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        barcodeReaderComponent.BarcodesDetected += CameraBarcodeReaderView_BarcodesDetected;
    }
}


