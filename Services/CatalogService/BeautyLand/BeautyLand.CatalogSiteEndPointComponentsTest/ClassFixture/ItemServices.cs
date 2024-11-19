
using BeautyLand.Application.Services.Databases.Catalogs;
using BeautyLand.Application.Services.Site.Catalogs.Items;
using BeautyLand.Application.Services.Site.Catalogs.Types;
using BeautyLand.Persistence.Databases.Payments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.SiteEndPointComponentTest.ClassFixture
{
    public class ItemServices
    {
        //ItemManagement service needs to inject IItemService
        public IItemService ItemService { get; }
        public ITypeService TypeService { get; }    

        //IItemService needs to inject ICatalogDatabaseService
        public ICatalogDatabaseService CatalogDatabaseService { get; }

        //These properties should be implemented in constructor
        public ItemServices()
        {
            DbContextOptionsBuilder<CatalogDatabase> builder = new DbContextOptionsBuilder<CatalogDatabase>();
            //In memory provider will create a face database 
            builder.UseInMemoryDatabase("CatalogDatabaseTest");

            //Beacuse used clean architecture and there is no CatalogDatabase in Application layer for injecetion in IItemService, we can use concreate class 

            CatalogDatabaseService = new CatalogDatabase(builder.Options);
            ItemService = new ItemService(CatalogDatabaseService);
            TypeService = new TypeService(CatalogDatabaseService);  

        }
    }
}
