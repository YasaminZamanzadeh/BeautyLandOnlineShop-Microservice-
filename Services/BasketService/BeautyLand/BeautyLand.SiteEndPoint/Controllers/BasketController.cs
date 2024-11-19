using BeautyLand.Application.Services.Site.BasketItems.BasketItems;
using BeautyLand.Application.Services.Site.BasketItems.Dtos.BasketItems;
using BeautyLand.Application.Services.Site.Baskets.Bskets;
using BeautyLand.Application.Services.Site.Baskets.Dtos.Baskets;
using BeautyLand.Application.Services.Site.Discounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BeautyLand.SiteEndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IBasketItemService _basketItemService;
        public BasketController(IBasketService basketService, IBasketItemService basketItemService)
        {
            _basketService = basketService;
            _basketItemService = basketItemService;
        }

        [HttpGet]
        public IActionResult Get(string userId)
        {
            var basket = _basketService.GetOrCreateBasket(userId);
            return Ok(basket);
        }

        [HttpPost]
        public IActionResult Post(CreateBasketItemDto createBasketItem, string userId)
        {
            var basket = _basketService.GetOrCreateBasket(userId);
            createBasketItem.BasketId = basket.Id;
            _basketItemService.CreateBasketItem(createBasketItem);
            _basketService.GetBasket(userId);
            return Ok();
        }
        [HttpPost("CheckoutBasket")]
        public IActionResult Post(BasketCheckoutDto checkoutBasket, [FromServices] IDiscountService discountService)
        {
           var model =  _basketService.ChekoutBasket(checkoutBasket, discountService);
            if (model.IsSuccess)
            {
                return Ok(model);
            }
            else
                return StatusCode(500, model);
        }
        [HttpDelete]
        public IActionResult Delete(Guid itemId)
        {
            _basketItemService.DeleteBaskeItem(itemId);
            return Ok();
        }
        [HttpPut]
        public IActionResult Put(Guid id, int quantity)
        {
            _basketItemService.SetQuantities(id, quantity);
            return Ok();
        }

        [HttpPut("{basketId}/{discountId}")]
        public IActionResult Put(Guid basketId, Guid discountId)
        {
            _basketService.ApplyDiscountBasket(basketId, discountId);
            return Accepted();
        }
    }
}
