using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcard.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        public string ReferenceId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        private Payment() { }

        public Payment(decimal amount, string currency, string referenceId)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required.");

            if (string.IsNullOrWhiteSpace(referenceId))
                throw new ArgumentException("Reference ID is required.");

            Amount = amount;
            Currency = currency;
            ReferenceId = referenceId;
        }
    }
}