using System;

namespace BeautyLand.Application.Administrator.ViewServices.Catalogs.Items
{
    //public record ItemDto(Guid Id, string? Name, string? Description, string? Image, int Price, TypeDto? type);
    public class ItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public TypeDto Type { get; set; }
    }
}
