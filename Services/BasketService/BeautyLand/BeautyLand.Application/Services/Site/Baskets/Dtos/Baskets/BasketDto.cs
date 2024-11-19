using System;
using System.Collections.Generic;
using System.Linq;
using BeautyLand.Application.Services.Site.BasketItems.Dtos.BasketItems;

namespace BeautyLand.Application.Services.Site.Baskets.Dtos.Baskets
{
    public class BasketDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid? DiscountId { get; set; }
        public IEnumerable<BasketItemDto> BasketItems { get; set; }
        public int TotalPrice()
        {
            if (BasketItems.Any())
            {
                int total = BasketItems.Sum(p => p.Price * p.Quantity);
                return total;
            }
            return 0;
        }
    }

}
