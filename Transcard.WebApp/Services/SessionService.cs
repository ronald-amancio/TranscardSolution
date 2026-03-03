using Microsoft.AspNetCore.Components;

namespace Transcard.WebApp.Services
{
    public class SessionService
    {
        private readonly NavigationManager _nav;
        private readonly NotificationService _notification;
        //[Inject] private SessionService SessionService { get; set; } = default!;

        public SessionService(NavigationManager nav, NotificationService notification)
        {
            _nav = nav;
            _notification = notification;
        }

        public async Task HandleIdleWarning()
        {
            _notification.ShowWarning("You will be logged out in 1 minute due to inactivity.");
        }

        public async Task HandleIdleLogout()
        {
            _notification.ShowError("Session expired due to inactivity.");

            _nav.NavigateTo("/auth/logout", forceLoad: true);
        }
    }
}