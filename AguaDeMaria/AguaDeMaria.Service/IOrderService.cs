using System.Linq;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;

namespace AguaDeMaria.Service
{
    public interface IOrderService
    {
        Order Get(int orderId);
        void Update(Order order);
    }

    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public Order Get(int orderId)
        {
            return orderRepository.Get(x => x.OrderId == orderId,
                includedProperties: "Customer,DeliveryReceipts,DeliveryReceipts.DeliveryReceiptDetails")
                .FirstOrDefault();
        }

        public void Update(Order order)
        {
            orderRepository.Update(order);
        }
    }
}