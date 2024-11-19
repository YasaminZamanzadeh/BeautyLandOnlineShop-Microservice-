using BeautyLand.Application.Services.Site.Catalogs.Type.Dtos;

namespace BeautyLand.Application.Services.Site.Catalogs.Item.Dtos
{
    public class ItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public TypeDto Type { get; set; }
    }
}
