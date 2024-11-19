using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeautyLand.Application.Services.Site.BasketItems.Dtos.BasketItems;

namespace BeautyLand.Application.Services.Site.BasketItems.BasketItems
{
    public interface IBasketItemService
    {
        void CreateBasketItem(CreateBasketItemDto createBasket);
        void DeleteBaskeItem(Guid itemId);
        void SetQuantities(Guid id, int quantity);
        void Transferasket(string anonymousId, string userId);
    }
}
