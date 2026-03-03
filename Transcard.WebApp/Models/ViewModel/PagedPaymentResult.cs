using Transcard.Application.DTOs;
using Transcard.Domain.Entities;

namespace Transcard.WebApp.Models.ViewModel
{
    public class PagedPaymentResult
    {
        public List<PaymentRequestDto> Payment { get; set; } = new();
        public int TotalCount { get; set; }
    }
}