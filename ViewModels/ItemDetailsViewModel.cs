using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;

namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class ItemDetailsViewModel:ObservableObject
    {

        [ObservableProperty]
        public CartItem currentItem;

        public event EventHandler quantityIncreased;
        public event EventHandler<string> deleteItemEvent;

        [RelayCommand]
        public Task IncreaseQuantity()
        {
            CurrentItem.Quantity++;
            updateTotalPrice(CurrentItem);
            OnPropertyChanged(nameof(CurrentItem));
            return Task.FromResult("Success");
        }

        [RelayCommand]
        public Task DecreaseQuantity()
        {
            if (CurrentItem.Quantity == 1)
                return Task.FromResult("Succes");

            CurrentItem.Quantity--;
            updateTotalPrice(CurrentItem);
            OnPropertyChanged(nameof(CurrentItem));
            return Task.FromResult("Success");

        }

        [RelayCommand]
        public async Task DeleteItem()
        {
            string selectedOption = await Shell.Current.DisplayActionSheet("Are you sure?", "Delete", "Cancel");
            if(selectedOption == "Delete")
            {
                deleteItemEvent?.Invoke(this,CurrentItem.SKUId);
            }
        }

        void updateTotalPrice(CartItem item)
        {
            item.TotalPrice = item.PricePerItem * item.Quantity;
        }

    }
}
