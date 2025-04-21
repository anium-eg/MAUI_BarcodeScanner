using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;

namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class SearchViewModel:ObservableObject
    {
        Inventory _inventory;
        Cart _cart;

        public RangedObservableCollection<InventoryItem> InventoryItems { get; set; } = new();

        public SearchViewModel(Inventory inventory, Cart cart)
        {
            _inventory = inventory;
            _cart = cart;
        }


        public void Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                InventoryItems.Clear();
                InventoryItems.AddRange(_inventory.Items);
            }

            IEnumerable<InventoryItem> searchMatches =  _inventory.Items.Where(item => (item.SKUId.Contains(searchTerm) || item.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
            InventoryItems.Clear();
            InventoryItems.AddRange(searchMatches);
        }

        [RelayCommand]
        public async Task RefreshList()
        {
            InventoryItems.Clear();
            InventoryItems.AddRange(_inventory.Items);
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
    }
}
