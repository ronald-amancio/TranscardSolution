using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transcard.Domain.Entities;

namespace Transcard.Application.Interfaces
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
    }
}
