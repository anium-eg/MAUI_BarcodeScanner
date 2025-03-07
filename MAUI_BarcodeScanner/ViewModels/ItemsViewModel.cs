
using System.Collections.ObjectModel;
using System.Diagnostics;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;


namespace MAUI_BarcodeScanner.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get;  }
        public Command<Item> ItemTapped { get; }
        public Command<string> ItemDeleteClicked { get; }
        public Command ClearAllItemsCommand { get; }

        readonly IDataStore<Item> inventory;

        public ItemsViewModel()
        {
            Title = "Cart";
            Items = new ObservableCollection<Item>();

            inventory = DependencyService.Get<IDataStore<Item>>();

            ItemDeleteClicked = new Command<string>(OnItemDelete);
            ClearAllItemsCommand = new Command(ClearAllItems);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }


        async void OnItemDelete(string id)
        {
            await inventory.DeleteItemAsync(id);
            Items.Remove(Items.FirstOrDefault(item => item.SKUId == id));
        }

        async void ClearAllItems()
        {
            await inventory.ClearAllAsync();
            Items.Clear();
        }
    }
}