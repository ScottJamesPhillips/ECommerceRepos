using System;
using System.Linq.Expressions;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderByPaymentIntentIdSpecification : BaseSpecfication<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId) : 
        base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}