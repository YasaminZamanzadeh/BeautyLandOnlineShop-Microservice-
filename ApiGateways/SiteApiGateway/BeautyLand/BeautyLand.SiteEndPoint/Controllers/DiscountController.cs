using BeautyLand.Application.Services.Site.Discounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BeautyLand.SiteEndPoint.Controllers
{
    //This implementation is rest structure that communicate with GRPC
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
           _discountService = discountService;
        }

        [HttpGet]
        public IActionResult Get(string code)
        {
            var discount = _discountService.GetDiscountByCode(code);
            return Ok(discount);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(Guid id)
        {
            var discount = _discountService.GetDiscountById(id);
            return Ok(discount);
        }

        [HttpPut]
        public IActionResult Put(Guid id)
        {
            var discount = _discountService.UseDiscount(id);
            return Ok(discount);
        }
    }
}
