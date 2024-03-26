using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.InfraStructure.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.InfraStructure.EfCore
{
    public class DiscountContext : DbContext
    {
        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }
        public DbSet<ColleagueDiscount> ColleagueDiscounts { get; set; }


        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CustomerDiscountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
