using BeautyLand.Application.Services.Databases.Catalogs;
using BeautyLand.Application.Services.Site.Catalogs.Dtos.Items;
using BeautyLand.Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeautyLand.Application.Services.Site.Catalogs.Items
{
    public class ItemService : IItemService
    {
        private readonly ICatalogDatabaseService _catalogContext;
        public ItemService(ICatalogDatabaseService catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public ItemDto GetItemById(Guid id)
        {
            var findItem = _catalogContext.Items
                 .Include(p => p.Type)
                 .SingleOrDefault(p => p.Id == id);

            if (findItem is null)
            {
                throw new Exception("Item is not found");
            }

            ItemDto item = new ItemDto
            {
                Id = findItem.Id,
                Name = findItem.Name,
                Description = findItem.Description, 
                Image = findItem.Image, 
                Price = findItem.Price, 
                 Type = new ItemTypeDto
                 {
                     Id = findItem.Type.Id,
                     Name = findItem.Type.Name, 
                 }
            };
            return item; 
        }

        public IEnumerable<ItemDto> GetItem()
        {
            var items = _catalogContext.Items
                .Include(p=> p.Type)
                 .OrderByDescending(p => p.Id)
                 .Select(p => new ItemDto
                 {
                     Id = p.Id, 
                     Name = p.Name, 
                     Description = p.Description,
                     Image = p.Image,
                     Price = p.Price,   
                     Type = new ItemTypeDto
                     { 
                      Id = p.Type.Id,
                      Name = p.Type.Name,
                     }
                 }).ToList();

            return items;   
             
        }

        public Guid CreateItem(CreateItemDto createItem)
        {
            var findType = _catalogContext.Types
                .Find(createItem.TypeId);

            if (findType is null)
            {
                throw new Exception("Type is not found");
            }

            Item item = new Item
            {
                Name = createItem.Name,
                Description = createItem.Description,
                Image = createItem.Image,
                Price = createItem.Price,
                Type = findType
            };
            _catalogContext.Items.Add(item);
            _catalogContext.SaveChanges();

            return item.Id;
        }

        public bool UpdateItem(UpdateItemDto updateItem)
        {
            var item = _catalogContext.Items
                 .Find(updateItem.Id);
            if (item is not null)
            {
                item.Name = updateItem.Name;
                _catalogContext.SaveChanges();
                return true;
                
            }
            else
            {
                return false;   
            }
        }
    }
}
