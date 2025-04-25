using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;
using Microsoft.Extensions.Configuration;

namespace MAUI_BarcodeScanner.Services
{
    public class Inventory
    {


        private readonly HttpClient _httpClient;
        private List<InventoryItem> _inventoryItems;
        readonly IConfiguration _config;
        private string apiUrl;

        public Inventory(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _inventoryItems = new List<InventoryItem>();
            _config = config;
            apiUrl = _config["ServerUrl"] + "/inventory";
        }

        public List<InventoryItem> GetAllItems()
        {
            return _inventoryItems;
        }


        public bool ItemExists(string skuId)
        {
            return _inventoryItems.Any(item => item.SKUId == skuId);
        }


        public async Task RefreshInventoryAsync()
        {
            _inventoryItems = await FetchInventoryFromApiAsync();
        }

        private async Task<List<InventoryItem>> FetchInventoryFromApiAsync()
        {
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var inventoryItems = await response.Content.ReadFromJsonAsync<List<InventoryItem>>();
            return inventoryItems;
        }

        public async Task<HttpResponseMessage> AddInventoryItemAsync(InventoryItem newItem)
        {
            var response = await _httpClient.PostAsJsonAsync(apiUrl, newItem);
            return response;
        }


    }
}
