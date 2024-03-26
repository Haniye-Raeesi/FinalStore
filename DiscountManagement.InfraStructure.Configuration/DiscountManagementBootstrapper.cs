using DiscountManagement.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Repository;
using DiscountManagement.InfraStructure.EfCore;
using DiscountManagement.InfraStructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.InfraStructure.Configuration
{
    public class DiscountManagementBootstrapper
    {
        
        public static void Configure(IServiceCollection Services, string connectionString)
        {
            Services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();
            Services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            Services.AddTransient<IColleagueDiscountApplication, ColleagueDiscountApplication>();
            Services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();



            //Services.AddDbContext<DiscountContext>(x => x.UseSqlServer(connectionString));

            Services.AddDbContext<DiscountContext>(
            options => options.UseSqlServer(
            (connectionString),
            providerOptions => providerOptions.EnableRetryOnFailure()));


        }
    }
}
