using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Transcard.WebApp.Services;

namespace Transcard.WebApp.Components.Layout
{
    public partial class MainLayout
    {
        [Inject] private IJSRuntime JS { get; set; } = default!;
        [Inject] private SessionService SessionService { get; set; } = default!;

        private DotNetObjectReference<MainLayout>? _objRef;
        private string? _toastMessage;
        private bool _toastSuccess;
        private bool _showSuccessAnimation;
        private bool _menuOpen;
        private bool _showSessionWarning;

        private void ToggleMenu()
        {
            _menuOpen = !_menuOpen;
        }

        private string MenuCss =>
                _menuOpen
                    ? "mobile-menu show"
                    : "mobile-menu";

        [JSInvokable]
        public Task TriggerIdleWarning()
        {
            _showSessionWarning = true;
            StateHasChanged();
            return Task.CompletedTask;
        }

        [JSInvokable]
        public async Task TriggerIdleLogout()
        {
            await SessionService.HandleIdleLogout();
        }

        private async Task ContinueSession()
        {
            _showSessionWarning = false;

            await JS.InvokeVoidAsync("restartIdleTimer");
        }

        private async Task LogoutNow()
        {
            _showSessionWarning = false;
            await SessionService.HandleIdleLogout();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //_objRef = DotNetObjectReference.Create(this);
                //await JS.InvokeVoidAsync("startIdleTimer", _objRef, 2);

                await JS.InvokeVoidAsync("startIdleTimer", DotNetObjectReference.Create(this), 2);
            }
        }

        //[JSInvokable]
        //public async Task TriggerIdleWarning()
        //{
        //    await SessionService.HandleIdleWarning();
        //}

        //[JSInvokable]
        //public async Task TriggerIdleLogout()
        //{
        //    ShowToast("You have been logged out.", true);
        //    await SessionService.HandleIdleLogout();
        //}

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

        private void ShowSuccessAnimation()
        {
            _showSuccessAnimation = true;

            Task.Delay(1500).ContinueWith(_ =>
            {
                _showSuccessAnimation = false;
                InvokeAsync(StateHasChanged);
            });
        }
    }
}