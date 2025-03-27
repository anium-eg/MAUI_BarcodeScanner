namespace MAUI_BarcodeScanner.Models
{
    public class CartItem
    {
        public string SKUId { get; set; }
        public string ProductName { get; set; }
        public int PricePerItem { get; set; }
        public int TotalPrice { get; set; }
        public int Quantity { get; set; } = 1;
    }
}