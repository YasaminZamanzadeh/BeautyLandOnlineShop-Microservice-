using BeautyLand.Domain.Attributes;
using System;

namespace BeautyLand.Domain.Catalogs
{
    [AudiTable]
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public virtual Type Type { get; set; }
        public Guid TypeId { get; set; }

        public void UpdatePrice(int price)
        {
            if (price is 0)
            {
                throw new Exception("Price cannot be zero");
            }
            Price = price;
        }
    }
}