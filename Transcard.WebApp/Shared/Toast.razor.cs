using Microsoft.AspNetCore.Components;
using Transcard.WebApp.Services;

namespace Transcard.WebApp.Shared
{
    public partial class Toast : ComponentBase, IDisposable
    {
        [Inject] NotificationService NotificationService { get; set; } = default!;

        private bool IsVisible;
        private string Message = string.Empty;
        private string ToastType = "success";
        private CancellationTokenSource? _cts;

        protected override void OnInitialized()
        {
            NotificationService.OnNotify += Show;
        }

        private async void Show(string message, string type)
        {
            Message = message;
            ToastType = type;
            IsVisible = true;
            StateHasChanged();

            _ = Task.Delay(3000).ContinueWith(_ =>
            {
                IsVisible = false;
                InvokeAsync(StateHasChanged);
            });
        }

        private void Hide()
        {
            _cts?.Cancel();
            IsVisible = false;
        }

        public void Dispose()
        {
            NotificationService.OnNotify -= Show;
            _cts?.Cancel();
        }
    }
}