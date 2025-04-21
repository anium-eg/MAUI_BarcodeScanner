
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_BarcodeScanner.Components;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;
using Mopups.Services;
using The49.Maui.BottomSheet;


namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        public RangedObservableCollection<CartItem> Items { get; set; } = new();

        [ObservableProperty]
        int totalPrice;

        private Cart _cart;

        [ObservableProperty]
        public bool isBusy;

        public CartViewModel(Cart cart)
        {
            _cart = cart;
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

            MopupDetailSheetViewModel detailSheetViewModel = new MopupDetailSheetViewModel(selectedItem);
            detailSheetViewModel.deleteItemEvent += DeleteItem;
            detailSheetViewModel.popupClosedEvent += OnDismissed;

            await MopupService.Instance.PushAsync(new MopupDetailSheet(detailSheetViewModel));

        }

        private async void OnDismissed(object? sender, EventArgs e)
        {
            await RefreshList();
        }

        private async void DeleteItem(object? sender, string id)
        {
            await _cart.DeleteItemAsync(id);
            await RefreshList();
            await MopupService.Instance.PopAsync();
        }

        private int calculateTotalPrice()
        {
            return Items.Sum(item => item.TotalPrice);
        }

    }
}