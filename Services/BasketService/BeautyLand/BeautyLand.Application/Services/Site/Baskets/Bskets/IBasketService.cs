using BeautyLand.Application.Services.Site.Baskets.Dtos.Baskets;
using BeautyLand.Application.Services.Site.Discounts;
using BeautyLand.Application.Services.Site.Dto;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Baskets.Bskets
{
    public interface IBasketService
    {
        BasketDto GetOrCreateBasket(string userId);
        BasketDto GetBasket(string userId);
        void ApplyDiscountBasket(Guid basketId, Guid discountId);
        ResultDto ChekoutBasket(BasketCheckoutDto checkout, IDiscountService discountService);
    }

}
