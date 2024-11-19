using BeautyLand.Application.Services.Site.Discounts.Dtos;
using BeautyLand.Application.Services.Site.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Discounts
{
    public interface IDiscountService
    {
        ResultDto<DiscountDto> GetDiscountByCode(string code);
        ResultDto<DiscountDto> GetDiscountById(Guid id);
        ResultDto UseDiscount(Guid id);
    }

}
