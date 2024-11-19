using System;

namespace BeautyLand.Application.Services.Site.BasketItems.Dtos.Items
{
    public class ItemDto
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }
}
