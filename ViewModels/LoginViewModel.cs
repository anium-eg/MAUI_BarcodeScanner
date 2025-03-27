
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_BarcodeScanner.Models;
using MAUI_BarcodeScanner.Views;


namespace MAUI_BarcodeScanner.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        private readonly HttpClient httpClient = new HttpClient();
        public LoginViewModel()
        {
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
                //Mocking server authentication request
                HttpResponseMessage response = await httpClient.GetAsync("https://google.com");

                if(loginData.CashierId == "cash001" && loginData.Password == "password")
                {
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
