using Transcard.Application.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Transcard.WebApp.Components.Payments
{
    public partial class PaymentForm
    {
        private PaymentRequestDto _model = new();
        private bool _loading;
        private string? _error;
        private string? _success;

        private async Task SubmitAsync()
        {
            try
            {
                _loading = true;
                _error = null;
                _success = null;

                var result = await PaymentClient.SubmitAsync(_model);

                _success = $"Payment ID: {result.PaymentId}";
            }
            catch (UnauthorizedAccessException)
            {
                _error = "Session expired. Please login again.";
            }
            catch
            {
                _error = "Unable to process payment.";
            }
            finally
            {
                _loading = false;
            }
        }
    }
}