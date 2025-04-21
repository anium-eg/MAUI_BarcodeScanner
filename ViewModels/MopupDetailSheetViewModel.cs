using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation.Results;
using MAUI_BarcodeScanner.Components;
using MAUI_BarcodeScanner.Helpers.Validators;
using MAUI_BarcodeScanner.Models;
using Mopups.Pages;
using Mopups.PreBaked.AbstractClasses;
using Mopups.PreBaked.Interfaces;
using Mopups.Services;

namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class MopupDetailSheetViewModel : ObservableObject 
    {
        [ObservableProperty]
        public CartItem currentItem;


        CartItemPriceValidator cartValidator;

        public MopupDetailSheetViewModel(CartItem cartItem)
        {
            CurrentItem = cartItem;
            pricePerItem = CurrentItem.PricePerItem.ToString();
            cartValidator = new CartItemPriceValidator();
        }


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrentItem))]
        public string pricePerItem;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanCloseBottomSheet))]
        [NotifyCanExecuteChangedFor(nameof(CloseBottomSheetCommand))]
        public bool isPriceInvalid;

        [ObservableProperty]
        public string invalidPriceMessage = "";

        public bool CanCloseBottomSheet => !IsPriceInvalid;

        public event EventHandler popupClosedEvent;
        public event EventHandler<string> deleteItemEvent;

        partial void OnPricePerItemChanged(string value)
        {
            if (int.TryParse(PricePerItem, out int result))
            {
                currentItem.PricePerItem = result;
                ValidationResult validationResult = cartValidator.Validate(CurrentItem);
                IsPriceInvalid = !validationResult.IsValid;
                InvalidPriceMessage = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty;
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
            if (selectedOption == "Delete")
            {
                deleteItemEvent?.Invoke(this, CurrentItem.SKUId);
            }
        }


        [RelayCommand(CanExecute = nameof(CanCloseBottomSheet))]
        public async Task CloseBottomSheet()
        {
            popupClosedEvent.Invoke(this,null);
            await MopupService.Instance.PopAsync();
        }


        [RelayCommand]
        public async Task BackgroundClicked()
        {
            popupClosedEvent.Invoke(this, EventArgs.Empty);

            if (IsPriceInvalid)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Price", "Please enter a valid price!", "Ok");
            }

        }


    }
}
