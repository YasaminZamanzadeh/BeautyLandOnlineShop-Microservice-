using BeautyLand.Application.Services.Databases.Catalogs;
using BeautyLand.Application.Services.Site.Catalogs.Types;
using BeautyLand.Persistence.Databases.Payments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.SiteEndPointIntegrationTest.ClassFixture
{
    public class TypeServices : IDisposable
    {
        public ITypeService TypeService { get; }
        public ICatalogDatabaseService CatalogDatabaseService { get; }

        public TypeServices()
        {
            DbContextOptionsBuilder<CatalogDatabase> builder = new DbContextOptionsBuilder<CatalogDatabase>();
            builder.UseSqlServer("Data Source = .; Initial Catalog = CatalogBeautyLandTest; Integrated Security = True;");
            CatalogDatabaseService = new CatalogDatabase(builder.Options);
            CatalogDatabaseService.Database.EnsureCreated();
            TypeService = new TypeService(CatalogDatabaseService);
        }

        public void Dispose()
        {
            CatalogDatabaseService.Database.EnsureDeleted();
        }
    }
}
