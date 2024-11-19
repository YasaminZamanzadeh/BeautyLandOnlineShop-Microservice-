using BeautyLand.Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Databases.Catalogs
{
    public interface ICatalogDatabaseService
    {
         DbSet<Item> Items { get; set; }
         DbSet<Type> Types { get; set; }
        int SaveChanges();
        DatabaseFacade Database { get; set; }

    }
}
