using Transcard.Application.DTOs;
using Transcard.Application.Interfaces;
using Transcard.Domain.Entities;

namespace Transcard.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;

        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaymentResponseDto> SubmitAsync(PaymentRequestDto request)
        {
            var payment = new Payment(
                request.Amount,
                request.Currency,
                request.ReferenceId);

            await _repository.AddAsync(payment);

            return new PaymentResponseDto
            {
                PaymentId = payment.Id,
                Status = "Success"
            };
        }
    }
}