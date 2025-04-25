using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_BarcodeScanner.Services;
using Mopups.Services;

namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class AddNewInventoryItemPopupViewModel:ObservableObject
    {
        readonly Inventory _inventoryService;
        public AddNewInventoryItemPopupViewModel(Inventory invetoryService)
        {
            _inventoryService = invetoryService;
        }

        [ObservableProperty]
        private string skuId;

        [ObservableProperty]
        private string productName;

        [ObservableProperty]
        private int productPrice;

        [ObservableProperty]
        private int productStock;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool hasErrors;

        [ObservableProperty]
        private string errorMessage;

        [RelayCommand]
        public async Task SubmitNewItemRequest()
        {
            IsLoading = true;
            HttpResponseMessage response = await _inventoryService.AddInventoryItemAsync(
                new Models.InventoryItem
                    {
                        Price = ProductPrice,
                        SKUId = SkuId,
                        Stock = ProductStock,
                        ProductName = ProductName
                    });

            if (response.IsSuccessStatusCode)
            {
                await Toast.Make("Added new item to inventory!", ToastDuration.Short).Show();
                await MopupService.Instance.PopAsync();
                resetForm();

            }

            else if(response.StatusCode == HttpStatusCode.Conflict)
            {
                HasErrors = true;
                ErrorMessage = "Item with SKU ID already exists!";
            }

            else
            {
                HasErrors = true;
                ErrorMessage = "Something went wrong! Try again";
            }

            IsLoading = false;

        }

        [RelayCommand]
        public void PopupBackgroundClicked()
        {
            Debug.WriteLine("Backgorund Clicked");
            MopupService.Instance.PopAsync();
            resetForm();
        }

        private void resetForm()
        {
            SkuId = "";
            ProductName = "";
            ProductPrice = 0;
            ProductStock = 0;

            HasErrors = false;
            ErrorMessage = "";
        }
    }
}
