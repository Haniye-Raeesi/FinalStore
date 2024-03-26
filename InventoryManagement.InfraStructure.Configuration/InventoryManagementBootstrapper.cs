using InventoryManagement.Application;
using InventoryManagement.Application.Contracts;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection Services, string connectionString)
        {
            Services.AddTransient<IInventoryRepository, InventoryRepository>();
            Services.AddTransient<IInventoryApplication, InventoryApplication>();




            Services.AddDbContext<InventoryContext>(
            options => options.UseSqlServer(
            (connectionString),
            providerOptions => providerOptions.EnableRetryOnFailure()));


        }
    }
}
