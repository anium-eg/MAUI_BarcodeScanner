
using System.Windows.Input;

using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;
using MAUI_BarcodeScanner.Views;

namespace MAUI_BarcodeScanner.ViewModels
{
    public class ScannerViewModel : BaseViewModel
    {
        private readonly IDataStore<Item> dataStore;
        private readonly Inventory inventory;

        public ScannerViewModel()
        {
            Title = "Item Scanner";
            OpenScannerCommand = new Command(OpenScannerFunction);
            LogoutCommand = new Command(Logout);
            dataStore = DependencyService.Get<IDataStore<Item>>();
            inventory = DependencyService.Get<Inventory>();

        }
        public ICommand OpenScannerCommand { get; }
        public ICommand LogoutCommand { get; }
        public async void OpenScannerFunction()
        {
             await Application.Current.MainPage.Navigation.PushAsync(new ScannerCameraView());
        }

        public async void Logout()
        {
            Preferences.Set("isLoggedIn", false);
            await Shell.Current.GoToAsync("//LoginPage");
        }


    }
}