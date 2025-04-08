
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_BarcodeScanner.Components;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;

using The49.Maui.BottomSheet;


namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {

        public RangedObservableCollection<CartItem> Items { get; set; } = new();

        [ObservableProperty]
        int totalPrice;

        private Cart _cart;
        private ItemDetailSheet _detailSheet;

        [ObservableProperty]
        public bool isBusy;

        public CartViewModel(Cart cart, ItemDetailSheet detailSheet)
        {
            _cart = cart;
            _detailSheet = detailSheet;

            _detailSheet.viewModel.deleteItemEvent += DeleteItem;
            _detailSheet.Dismissed += OnDismissed;
        }


        [RelayCommand]
        public async Task ClearAllItems()
        {
            string confirmation = await Shell.Current.DisplayActionSheet("Are you sure you want to clear all items?", "Cancel", "Yes");
            if (confirmation.Equals("Yes"))
            {
                await _cart.ClearAllAsync();
                await RefreshList();
            }

        }

        [RelayCommand]
        async Task RefreshList()
        {
            IsBusy = true;
            IEnumerable<CartItem> scannedItems = await _cart.GetItemsAsync();
            Items.Clear();
            Items.AddRange(scannedItems);
            TotalPrice = calculateTotalPrice();
            IsBusy = false;
        }

        [RelayCommand]
        async void SelectItem(CartItem selectedItem)
        {
            _detailSheet.viewModel.CurrentItem = selectedItem;
            _detailSheet.ShowAsync(); //Call is not awaited to prevent edit button from being stuck in a disabled state
        }

        private async void OnDismissed(object sender, DismissOrigin e)
        {
            await RefreshList();
        }

        private async void DeleteItem(object sender, string id)
        {
            await _cart.DeleteItemAsync(id);
            await RefreshList();
            await _detailSheet.DismissAsync();
        }

        private int calculateTotalPrice()
        {
            return Items.Sum(item => item.TotalPrice);
        }

    }
}