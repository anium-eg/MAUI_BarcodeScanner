using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MAUI_BarcodeScanner.Models;
using Microsoft.Extensions.Configuration;
using static System.Net.WebRequestMethods;

namespace MAUI_BarcodeScanner.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        readonly IConfiguration _config;
        private string apiUrl;

        public AuthService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            apiUrl = _config["ServerUrl"] + "/auth";
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var creds = new { Username = username, HashedPassword = password };
            var res = await _httpClient.PostAsJsonAsync($"{apiUrl}/login", creds);
            if (res.IsSuccessStatusCode)
                return true;
            return false;
        }

    }
}
