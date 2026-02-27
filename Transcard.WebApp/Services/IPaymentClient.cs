using Transcard.Application.DTOs;

namespace Transcard.WebApp.Services
{
    public interface IPaymentClient
    {
        Task<PaymentResponseDto> SubmitAsync(PaymentRequestDto request);
    }
}