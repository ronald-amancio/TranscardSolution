using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Linq.Expressions;
using Transcard.Application.DTOs;

namespace Transcard.WebApp.Pages
{
    public partial class PaymentForm
    {
        [Inject] private IJSRuntime JS { get; set; } = default!;

        private PaymentRequestDto _model = new();
        private EditContext _editContext = default!;
        private bool _loading;
        private string? _toastMessage;
        private bool _toastSuccess;
        //private bool _showSuccessAnimation;
        private bool _submitted;

        private List<string> _currencies = new()
        {
            "USD",
            "EUR",
            "PHP",
            "GBP",
            "AUD"
        };

        protected override void OnInitialized()
        {
            _editContext = new EditContext(_model);
        }

        private async Task SubmitAsync()
        {
            try
            {
                _loading = true;

                var result = await PaymentClient.SubmitAsync(_model);

                ShowToast("Payment successful", true);
                //ShowSuccessAnimation();

                _model = new();
                _editContext = new EditContext(_model);
            }
            catch (UnauthorizedAccessException)
            {
                ShowToast("Session expired. Please login again.", false);
            }
            catch
            {
                ShowToast("Unable to process payment.", false);
            }
            finally
            {
                _loading = false;
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

        //private void ShowSuccessAnimation()
        //{
        //    _showSuccessAnimation = true;

        //    Task.Delay(1500).ContinueWith(_ =>
        //    {
        //        _showSuccessAnimation = false;
        //        InvokeAsync(StateHasChanged);
        //    });
        //}

        private async Task FocusFirstInvalidField()
        {
            await JS.InvokeVoidAsync("focusFirstInvalid");
        }

        private async void HandleInvalidSubmit(EditContext context)
        {
            _submitted = true;

            ShowToast("Please correct the highlighted fields.", false);

            await FocusFirstInvalidField();
        }

        private string FieldCss(Expression<Func<object>> accessor)
        {
            if (!_submitted)
                return string.Empty;

            var field = FieldIdentifier.Create(accessor);

            return _editContext.GetValidationMessages(field).Any()
                ? "is-invalid"
                : "is-valid";
        }
    }
}