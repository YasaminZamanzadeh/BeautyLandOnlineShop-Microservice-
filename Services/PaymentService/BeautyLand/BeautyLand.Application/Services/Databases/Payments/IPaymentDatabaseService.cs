using BeautyLand.Domain.Order;
using BeautyLand.Domain.Payment;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Databases.Catalogs
{
    public interface IPaymentDatabaseService
    {
         DbSet<Order> Orders { get; set; }
         DbSet<Payment> Payments { get; set; }
        int SaveChanges();
    }
}
