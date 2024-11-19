using BeautyLand.Application.Services.Site.Orders;
using BeautyLand.Application.Services.Site.Orders.Dtos;
using BeautyLand.Application.Services.Site.Payments;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BeautyLand.SiteEndpoint.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentsService _paymentsService;
        private readonly IOrderService _orderService;
        public PaymentController(IPaymentsService paymentsService, IOrderService orderService)
        {
                _paymentsService = paymentsService;
            _orderService = orderService;
        }
        public IActionResult Pay(Guid orderId, string callingBackUrl)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order.PaymentStatus == PaymentStatus.Paid)
            {
                ViewBag["message"] = "Success";
            }
            if (order.PaymentStatus == PaymentStatus.UnPaid)
            {
                var payments = _orderService.RequestPayment(orderId);
            }
            var calledBackUrl = Url.Action
                (
                "Detail",
                "Payment",
                new { id = orderId },
                protocol: Request.Scheme
                );

            var payment = _paymentsService.GetPayment(orderId, callingBackUrl);
            if (payment.IsSuccess)
            {
                return Redirect(payment.Model.Link);
            }
          else
            {
                return NotFound();
            }
        }
    }
}
