using BeautyLand.Application.Services.Databases.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Catalogs.Items
{
    public interface IItemService
    {
        bool UpdateItem(Guid id, string name);
    }

    public class ItemService : IItemService
    {
        private readonly IBasketDatabaseService _basketContext;
        public ItemService(IBasketDatabaseService basketService)
        {
            _basketContext = basketService;
        }

        public bool UpdateItem(Guid id, string name)
        {
            var item = _basketContext.Items
                 .Find(id);
            if (item is not null)
            {
                item.Name = name;
                _basketContext.SaveChanges();
            }
            return true;
        }
    }
}
