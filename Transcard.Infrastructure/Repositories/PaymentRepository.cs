using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transcard.Application.Interfaces;
using Transcard.Domain.Entities;

namespace Transcard.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public Task AddAsync(Payment payment)
        {
            // Simulate persistence
            return Task.CompletedTask;
        }
    }
}