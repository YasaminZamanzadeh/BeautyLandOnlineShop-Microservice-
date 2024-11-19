using BeautyLand.Application.Services.Site.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BeautyLand.SiteEndpoint.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;       
        }
        public IActionResult Index()
        {
            string userId = "1";
            var order = _orderService.GetOrders(userId).Result;
            return View(order); 
        }
        public IActionResult Detail(Guid id)
        {
            var orders = _orderService.GetOrderById(id);
            return View(orders);
        }

    }
}
