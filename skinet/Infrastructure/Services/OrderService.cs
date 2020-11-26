using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork; 

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
        }


        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Core.Entities.OrderAggregate.Address shippingAddress)
        {
            //213 - Get basket from repo...
            var basket = await _basketRepo.GetBasketAsync(basketId);

            //213 - Get items from product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            //213 - Get delivery method repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
             if (deliveryMethod == null) return null;

            //213 - calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            //213 - create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            //219. Add order to Unit of Work changes
            _unitOfWork.Repository<Order>().Add(order);

            //219 - Save to db
            var result = await _unitOfWork.Complete();

            if(result <= 0 ) return null;

            //delete basket
            await _basketRepo.DeleteBasketAsync(basketId);

            //213 - Return order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecifications(id, buyerEmail);
            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecifications(buyerEmail);
            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}