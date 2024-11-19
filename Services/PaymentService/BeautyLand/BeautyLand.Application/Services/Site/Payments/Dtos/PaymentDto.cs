using System;

namespace BeautyLand.Application.Services.Site.Payments.Dtos
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
        public bool IsPaid { get; set; }
    }
}
