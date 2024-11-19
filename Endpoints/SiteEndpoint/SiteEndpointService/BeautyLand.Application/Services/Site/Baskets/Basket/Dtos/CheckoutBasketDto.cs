using System;

namespace BeautyLand.Application.Services.Site.Baskets.Basket.Dtos
{
	public class CheckoutBasketDto
    {
		public Guid Id { get; set; }
		public string UserId { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
		public string PostalCode { get; set; }
	}
}
