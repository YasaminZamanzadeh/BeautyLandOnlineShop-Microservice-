using BeautyLand.Application.Services.Databases.Catalogs;
using BeautyLand.Application.Services.Site.Payments.Dtos;
using BeautyLand.Domain.Order;
using BeautyLand.Domain.Payment;
using System;
using System.Linq;

namespace BeautyLand.Application.Services.Site.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDatabaseService _paymentContext;
        public PaymentService(IPaymentDatabaseService paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public bool CreatePayment(Guid orderId, int amount)
        {
            var order = GetOrder(orderId, amount);
            var payment = _paymentContext.Payments
                .SingleOrDefault(p => p.OrderId == order.Id);

            if (payment != null)
            {
                return true;
            }
            else
            {
                Payment newPayment = new Payment
                {
                    Id = Guid.NewGuid(),
                    Amount = amount,
                    IsPaid = false,
                    Order = order,
                    OrderId = order.Id,
                };
                _paymentContext.Payments.Add(newPayment);
                _paymentContext.SaveChanges();
                return true;
            }
        }

        private Order GetOrder(Guid orderId, int amount)
        {
            var order = _paymentContext.Orders
                .SingleOrDefault(p => p.Id == orderId);
            if (order != null)
            {
                if (order.Amount != amount)
                {
                    order.Amount = amount;
                    _paymentContext.SaveChanges();
                }
                return order;
            }
            else
            {
                Order newOrder = new Order
                {
                    Id = orderId,
                    Amount = amount
                };
                _paymentContext.Orders.Add(newOrder);
                _paymentContext.SaveChanges();
                return newOrder;
            }

        }

        public PaymentDto GetPaymentOrderById(Guid id)
        {
            var payment = _paymentContext.Payments
                 .SingleOrDefault(p => p.Id == id);

            if (payment != null)
            {
                return new PaymentDto
                {
                    Id = payment.Id,
                    OrderId = payment.OrderId,
                    Amount = payment.Amount,
                    IsPaid = payment.IsPaid,
                };
            }

            else
                return null;
        }

        public PaymentDto GetPaymentOrderByOrderId(Guid orderId)
        {
            var payment = _paymentContext.Payments
                 .SingleOrDefault(p => p.OrderId == orderId);

            if (payment != null)
                return new PaymentDto
                {
                    Id = payment.Id,
                    OrderId = payment.OrderId,
                    Amount = payment.Amount,
                    IsPaid = payment.IsPaid,
                };
            else
                return null;
        }

        public void Paid(Guid id, string authority, long refId)
        {
            var payment = _paymentContext.Payments
                 .SingleOrDefault(p => p.Id == id);
            payment.IsPaid = true;  
            payment.Authority = authority;
            payment.RefId = refId;
            _paymentContext.SaveChanges();
        }
    }
}
   