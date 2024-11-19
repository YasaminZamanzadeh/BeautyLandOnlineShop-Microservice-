using BeautyLand.Application.Services.Databases.Baskets;
using BeautyLand.Application.Services.Site.BasketItems.Dtos.BasketItems;
using BeautyLand.Application.Services.Site.Baskets.Dtos.Baskets;
using BeautyLand.Application.Services.Site.Discounts;
using BeautyLand.Application.Services.Site.Discounts.Dots;
using BeautyLand.Application.Services.Site.Dto;
using BeautyLand.Domain.Baskets;
using BeautyLand.Subscription.Messages.MessagesBus.Dtos.Publisher;
using BeautyLand.Subscription.Messages.MessagesBus.Publisher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;

namespace BeautyLand.Application.Services.Site.Baskets.Bskets
{
    public class BasketService : IBasketService
	{
		private readonly IBasketDatabaseService _basketContext;
		private readonly IMessagePublisher _messagePublisher;
		private readonly string _queueName;
		public BasketService(IBasketDatabaseService basketContext, IMessagePublisher messagePublisher, IOptions<RabbitMQBasketConfigurationDto> options)
		{
			_basketContext = basketContext;
			_messagePublisher = messagePublisher;
			_queueName = options.Value.BasketQueueName;
		}

		public void ApplyDiscountBasket(Guid basketId, Guid discountId)
		{
			var basket = _basketContext.Baskets.Find(basketId);
			if (basket == null)
			{
				throw new Exception("Basket not found");
			}
			basket.DiscountId = discountId;
			_basketContext.SaveChanges();
		}

		public ResultDto ChekoutBasket(BasketCheckoutDto checkout, IDiscountService discountService)
		{
			//Find basket
			var basket = _basketContext.Baskets
				.Include(p=> p.BasketItems)
				.ThenInclude(p=> p.Item)
				.SingleOrDefault(p => p.Id == checkout.Id);

			if(basket == null)
			{
				return new ResultDto
				{
					IsSuccess = false,
					Message = "Basket is not found"
				};
			}
			
			//Send message for order service
			var message = new BasketCheckoutMessageDto
			{
				Id = checkout.Id,
				UserId = checkout.UserId,
				Name = checkout.Name,
				PhoneNumber = checkout.PhoneNumber,
				Address = checkout.Address,
				PostalCode = checkout.PostalCode,
			};

			int totalPrice = 0;
			foreach (var item in basket.BasketItems)
			{
				var basketItem = new BasketItemMessageDto
				{
					Id = item.Id,	
					ItemId = item.ItemId,	
					Name = item.Item.Name,
					Price = item.Item.Price,	
					Quantity = item.Quantity
				};
				totalPrice += item.Item.Price * item.Quantity;
				message.BasketItems.Add(basketItem);
            }

            //Find discount form discount microservice
            DiscountDto discount = null;

            if (basket.DiscountId.HasValue)
                discount = discountService.GetDiscountById(basket.DiscountId.Value);
            
			if (discount != null)
			{
				message.TotalPrice = totalPrice - discount.Amount;
			}
			else
			{
				message.TotalPrice = totalPrice;	
			}

			_messagePublisher.Publish(message, _queueName);	

            //Delete basket
            _basketContext.Baskets.Remove(basket);
			_basketContext.SaveChanges();
			return new ResultDto
			{

				IsSuccess = true,
				Message = "Successed"
			};

		}

		public BasketDto GetBasket(string userId)
		{
			var basket = _basketContext.Baskets
			  .Include(p => p.BasketItems)
			  .ThenInclude(p => p.Item)
			  .SingleOrDefault(p => p.UserId == userId);

			if (basket == null)
			{
				return null;
			}
			return new BasketDto
			{
				Id = basket.Id,
				UserId = basket.UserId,
				BasketItems = basket.BasketItems
			  .Select(p => new BasketItemDto
			  {
				  Id = p.Id,
				  ItemId = p.Item.ItemId,
				  Name = p.Item.Name,
				  Image = p.Item.Image,
				  Price = p.Item.Price,
				  Quantity = p.Quantity,
			  }).ToList()
			};
		}

		public BasketDto GetOrCreateBasket(string userId)
		{
			var basket = _basketContext.Baskets
				.Include(p => p.BasketItems)
				.ThenInclude(p => p.Item)
				.SingleOrDefault(p => p.UserId == userId);

			if (basket == null)
			{
				return CreateBasket(userId);
			}
			return new BasketDto
			{
				Id = basket.Id,
				UserId = basket.UserId,
				DiscountId = basket.DiscountId,
				BasketItems = basket.BasketItems
				.Select(p => new BasketItemDto
				{
					Id = p.Id,
					ItemId = p.Item.ItemId,
					Name = p.Item.Name,
					Image = p.Item.Image,
					Price = p.Item.Price,
					Quantity = p.Quantity,
				}).ToList()
			};
		}
		private BasketDto CreateBasket(string userId)
		{
			Basket basket = new Basket
			{
				UserId = userId,
			};
			_basketContext.Baskets.Add(basket);
			_basketContext.SaveChanges();
			return new BasketDto
			{
				Id = basket.Id,
				UserId = basket.UserId
			};
		}
	}

}
