using System;
using System.Collections.Generic;
using System.Text;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;

namespace MAUI_BarcodeScanner.Services
{
    class Inventory
    {
        public List<InventoryItem> Items = new List<InventoryItem>
        {
            new InventoryItem { SKUId = "0705632441947", ProductName = "Cycling Gloves" },
            new InventoryItem { SKUId = "8906004863080", ProductName = "Origami Tissues" },
            new InventoryItem { SKUId = "4987176191359", ProductName = "Vicks Inhaler" },
            new InventoryItem { SKUId = "194632852486", ProductName = "Lenovo Backpack" },
            new InventoryItem { SKUId = "1234567890129", ProductName = "Cycling Jersey" },
        };
    }
}
