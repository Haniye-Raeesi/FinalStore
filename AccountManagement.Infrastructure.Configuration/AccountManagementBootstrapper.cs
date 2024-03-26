using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.InfraStructure.EfCore;
using AccountManagement.InfraStructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infrastructure.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection Services, String ConnectionString)
        {
            Services.AddTransient<IAccountApplication, AccountApplication>();
            Services.AddTransient<IAccountRepository, AccountRepository>();
            Services.AddTransient<IRoleApplication, RoleApplication>();
            Services.AddTransient<IRoleRepository, RoleRepository>();


            Services.AddDbContext<AccountContext>(
            options => options.UseSqlServer(
            (ConnectionString),
            providerOptions => providerOptions.EnableRetryOnFailure()));

        }
    }
  
}
