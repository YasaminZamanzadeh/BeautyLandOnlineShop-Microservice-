using BeautyLand.Domain.Attributes;
using BeautyLand.Domain.Catalogs.Item;
using System;

namespace BeautyLand.Domain.Baskets
{
    [AudiTable]
    public class BasketItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Basket Basket { get; set; }
        public Guid BasketId { get; set; }
		public Item Item { get; set; }
		public Guid ItemId { get; set; }
		public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
    
    }
}
