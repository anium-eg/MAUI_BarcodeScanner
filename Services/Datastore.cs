
using System.Collections.ObjectModel;
using MAUI_BarcodeScanner.Models;


namespace MAUI_BarcodeScanner.Services
{
    public class Datastore
    {

        readonly ObservableCollection<CartItem> items;
        public Datastore()
        {
            items = new ObservableCollection<CartItem>();
        }

        public async Task<bool> AddItemAsync(CartItem item)
        {
            CartItem? currentItem = items.FirstOrDefault(itemInStore => itemInStore.SKUId == item.SKUId);

            if (currentItem != null)
            {
                currentItem.Quantity += 1;
                currentItem.TotalPrice = currentItem.PricePerItem * currentItem.Quantity;
            }

            else
            {
                item.TotalPrice = item.PricePerItem;
                items.Add(item);
            }
                

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(CartItem item)
        {
            var oldItem = items.First((CartItem arg) => arg.SKUId == item.SKUId);
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.First((CartItem item) => item.SKUId == id);
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<CartItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.First(item => item.SKUId == id));
        }

        public async Task<IEnumerable<CartItem>> GetItemsAsync(bool forceRefresh = false)
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