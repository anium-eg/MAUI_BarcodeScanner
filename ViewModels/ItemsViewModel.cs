
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;
using Microsoft.Extensions.Logging;
using MvvmHelpers;


namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class ItemsViewModel : ObservableObject
    {

        public ObservableRangeCollection<Item> Items { get; set; } = new();

        [ObservableProperty]
        private Datastore mockDatastore;

        [ObservableProperty]
        public bool isBusy = false;

        public ItemsViewModel(Datastore _datastore)
        {
            mockDatastore = _datastore;
            //Items = mockDatastore.GetItemsAsync().Result;
        }

        //async Task ExecuteLoadItemsCommand()
        //{
        //    IsBusy = true;
        //    IEnumerable<Item> scannedItems = await mockDatastore.GetItemsAsync();
        //    //Items = scannedItems.ToObservableCollection<Item>();
        //    IsBusy = false;
        //}

        [RelayCommand]
        public async Task OnItemDelete(string id)
        {
            await MockDatastore.DeleteItemAsync(id);
            Items.Remove(Items.First(item => item.SKUId == id));
        }

        [RelayCommand]
        public async Task ClearAllItems()
        {
            await MockDatastore.ClearAllAsync();
            Items.Clear();
        }

        [RelayCommand]
        async Task RefreshList()
        {
            Debug.WriteLine("Refreshed this list-----------");
            IsBusy = true;
            IEnumerable<Item> scannedItems = await MockDatastore.GetItemsAsync();
            Items.Clear();
            Items.AddRange(scannedItems);
            IsBusy = false;
        }


        //[RelayCommand]
        //public async Task Appearing()
        //{
        //    Debug.WriteLine("-----------------------------------Executing on appear");
        //    IsBusy = true;
        //    IEnumerable<Item> scannedItems = await mockDatastore.GetItemsAsync();
        //    Debug.WriteLine("HERE HERE HRER H------"+scannedItems.First().Quantity);
        //    Items.Clear();
        //    Items.AddRange(scannedItems);
        //    Debug.WriteLine("In the itme"+Items.First().Quantity);
        //    IsBusy = false;
        //}

        //[RelayCommand]
        //public async Task LoadItems()
        //{
            
        //    Debug.WriteLine("Items are loading -----------------");
        //    await Appearing();
        //    return;
        //}
    }
}