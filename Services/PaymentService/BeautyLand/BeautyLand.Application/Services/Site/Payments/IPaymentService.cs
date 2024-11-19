using BeautyLand.Application.Services.Site.Payments.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Payments
{
    public interface IPaymentService
    {
        PaymentDto GetPaymentOrderByOrderId(Guid orderId);
        PaymentDto GetPaymentOrderById(Guid id);
        bool CreatePayment(Guid orderId, int amount);
        void Paid(Guid id, string authority, long refId);
    }
}
   