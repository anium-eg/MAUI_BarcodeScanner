using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_BarcodeScanner.Components;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;
using Mopups.Services;

namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class SearchViewModel:ObservableObject
    {
        Inventory _inventory;
        Cart _cart;
        AddNewInventoryItemPopup _popup;

        [ObservableProperty]
        bool isRefreshing = false;

        public RangedObservableCollection<InventoryItem> InventoryItems { get; set; } = new();

        public SearchViewModel(Inventory inventory, Cart cart, AddNewInventoryItemPopup popup)
        {
            _inventory = inventory;
            _cart = cart;
            _popup = popup;
        }


        public void Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                InventoryItems.Clear();
                InventoryItems.AddRange(_inventory.GetAllItems());
            }

            IEnumerable<InventoryItem> searchMatches =  _inventory.GetAllItems().Where(item => (item.SKUId.Contains(searchTerm) || item.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
            InventoryItems.Clear();
            InventoryItems.AddRange(searchMatches);
        }

        [RelayCommand]
        public async Task RefreshList()
        {
            IsRefreshing = true;
            await _inventory.RefreshInventoryAsync();
            InventoryItems.Clear();
            InventoryItems.AddRange(_inventory.GetAllItems());
            IsRefreshing = false;
        }

        [RelayCommand]
        public async Task AddToCart(InventoryItem item)
        {
            await _cart.AddItemAsync(new CartItem
            {
                SKUId = item.SKUId,
                ProductName = item.ProductName,
                PricePerItem = item.Price,
                MRP = item.Price
            });

            await Shell.Current.DisplayAlert("Item added!", item.ProductName + " added to cart", "Ok");
        }

        [RelayCommand]
        public async Task AddNewItem()
        {
            await MopupService.Instance.PushAsync(_popup);
        }
    }
}
