using System.Diagnostics;
using BarcodeScanning;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;
using MAUI_BarcodeScanner.Views;

namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class ScannerCameraViewModel : ObservableObject
    {
        readonly Inventory inventory;
        readonly Datastore dataStore;

        [ObservableProperty]
        private bool hasFiredAlready;

        public ScannerCameraViewModel(Inventory _inventory, Datastore _datastore)
        {
            inventory = _inventory;
            dataStore = _datastore;
        }

        [RelayCommand]
        public async Task BarcodeDetected(IReadOnlySet<BarcodeResult> scannedBarcodes)
        {
            BarcodeResult? result = scannedBarcodes.FirstOrDefault();

            if (result != null && !HasFiredAlready)
            {
                HasFiredAlready = true;
                IEnumerable<CartItem> scannedItems = await dataStore.GetItemsAsync();
                InventoryItem? scannedItem = inventory.Items.Find(item => item.SKUId == result.RawValue); 

                if (scannedItem != null)
                {
                    await dataStore.AddItemAsync(new CartItem
                    {
                        SKUId = scannedItem.SKUId,
                        ProductName = scannedItem.ProductName,
                        PricePerItem = scannedItem.Price
                    });

                    await Shell.Current.GoToAsync("//"+nameof(ItemsPage));
                }

                else
                {
                    await Shell.Current.DisplayAlert("Invalid Item!", $"Item with SKU Id:{result.RawValue} is not in the inventory.", "Ok");
                    HasFiredAlready = false;
                }

            }
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("//" + nameof(ScannerPage));
        }
    }
}
