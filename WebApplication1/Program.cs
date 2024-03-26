using _0_Framework.Application;
using _0_FrameWork.Application;
using AccountManagement.Infrastructure.Configuration;
using AccountManagement.InfraStructure.EfCore;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.InfraStructure.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Configuration;
using static WebApplication1.FileUploder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


//builder.Services.AddDbContext<AccountContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("RealStore"));
//});
var ConnectionString = builder.Configuration.GetConnectionString("RealStore");
ShopManagementBootstrapper.Configure(builder.Services, ConnectionString);
DiscountManagementBootstrapper.Configure(builder.Services, ConnectionString);
InventoryManagementBootstrapper.Configure(builder.Services, ConnectionString);
CommentManagementBootstrapper.Configure(builder.Services, ConnectionString);
BlogManagementBootstrapper.Configure(builder.Services, ConnectionString);
AccountManagementBootstrapper.Configure(builder.Services, ConnectionString);
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
