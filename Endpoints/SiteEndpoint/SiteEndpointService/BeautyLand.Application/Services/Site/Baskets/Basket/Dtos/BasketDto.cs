using BeautyLand.Application.Services.Site.Baskets.BasketItem.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Baskets.Basket.Dtos
{
	public class BasketDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public Guid? DiscountId { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public DiscountBasketDto DiscountBasket { get; set; } = null;
        public int TotalPrice()
        {
            if (!BasketItems.Any())
            {
                return 0;
            }
            else
            {
				var totalPrice = BasketItems.Sum(p => p.Quantity * p.Price);

                if (DiscountId.HasValue)
                {
                    totalPrice = totalPrice - DiscountBasket.Amount;
                }

                return totalPrice;
			}
        }
    }
}
