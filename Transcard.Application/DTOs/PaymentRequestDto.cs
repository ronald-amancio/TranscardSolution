using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcard.Application.DTOs
{
    public class PaymentRequestDto
    {
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string ReferenceId { get; set; } = string.Empty;
    }
}