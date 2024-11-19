using System;

namespace BeautyLand.Application.Services.Site.Catalogs.Dtos.Items
{
    public class CreateItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public Guid TypeId { get; set; }

    }
}
