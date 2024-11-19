using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyLand.Domain.Baskets;
using BeautyLand.Domain.Catalogs.Item;

namespace BeautyLand.Application.Services.Databases.Baskets
{
    public interface IBasketDatabaseService
    {
        DbSet<Basket> Baskets { get; set; }
        DbSet<BasketItem> BasketItems { get; set; }
		DbSet<Item> Items { get; set; }

		int SaveChanges();
    }
}
