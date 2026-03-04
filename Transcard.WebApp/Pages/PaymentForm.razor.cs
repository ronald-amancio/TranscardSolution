using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Linq.Expressions;
using Transcard.Application.DTOs;
using Transcard.Application.Interfaces;
using Transcard.WebApp.Services;

namespace Transcard.WebApp.Pages
{
    public partial class PaymentForm : ComponentBase
    {
        [Inject] private IJSRuntime JS { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IPaymentService PaymentService { get; set; } = default!;
        [Inject] private NotificationService NotificationService { get; set; } = default!;

        [SupplyParameterFromForm]
        private PaymentRequestDto _model { get; set; } = new();
        private bool _loading;
        private bool _submitted;

        private string? _toastMessage;
        private bool _toastSuccess;
        private bool _showSuccessAnimation;

        private readonly List<string> _currencies = new()
        {
            "USD", "EUR", "PHP", "GBP", "AUD"
        };

        private async Task SubmitAsync()
        {
            _loading = true;

            try
            {
                var result = await PaymentService.SubmitAsync(_model);
                _ = ShowToast("Your payment has been successfully posted.", true);

                _model = new();
                _submitted = false;
            }
            catch (UnauthorizedAccessException)
            {
                NotificationService.ShowError("Session expired.");
                Nav.NavigateTo("/login");
            }
            catch (Exception)
            {
                NotificationService.ShowError("Unable to process payment.");
            }
            finally
            {
                _loading = false;
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task ShowToast(string message, bool success)
        {
            _toastMessage = message;
            _toastSuccess = success;

            await InvokeAsync(StateHasChanged);

            await Task.Delay(3000);

            _toastMessage = null;

            await InvokeAsync(StateHasChanged);
        }

        private async Task HandleInvalidSubmit()
        {
            _submitted = true;

            NotificationService.ShowError("Please correct the highlighted fields.");
            StateHasChanged();
        }
    }
}