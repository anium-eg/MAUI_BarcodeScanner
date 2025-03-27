
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
        int totalPrice = 50;

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
            await RefreshList();
        }

        [RelayCommand]
        async Task RefreshList()
        {
            IsBusy = true;
            IEnumerable<CartItem> scannedItems = await _datastore.GetItemsAsync();
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
            await _datastore.DeleteItemAsync(id);
            await RefreshList();
            await _detailSheet.DismissAsync();
        }

        private int calculateTotalPrice()
        {
            return Items.Sum(item => item.TotalPrice);
        }

    }
}