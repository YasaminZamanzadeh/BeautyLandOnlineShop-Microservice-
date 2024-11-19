using BeautyLand.Domain.Baskets;
using System;
using System.Collections.Generic;

namespace BeautyLand.Application.Services.Site.BasketItems.Dtos.BasketItems
{
	public class CreateBasketItemDto
    {
        public Guid Id { get; set; }
        public Guid BasketId { get; set; }
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
