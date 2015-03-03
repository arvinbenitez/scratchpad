using System.Collections.Generic;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;

namespace AguaDeMaria.Service.Implementation
{
    public class PaymentService: IPaymentService
    {
        private readonly IRepository<Receivable> receivableRepository;

        public PaymentService(IRepository<Receivable> receivableRepository )
        {
            this.receivableRepository = receivableRepository;
        }

        public IEnumerable<Receivable> GetReceivables(int customerId)
        {
            return receivableRepository.Get(x => x.CustomerId == customerId);
        }
    }
}
