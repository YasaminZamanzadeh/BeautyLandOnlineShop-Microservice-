using BeautyLand.Application.Services.Databases.Discounts;
using BeautyLand.Application.Services.Site.Discounts.Dtos;
using BeautyLand.Domain.Discounts;
using System;
using System.Linq;

namespace BeautyLand.Application.Services.Site.Discounts
{
    public class DiscountService: IDiscountServices
    {
        private readonly IDiscountDatabaseService _discountContext;
        public DiscountService(IDiscountDatabaseService discountContext)
        {
            _discountContext = discountContext;
        }

        public bool CreateDiscount(string code, int amount)
        {
            Discount discount = new Discount
            {
                Code = code,    
                Amount = amount,
                IsUsed = false
            };
            _discountContext.Discounts.Add(discount);
            _discountContext.SaveChanges();
            return true;    
        }

        public DiscountDto GetDiscountByCode(string code)
        {
            var discount = _discountContext.Discounts
                  .SingleOrDefault(p => p.Code == code);

            if (discount == null)
            {
                return null;
            }

            return new DiscountDto
            {
                Id = discount.Id,   
                Code= discount.Code,
                Amount= discount.Amount,    
                IsUsed = discount.IsUsed,   
            };
        }

        public DiscountDto GetDiscountById(Guid id)
        {
            var discount = _discountContext.Discounts
                     .SingleOrDefault(p => p.Id == id);

            if (discount == null)
            {
                return null;
            }

            return new DiscountDto
            {
                Id = discount.Id,
                Code = discount.Code,
                Amount = discount.Amount,
                IsUsed = discount.IsUsed,
            };

        }

        public bool UseDiscount(Guid id)
        {
            var discount = _discountContext.Discounts
                  .Find(id);

            if (discount == null)
            {
                throw new Exception("Discount is not found");
            }

            discount.IsUsed = true;
            _discountContext.SaveChanges(); 
            return true;    
        }
    }
}
