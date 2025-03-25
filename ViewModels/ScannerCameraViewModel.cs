using System.Diagnostics;
using BarcodeScanning;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;

namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class ScannerCameraViewModel : ObservableObject
    {
        readonly Inventory inventory;
        readonly Datastore dataStore;

        private bool hasFiredAlready = false;
        public ScannerCameraViewModel(Inventory _inventory, Datastore _datastore)
        {
            inventory = _inventory;
            dataStore = _datastore;
        }

        [RelayCommand]
        public async Task BarcodeDetected(IReadOnlySet<BarcodeResult> scannedBarcode)
        {
            var result = scannedBarcode.FirstOrDefault();

            IEnumerable<Item> scannedItems = await dataStore.GetItemsAsync();


            if (result != null && !hasFiredAlready)
            {
                hasFiredAlready = true;

                InventoryItem? scannedItem = inventory.Items.Find(item => item.SKUId == result.RawValue); 

                if (scannedItem != null)
                {
                    await dataStore.AddItemAsync(new Item
                    {
                        SKUId = scannedItem.SKUId,
                        ProductName = scannedItem.ProductName
                    });

                    await Shell.Current.Navigation.PopToRootAsync();
                    await Shell.Current.DisplayAlert("Title",result.RawValue,"Ok");
                    await Shell.Current.GoToAsync("//ItemsPage");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Invalid Item!", $"Item with SKU Id:{result.RawValue} is not in the inventory.", "Ok");
                }

                hasFiredAlready = false;

            }

            Debug.WriteLine("current count" + scannedItems.FirstOrDefault().Quantity);


        }

    }
}
