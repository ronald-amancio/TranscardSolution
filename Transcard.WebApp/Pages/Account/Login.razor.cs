using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Transcard.Application.DTOs;
using Transcard.WebApp.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Transcard.WebApp.Pages.Account
{
    public partial class Login
    {
        [Inject] private LoginService LoginService { get; set; } = default!;
        [Inject] private NotificationService NotificationService { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [SupplyParameterFromForm]
        private LoginRequestDto _model { get; set; } = new();
        private string? _error;

        private string? _toastMessage;
        private bool _toastSuccess;

        //private async Task LoginAsync()
        //{

        //    //var response = await Http.PostAsJsonAsync("https://localhost:7058/api/auth/login", _model);
        //    var loginSuccess = await LoginService.LoginAsync(_model.Username, _model.Password);

        //    if (!loginSuccess)
        //    {
        //        _error = "Invalid credentials";
        //        return;
        //    }

        //    ShowToast("Successfully Logged in", false);
        //    Nav.NavigateTo($"/paymentform", forceLoad: true);
        //}

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity?.IsAuthenticated == true)
            {
                Nav.NavigateTo("/paymentform");
            }
        }

        private async Task LoginAsync()
        {
            var form = new Dictionary<string, string>
            {
                { "Username", _model.Username },
                { "Password", _model.Password }
            };

            var response = await Http.PostAsync("auth/login",
                new FormUrlEncodedContent(form));

            if (!response.IsSuccessStatusCode)
            {
                _error = "Invalid credentials";
                return;
            }

            Nav.NavigateTo("/paymentform", true);
        }

        private void ShowToast(string message, bool success)
        {
            _toastMessage = message;
            _toastSuccess = success;

            Task.Delay(3000).ContinueWith(_ =>
            {
                _toastMessage = null;
                InvokeAsync(StateHasChanged);
            });
        }
    }
}