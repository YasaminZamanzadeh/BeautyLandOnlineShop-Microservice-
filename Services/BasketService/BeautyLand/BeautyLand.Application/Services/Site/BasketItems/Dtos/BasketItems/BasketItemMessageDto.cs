using System;

namespace BeautyLand.Application.Services.Site.BasketItems.Dtos.BasketItems
{
    public class BasketItemMessageDto
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
