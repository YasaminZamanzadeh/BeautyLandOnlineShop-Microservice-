using BeautyLand.Domain.Attributes;
using System;
using System.Collections.Generic;

namespace BeautyLand.Domain.Baskets
{
    [AudiTable]
    public class Basket
    {
        public Basket(string userId)
        {
            UserId = userId;
        }
        public Basket()
        {
                
        }
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid? DiscountId { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
