
using System.Collections.ObjectModel;
using MAUI_BarcodeScanner.Models;


namespace MAUI_BarcodeScanner.Services
{
    public class Datastore
    {

        readonly ObservableCollection<Item> items;
        public Datastore()
        {
            items = new ObservableCollection<Item>();
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            Item? currentItem = items.FirstOrDefault(itemInStore => itemInStore.SKUId == item.SKUId);

            if (currentItem != null)
                currentItem.Quantity += 1;

            else
                items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.First((Item arg) => arg.SKUId == item.SKUId);
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.First((Item item) => item.SKUId == id);
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.First(item => item.SKUId == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> ClearAllAsync()
        {
            items.Clear();
            return await Task.FromResult(true);
        }
    }
}