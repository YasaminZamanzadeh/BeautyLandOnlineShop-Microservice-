using System;

namespace BeautyLand.Application.Services.Site.Orders.Dtos
{
    public class OrderItemDetailDto
    {
        public Guid Id { get; set; }
        public string Consignment { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

    }
}
