using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUI_BarcodeScanner.Models
{
    public partial class CartItem : ObservableObject
    {
        [ObservableProperty]
        public string sKUId;

        [ObservableProperty]
        public string productName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalPrice))]
        public int pricePerItem;

        [ObservableProperty]
        public int mRP;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalPrice))]
        public int quantity = 1;

        public int TotalPrice => PricePerItem * Quantity;


    }
}