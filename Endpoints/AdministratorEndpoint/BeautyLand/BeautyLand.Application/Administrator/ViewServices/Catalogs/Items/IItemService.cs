using BeautyLand.Application.Administrator.ViewServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Administrator.ViewServices.Catalogs.Items
{
    public interface IItemService
    {
        IEnumerable<ItemDto> GetItem();
        ResultDto UpadeteItem(UpdateItemDto updateItem);
    }
    
}
