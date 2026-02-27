using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcard.Application.DTOs
{
    public class PaymentResponseDto
    {
        public Guid PaymentId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}