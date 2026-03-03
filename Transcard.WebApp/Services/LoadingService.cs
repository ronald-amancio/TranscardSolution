namespace Transcard.WebApp.Services
{
    public class LoadingService
    {
        public event Action? OnLoadingChanged;
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnLoadingChanged?.Invoke();
                }
            }
        }

        public void Show() => IsLoading = true;
        public void Hide() => IsLoading = false;
    }
}
