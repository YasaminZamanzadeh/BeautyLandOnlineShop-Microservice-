using BeautyLand.Application.Services.Site.Baskets.Basket;
using BeautyLand.Application.Services.Site.Baskets.Basket.Dtos;
using BeautyLand.Application.Services.Site.Catalogs.Item;
using BeautyLand.Application.Services.Site.Discounts;
using BeautyLand.Application.Services.Site.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace BeautyLand.SiteEndpoint.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IItemService _itemService;
        private readonly IDiscountService _discountService;
        private readonly string _userId = "1";
        public BasketController(IBasketService basketService, IItemService itemService, IDiscountService discountService)
        {
            _basketService = basketService;
            _itemService = itemService;
            _discountService = discountService;
        }
        public IActionResult Index()
        {
            var basket = _basketService.GetBasket(_userId);
            if (basket.DiscountId.HasValue)
            {
                var discount = _discountService.GetDiscountById(basket.DiscountId.Value);
                basket.DiscountBasket = new DiscountBasketDto
                {
                    Amount = discount.Model.Amount,
                    Code = discount.Model.Code,
                };
            }
            return View(basket);
        }
        public IActionResult Create(Guid id)
        {
            var item = _itemService.GetItemById(id);
            var basket = _basketService.GetBasket(_userId);
            CreateBasketDto createBasket = new CreateBasketDto
            {
                BasketId = basket.Id,
                ItemId = item.Id,
                Name = item.Name,
                Image = item.Image,
                Price = item.Price,
                Quantity = 1
            };
            _basketService.CreateBasket(createBasket, _userId);
            return RedirectToAction("Index");

        }

        public IActionResult Edit(Guid id, int quantity)
        {
            _basketService.UpdateBasket(id, quantity);
            return RedirectToAction("Index");

        }

        public IActionResult Delete(Guid itemId)
        {
            _basketService.DeleteBasket(itemId);
            return RedirectToAction("Index");
        }

        public IActionResult ApplyDiscount(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Json
                    (
                    new ResultDto
                    {
                        IsSuccess = false,
                        Message = "Enter the code please"
                    });
            }

            var discount = _discountService.GetDiscountByCode(code);
            if (discount.IsSuccess == true)
            {
                if (discount.Model.IsUsed)
                {
                    return Json
                        (
                        new ResultDto
                        {
                            IsSuccess = false,
                            Message = "The code have been used"
                        });
                }
                var basket = _basketService.GetBasket(_userId);
                _basketService.ApplyDiscountBasket(Guid.Parse(basket.Id), discount.Model.Id);
                _discountService.UseDiscount(discount.Model.Id);
                return Json
                    (
                    new ResultDto
                    {
                        IsSuccess = true,
                        Message = "Success"
                    });
            } else
            {
				return Json
				   (
				   new ResultDto
				   {
					   IsSuccess = false,
					   Message = discount.Message
				   });
			}
        }
        public IActionResult Checkout()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutBasketDto checkoutBasket)
        {

            checkoutBasket.UserId = _userId;
            checkoutBasket.Id = Guid.Parse(_basketService.GetBasket(_userId).Id);
            var model = _basketService.CheckoutBasket(checkoutBasket);
                return Redirect("CreateOrder");
         
        }
        public IActionResult CreateOrder()
        {
            return View();
        }
	}
}
