using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyLand.Application.Services.Site.Catalogs.Item.Dtos;

namespace BeautyLand.Application.Services.Site.Catalogs.Item
{
    public interface IItemService
    {
        IEnumerable<ItemDto> GetItem();
        ItemDto GetItemById(Guid id);
    }
}
