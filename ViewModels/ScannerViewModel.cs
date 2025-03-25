
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_BarcodeScanner.Views;


namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class ScannerViewModel : ObservableObject
    {
        public ScannerViewModel()
        {

        }

        [RelayCommand]
        private async Task OpenScanner()
        {
            await Shell.Current.GoToAsync("///"+nameof(ScannerCameraView));
        }

        [RelayCommand]
        public async Task Logout()
        {
            Preferences.Set("isLoggedIn", false);
            await Shell.Current.GoToAsync("///"+nameof(LoginPage));
        }


    }
}