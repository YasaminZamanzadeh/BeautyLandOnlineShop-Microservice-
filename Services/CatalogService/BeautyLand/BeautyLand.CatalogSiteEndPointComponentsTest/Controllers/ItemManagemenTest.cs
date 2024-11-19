using BeautyLand.Application.Services.Site.Catalogs.Dtos.Items;
using BeautyLand.Application.Services.Site.Catalogs.Dtos.Types;
using BeautyLand.CatalogSiteEndPoint.Controllers;
using BeautyLand.SiteEndPoint.Controllers;
using BeautyLand.SiteEndPointComponentTest.ClassFixture;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;

namespace BeautyLand.SiteEndPointComponentTest.Controllers
{
    public class ItemManagemenTest : IClassFixture<ItemServices>   
    {
        private readonly ItemServices _itemServices;

        public ItemManagemenTest(ItemServices itemServices)
        {
            _itemServices = itemServices;
        }
        [Fact]
        public void Create_Item()
        {
            //Arrange
            var createType = new Filler<TypeDto>().Create();
            var typeId = _itemServices.TypeService.CreateType(createType);
            var createItem = new Filler<CreateItemDto>().Create();
            createItem.TypeId = typeId;

            //ItemManagementController instance needs to inject IItemService and because interface does not have implementation, I have to use concreate class with inheriting IClassFixture
            ItemManagementController itemManagementController = new ItemManagementController(_itemServices.ItemService);
            //Act
            var result = itemManagementController.Post(createItem) as CreatedResult;
            //Assert

            var latestItem = _itemServices.CatalogDatabaseService.Items
                .FirstOrDefault(p => p.Id == Guid.Parse(result.Value.ToString()));
            Assert.NotNull(latestItem);
            Assert.Equal(latestItem.Name, createItem.Name);
            Assert.Equal(latestItem.Description, createItem.Description);
            Assert.Equal(latestItem.Price, createItem.Price);



        }
    }
}
