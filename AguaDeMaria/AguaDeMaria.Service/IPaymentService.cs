using System.Collections.Generic;
using AguaDeMaria.Model;

namespace AguaDeMaria.Service
{
    public interface IPaymentService
    {
        IEnumerable<Receivable> GetReceivables(int customerId);
    }
}