using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using BeautyLand.Domain.Attributes;
using BeautyLand.Domain.Discounts;
using BeautyLand.Application.Services.Databases.Discounts;

namespace BeautyLand.Persistence.Databases.Discounts
{
    public class DiscountDatabase : DbContext, IDiscountDatabaseService
    {
        public DiscountDatabase(DbContextOptions<DiscountDatabase> options) : base(options)
        {

        }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityTypes in modelBuilder.Model.GetEntityTypes())
            {
                if (entityTypes.ClrType.GetCustomAttributes(typeof(AudiTable), true).Length > 0)
                {
                    modelBuilder
                        .Entity(entityTypes.Name)
                        .Property<DateTime?>("CreateDate")
                        .HasDefaultValue(DateTime.Now);

                    modelBuilder
                       .Entity(entityTypes.Name)
                       .Property<DateTime?>("ModifieDate");

                    modelBuilder
                    .Entity(entityTypes.Name)
                    .Property<DateTime?>("DeleteDate");

                    modelBuilder
                    .Entity(entityTypes.Name)
                    .Property<bool>("IsDeleted")
                    .HasDefaultValue(false);

                }
            }
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(entry =>
                entry.State == EntityState.Added ||
                entry.State == EntityState.Modified ||
                entry.State == EntityState.Deleted
                );

            foreach (var item in entries)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                if (entityType != null)
                {
                    var createProperty = entityType?.FindProperty("CreateDate");
                    var modifieProperty = entityType?.FindProperty("ModifieDate");
                    var deleteProperty = entityType?.FindProperty("DeleteDate");
                    var isDeletedProperty = entityType?.FindProperty("IsDeleted");

                    if (item.State == EntityState.Added && createProperty != null)
                    {
                        item.Property("CreateDate").CurrentValue = DateTime.Now;
                    }

                    if (item.State == EntityState.Modified && modifieProperty != null)
                    {
                        item.Property("ModifieDate").CurrentValue = DateTime.Now;
                    }

                    if (item.State == EntityState.Deleted)
                    {

                        if (deleteProperty != null && isDeletedProperty != null)
                        {
                            item.Property("DeleteDate").CurrentValue = DateTime.Now;
                           item.Property("IsDeleted").CurrentValue = true;

                            item.State = EntityState.Modified;
                        }
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
