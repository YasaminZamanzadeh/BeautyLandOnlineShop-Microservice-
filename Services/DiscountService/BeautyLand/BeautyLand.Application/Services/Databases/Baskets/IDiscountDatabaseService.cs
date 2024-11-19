using Microsoft.EntityFrameworkCore;
using BeautyLand.Domain.Discounts;

namespace BeautyLand.Application.Services.Databases.Discounts
{
    public interface IDiscountDatabaseService
    {
        DbSet<Discount> Discounts { get; set; }
        int SaveChanges();
    }
}
