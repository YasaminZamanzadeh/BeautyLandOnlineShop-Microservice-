using System;

namespace BeautyLand.Application.Services.Site.Discounts.Dtos
{
    public class DiscountDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }
        public bool IsUsed { get; set; }
    }
}
