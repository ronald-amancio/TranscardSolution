using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transcard.Application.DTOs;

namespace Transcard.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> SubmitAsync(PaymentRequestDto request);
    }
}