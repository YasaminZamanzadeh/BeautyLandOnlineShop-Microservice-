using BeautyLand.Application.Services.Databases.Baskets;
using BeautyLand.Application.Services.Site.BasketItems.Dtos.BasketItems;
using BeautyLand.Application.Services.Site.BasketItems.Dtos.Items;
using BeautyLand.Domain.Baskets;
using BeautyLand.Domain.Catalogs.Item;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BeautyLand.Application.Services.Site.BasketItems.BasketItems
{
    public class BasketItemService : IBasketItemService
    {
        private readonly IBasketDatabaseService _basketContext;
        public BasketItemService(IBasketDatabaseService basketItemContext)
        {
            _basketContext = basketItemContext;
        }

    

        public void CreateBasketItem(CreateBasketItemDto createBasket)
        {

            var basket = _basketContext.Baskets
                .FirstOrDefault(p => p.Id == createBasket.BasketId);

            if (basket == null)
            {
                throw new Exception("Basket is not found");
            }

            BasketItem basketItem = new BasketItem
            {
                Id = createBasket.Id,
                ItemId = createBasket.ItemId,
                BasketId = createBasket.BasketId,
                Quantity = createBasket.Quantity,
            };

            ItemDto item = new ItemDto
            {
                ItemId = createBasket.ItemId,
                Name = createBasket.Name,
                Price = createBasket.Price,
                Image = createBasket.Image,
            };
             CreateItem(item);
            _basketContext.BasketItems.Add(basketItem);
            _basketContext.SaveChanges();
        }

        public void DeleteBaskeItem(Guid itemId)
        {
            var basketItem = _basketContext.BasketItems
                 .SingleOrDefault(p => p.ItemId == itemId);

            if (basketItem == null)
            {
                throw new Exception("Item is not found");
            }

            _basketContext.BasketItems.Remove(basketItem);
            _basketContext.SaveChanges();

        }

        public void SetQuantities(Guid id, int quantity)
        {
            var basketItem = _basketContext.BasketItems
                .FirstOrDefault(p => p.Id == id);

            if (basketItem == null)
            {
                throw new Exception("Item is not found");
            }

            basketItem.SetQuantity(quantity);
            _basketContext.SaveChanges();
        }

        public void Transferasket(string anonymousId, string userId)
        {
            var anonymousBasket = _basketContext.Baskets
                .Include(p => p.BasketItems)
                .SingleOrDefault(p => p.UserId == anonymousId);

            if (anonymousBasket == null) return;

            var userBasket = _basketContext.Baskets
                .SingleOrDefault(p => p.UserId == userId);

            if (userBasket == null)
            {
                userBasket = new Basket(userId);
                _basketContext.Baskets.Add(userBasket);
            }

            foreach (var item in anonymousBasket.BasketItems)
            {
                userBasket.BasketItems.Add(new BasketItem
                {
                    Id = item.Id,
                    Quantity = item.Quantity
                });

                _basketContext.Baskets.Remove(anonymousBasket);
                _basketContext.SaveChanges();
            }
        }
        private ItemDto GetItem(Guid itemId)
        {
            var item = _basketContext.Items
                .SingleOrDefault(p => p.ItemId == itemId);

            if (item != null)
            {
                return new ItemDto
                {
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Price = item.Price,
                    Image = item.Image,
                };
            }
                return null;
        }

        private ItemDto CreateItem(ItemDto item)
        {
            var itemId = GetItem(item.ItemId);
            if (itemId != null)
            {
                return itemId;
            }
            else
            {
                Item newItem = new Item
                {
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Price = item.Price,
                    Image = item.Image,
                };

                _basketContext.Items.Add(newItem);
                _basketContext.SaveChanges();

                return new ItemDto
                {
                    ItemId = newItem.ItemId,
                    Name = newItem.Name,
                    Price = newItem.Price,
                    Image = newItem.Image,
                }; 
            }
        }

    }
}
