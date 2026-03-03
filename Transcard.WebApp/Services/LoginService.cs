using Microsoft.AspNetCore.Components;
using Transcard.Application.DTOs;
using Transcard.WebApp.Models.SharedModel;
using Transcard.WebApp.Models.ViewModel;
using static System.Net.WebRequestMethods;

namespace Transcard.WebApp.Services
{
    public class LoginService
    {
        private readonly HttpClient _http;
        private readonly LoadingService _loading;

        public LoginService(HttpClient http, LoadingService loading, NavigationManager nav)
        {
            _http = http;
            _loading = loading;
            _http.BaseAddress = new Uri(nav.BaseUri);
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var loginDetails = new LoginRequestDto
            {
                Username = username,
                Password = password
            };

            try
            {
                _loading.Show();
                //var response = await _http.PostAsJsonAsync("https://localhost:7058/api/auth/login", loginDetails);
                var response = await _http.PostAsJsonAsync("auth/login", loginDetails);

                return response.IsSuccessStatusCode;
            }
            finally
            {
                _loading.Hide();
            }
        }

        public async Task<PagedPaymentResult> GetTasksAsync(int page = 1, int pageSize = 10)
        {
            try
            {
                _loading.Show();
                var response = await _http.GetAsync($"api/Payment?page={page}&pageSize={pageSize}");
                response.EnsureSuccessStatusCode();

                var payments = await response.Content.ReadFromJsonAsync<List<PaymentRequestDto>>() ?? new();

                int totalCount = 0;
                if (response.Headers.TryGetValues("X-Total-Count", out var countValues))
                {
                    int.TryParse(countValues.FirstOrDefault(), out totalCount);
                }

                return new PagedPaymentResult
                {
                    Payment = payments,
                    TotalCount = totalCount
                };
            }
            finally
            {
                _loading.Hide();
            }
        }
    }
}