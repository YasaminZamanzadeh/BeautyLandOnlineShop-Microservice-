using System;
using System.Collections.Generic;

namespace BeautyLand.Application.Services.Site.Orders.Dtos
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public bool IsPaid { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int TotalPrice { get; set; }
        public List<OrderItemDetailDto> OrderItems { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

    }
}
