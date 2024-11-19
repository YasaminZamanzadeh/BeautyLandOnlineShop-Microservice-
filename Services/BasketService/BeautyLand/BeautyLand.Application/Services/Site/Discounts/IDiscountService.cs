using BeautyLand.Application.Services.Site.Discounts.Dots;
using BeautyLand.Application.Services.Site.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Discounts
{
	public interface IDiscountService
	{
		DiscountDto GetDiscountByCode(string code);
		DiscountDto GetDiscountById(Guid id);
	}

}
