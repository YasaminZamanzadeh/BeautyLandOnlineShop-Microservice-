using BeautyLand.Domain.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Domain.Catalogs.Item
{
	public class Item
	{
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
