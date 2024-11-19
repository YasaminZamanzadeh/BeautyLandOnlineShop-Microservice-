using BeautyLand.Application.Services.Site.Baskets.Basket.Dtos;
using BeautyLand.Application.Services.Site.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Baskets.Basket
{
    public interface IBasketService
    {
        BasketDto GetBasket(string userId);
        ResultDto CreateBasket(CreateBasketDto createBasket, string userId);
        ResultDto DeleteBasket(Guid itemId);
        ResultDto UpdateBasket(Guid basketId, int quantity);
       ResultDto ApplyDiscountBasket(Guid basketId, Guid discountId);
        ResultDto CheckoutBasket(CheckoutBasketDto checkoutBasket);

    }
}
