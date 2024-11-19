using System;

namespace BeautyLand.Application.Services.Site.Orders.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public int TotalPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsPaid { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }

}
