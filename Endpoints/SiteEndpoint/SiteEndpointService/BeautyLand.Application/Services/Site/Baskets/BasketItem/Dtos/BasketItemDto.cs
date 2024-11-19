using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Baskets.BasketItem.Dtos
{
    public class BasketItemDto
    {
        public string Id { get; set; }
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public int TotalPrice()
        {
            return Quantity * Price;

        }
    }
}

