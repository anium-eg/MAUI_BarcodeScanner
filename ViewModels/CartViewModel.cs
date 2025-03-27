
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_BarcodeScanner.Components;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;
using Microsoft.Extensions.Logging;
using MvvmHelpers;
using The49.Maui.BottomSheet;


namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {

        public ObservableRangeCollection<CartItem> Items { get; set; } = new();

        private Datastore _datastore;
        private ItemDetailSheet _detailSheet;

        [ObservableProperty]
        public bool isBusy;

        public CartViewModel(Datastore datastore, ItemDetailSheet detailSheet)
        {
            _datastore = datastore;
            _detailSheet = detailSheet;

            _detailSheet.viewModel.deleteItemEvent += DeleteItem;
            _detailSheet.Dismissed += OnDismissed;
        }


        [RelayCommand]
        public async Task ClearAllItems()
        {
            await _datastore.ClearAllAsync();
            Items.Clear();
        }

        [RelayCommand]
        async Task RefreshList()
        {
            IsBusy = true;
            IEnumerable<CartItem> scannedItems = await _datastore.GetItemsAsync();
            Items.Clear();
            Items.AddRange(scannedItems);
            IsBusy = false;
        }

        [RelayCommand]
        async Task SelectItem(CartItem selectedItem)
        {
            _detailSheet.viewModel.CurrentItem = selectedItem;
            await _detailSheet.ShowAsync();
        }

        private async void OnDismissed(object sender, DismissOrigin e)
        {
            await RefreshList();
        }

        private async void DeleteItem(object sender, string id)
        {
            await _datastore.DeleteItemAsync(id);
            Items.Remove(Items.First(item => item.SKUId == id));
            await _detailSheet.DismissAsync();
        }

    }
}