using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeautyLand.Application.Services.Site.Catalogs.Dtos.Items;

namespace BeautyLand.Application.Services.Site.Catalogs.Items
{
    public interface IItemService
    {
        IEnumerable<ItemDto> GetItem();
        ItemDto GetItemById(Guid id);
        Guid CreateItem(CreateItemDto item);
        bool UpdateItem(UpdateItemDto updateItem);
    }
}
