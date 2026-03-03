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

        //[Parameter] public EventCallback<PaymentRequestDto> OnSaved { get; set; }

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
                NotificationService.ShowSuccess($"Your payment has been successfully posted. Payment ID: {result.PaymentId}");
                ShowToast($"Your payment has been successfully posted.", true);
                ShowSuccessAnimation();

                _model = new();
                _submitted = false;
                //StateHasChanged();
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
                StateHasChanged();
            }
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

        private void ShowSuccessAnimation()
        {
            _showSuccessAnimation = true;

            Task.Delay(1500).ContinueWith(_ =>
            {
                _showSuccessAnimation = false;
                InvokeAsync(StateHasChanged);
            });
        }

        private async Task HandleInvalidSubmit()
        {
            _submitted = true;

            NotificationService.ShowError("Please correct the highlighted fields.");
            StateHasChanged();
        }
    }
}