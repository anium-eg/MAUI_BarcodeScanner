
using System.Collections.ObjectModel;
using MAUI_BarcodeScanner.Models;


namespace MAUI_BarcodeScanner.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        

        readonly ObservableCollection<Item> items;
        public MockDataStore()
        {
            items = new ObservableCollection<Item>();
        }

        public async Task<bool> AddItemAsync(Item item)
        {

            Item currentItem = items.FirstOrDefault(itemInStore => itemInStore.SKUId == item.SKUId);

            if (currentItem != null)
                currentItem.Quantity += 1;

            else
                items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.SKUId == item.SKUId).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.SKUId == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.SKUId == id));
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