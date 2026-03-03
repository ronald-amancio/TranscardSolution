using Microsoft.AspNetCore.Components;

namespace Transcard.WebApp.Shared
{
    public partial class ConfirmDialog : ComponentBase
    {
        private TaskCompletionSource<bool>? _tcs;

        public bool IsVisible { get; set; }
        public string Title { get; set; } = "Confirm";
        public string Message { get; set; } = "Are you sure?";

        public async Task<bool> Show(string title, string message)
        {
            Title = title;
            Message = message;
            IsVisible = true;
            StateHasChanged();

            _tcs = new TaskCompletionSource<bool>();
            return await _tcs.Task;
        }

        private void Confirm()
        {
            _tcs?.SetResult(true);
            Hide();
        }

        private void Cancel()
        {
            _tcs?.SetResult(false);
            Hide();
        }

        private void Hide()
        {
            IsVisible = false;
            StateHasChanged();
        }

        private void HandleBackdropClick()
        {
            Cancel(); // Clicking outside the modal will cancel
        }
    }
}