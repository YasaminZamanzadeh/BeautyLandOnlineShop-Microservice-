using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyLand.Application.Services.Site.Dtos;
using BeautyLand.Application.Services.Site.Orders.Dtos;

namespace BeautyLand.Application.Services.Site.Orders
{
    public interface IOrderService
    {
        OrderDetailDto GetOrderById(Guid id);
        Task<List<OrderDto>> GetOrders(string userId);
        ResultDto RequestPayment(Guid orderId);
    }
}
