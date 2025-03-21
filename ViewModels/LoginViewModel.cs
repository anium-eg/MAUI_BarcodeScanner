
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using MAUI_BarcodeScanner.Views;


namespace MAUI_BarcodeScanner.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private static readonly HttpClient httpClient = new HttpClient();
        public Command LoginCommand { get; }

        private string _cashierId;
        public string CashierId
        {
            get => _cashierId;
            set
            {
                if (_cashierId != value)
                {
                    _cashierId = value;
                    OnPropertyChanged(nameof(CashierId));
                }
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private bool _showInvalidText;
        public bool ShowInvalidText
        {
            get => _showInvalidText;
            set
            {
                if (_showInvalidText != value)
                {
                    SetProperty(ref _showInvalidText, value);
                    OnPropertyChanged(nameof(ShowInvalidText));
                }
            }
        }


        private string _invalidText = "No error yet";
        public string InvalidText
        {
            get => _invalidText;
            set
            {
                if (_invalidText != value)
                {
                    SetProperty(ref _invalidText, value);
                    OnPropertyChanged(nameof(InvalidText));
                }
            }
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    SetProperty(ref _isLoading, value);
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }


        public LoginViewModel()
        {
            bool isLoggedIn = Preferences.Get("isLoggedIn", false);

            if(isLoggedIn)
                Shell.Current.GoToAsync($"//{nameof(ScannerPage)}");


            LoginCommand = new Command(OnLoginClicked);
            
        }


        private async void OnLoginClicked(object obj)
        {
            var loginData = new
            {
                Username = this.CashierId,
                Password = this.Password
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(loginData),
                Encoding.UTF8,
                "application/json");

            try
            {
                IsLoading = true;
                HttpResponseMessage response = await httpClient.GetAsync("https://google.com");

               // HttpResponseMessage response = await httpClient.PostAsync("http://10.0.2.2:5142", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    if(CashierId == "cash001" && Password == "password")
                    {
                        Preferences.Set("isLoggedIn", true);
                        await Shell.Current.GoToAsync($"//{nameof(ScannerPage)}");
                        Debug.WriteLine("Logged in successfully.");
                    }

                    else
                    {
                        ShowInvalidText = true;
                        InvalidText = "Invalid credentials. Try again";
                    }

                }
                else
                {
                    ShowInvalidText = true;
                    Debug.WriteLine($"Login failed with status code: {(int)response.StatusCode} ({response.ReasonPhrase})");
                }
            }
            catch (Exception ex)
            {
                ShowInvalidText = true;
                InvalidText = "Connection error. Try again.";
                Debug.WriteLine($"An error occurred during login: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual async void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));    
        }
    }
}
