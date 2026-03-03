namespace Transcard.WebApp.Services
{
    public class NotificationService
    {
        public event Action<string, string>? OnNotify;

        public void ShowSuccess(string message) => OnNotify?.Invoke(message, "success");
        public void ShowWarning(string message) => OnNotify?.Invoke(message, "warning");
        public void ShowError(string message) => OnNotify?.Invoke(message, "danger");
    }
}