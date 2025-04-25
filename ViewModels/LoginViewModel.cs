
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Services;
using MAUI_BarcodeScanner.Views;


namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        private readonly HttpClient httpClient = new HttpClient();
        readonly AuthService _authService;
        public LoginViewModel(AuthService authService)
        {
            _authService = authService;

            bool isLoggedIn = Preferences.Get("isLoggedIn", false);

            if (isLoggedIn)
                Shell.Current.GoToAsync($"//{nameof(ScannerPage)}");
        }

        [ObservableProperty]
        public string cashierId;

        [ObservableProperty]
        public string password;

        [ObservableProperty]
        private string invalidText;

        [ObservableProperty]
        private bool showInvalidText;

        [ObservableProperty]
        private bool isLoading;

        [RelayCommand]
        private async Task Login()
        {
            IsLoading = true;

            LoginCredential loginData = new LoginCredential
            {
                CashierId = this.CashierId,
                Password = this.Password
            };

            StringContent jsonContent = new StringContent(
                JsonSerializer.Serialize(loginData),
                Encoding.UTF8,
                "application/json");

            try
            {

                if (await _authService.LoginAsync(CashierId, Password))
                {
                    CashierId = "";
                    Password = "";
                    ShowInvalidText = false;

                    Preferences.Set("isLoggedIn", true);
                    await Shell.Current.GoToAsync($"//{nameof(ScannerPage)}");
                }

                else
                {
                    ShowInvalidText = true;
                    InvalidText = "Invalid credentials. Try again";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                ShowInvalidText = true;
                InvalidText = "Connection error. Try again.";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
