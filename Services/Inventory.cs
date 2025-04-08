using System;
using System.Collections.Generic;
using System.Text;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;

namespace MAUI_BarcodeScanner.Services
{
    public class Inventory
    {
        public List<InventoryItem> Items = new List<InventoryItem>
        {
            new InventoryItem { SKUId = "705632441947", ProductName = "Cycling Gloves", Price=30, Stock=5 },
            new InventoryItem { SKUId = "8906004863080", ProductName = "Origami Tissues", Price=7, Stock=5 },
            new InventoryItem { SKUId = "4987176191359", ProductName = "Vicks Inhaler", Price=5, Stock=5 },
            new InventoryItem {SKUId = "194632852486", ProductName = "Lenovo Backpack", Price = 70, Stock = 5},
            new InventoryItem {SKUId = "01234567890128", ProductName = "Cycling Jersey", Price = 60, Stock = 5},
        };
    }
}
