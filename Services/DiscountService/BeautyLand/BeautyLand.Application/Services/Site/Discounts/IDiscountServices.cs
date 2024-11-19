using System;
using System.Text;
using System.Threading.Tasks;
using BeautyLand.Application.Services.Site.Discounts.Dtos;

namespace BeautyLand.Application.Services.Site.Discounts
{
    public interface IDiscountServices
    {
        DiscountDto GetDiscountByCode(string code);
        DiscountDto GetDiscountById(Guid id);
        bool UseDiscount(Guid id);
        bool CreateDiscount(string code, int amount);
    }
}
