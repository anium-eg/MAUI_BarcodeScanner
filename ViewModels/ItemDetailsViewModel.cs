using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation.Results;
using MAUI_BarcodeScanner.Helpers.Validators;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class ItemDetailsViewModel:ObservableObject
    {
        readonly CartItemPriceValidator cartValidator;
        public ItemDetailsViewModel()
        {
            cartValidator = new CartItemPriceValidator();
        }

        [ObservableProperty]
        public CartItem currentItem;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrentItem))]
        public string pricePerItem = "20";

        [ObservableProperty]
        public bool isPriceInvalid;

        [ObservableProperty]
        public string invalidPriceMessage = "";

        public event EventHandler quantityIncreased;
        public event EventHandler<string> deleteItemEvent;

        partial void OnPricePerItemChanged(string value)
        {
            if(int.TryParse(pricePerItem, out int result))
            {
                currentItem.PricePerItem = result;

                ValidationResult validationResult = cartValidator.Validate(currentItem);

                IsPriceInvalid = !validationResult.IsValid;

                if (validationResult.Errors.FirstOrDefault() == null)
                    InvalidPriceMessage = "";
                else
                    InvalidPriceMessage = validationResult.Errors.FirstOrDefault().ErrorMessage;

            }

            else
            {
                IsPriceInvalid = true;
                InvalidPriceMessage = "Please enter a valid number";
            }


        }


        [RelayCommand]
        public Task IncreaseQuantity()
        {
            CurrentItem.Quantity++;
            return Task.FromResult("Success");
        }

        [RelayCommand]
        public Task DecreaseQuantity()
        {
            if (CurrentItem.Quantity == 1)
                return Task.FromResult("Succes");

            CurrentItem.Quantity--;
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

    }
}
