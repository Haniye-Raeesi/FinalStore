using AccountManagement.Infrastructure.Configuration;
using AccountManagement.InfraStructure.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddDbContext<AccountContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RealStore"));
}); 
var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();



//var ConnectionString=builder.Configuration.GetConnectionString("RealStore");
//builder.Services.AddDbContext<AccountContext>();
//AccountManagementBootstrapper.Configure(builder.Services, ConnectionString);

