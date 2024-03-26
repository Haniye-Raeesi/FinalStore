using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _0_FrameWork.Application;
using _0_FrameWork.InfraStructure;
using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.InfraStructure.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using ServiceHost;
using ShopManagement.Configuration;
using ShopManagement.Domain.Services;
using ShopManagement.InfraStructure.InventoryAcl;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();

var ConnectionString =builder.Configuration.GetConnectionString("RealStore");
ShopManagementBootstrapper.Configure(builder.Services, ConnectionString);
DiscountManagementBootstrapper.Configure(builder.Services, ConnectionString);
InventoryManagementBootstrapper.Configure(builder.Services, ConnectionString);
CommentManagementBootstrapper.Configure(builder.Services, ConnectionString);
BlogManagementBootstrapper.Configure(builder.Services, ConnectionString);
AccountManagementBootstrapper.Configure(builder.Services, ConnectionString);
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
builder.Services.AddTransient<IShopInventoryAcl, ShopInventoryAcl>();



builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});
//builder.Services.AddRazorPages();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation()
    .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
    });



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });
    builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminArea",
        builder => builder.RequireRole(new List<string> { Roles.Administrator, Roles.ContentUploader }));

    options.AddPolicy("Shop",
        builder => builder.RequireRole(new List<string> { Roles.Administrator }));

    options.AddPolicy("Discount",
        builder => builder.RequireRole(new List<string> { Roles.Administrator }));

    options.AddPolicy("Account",
        builder => builder.RequireRole(new List<string> { Roles.Administrator }));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
// builder.Services.AddScoped<IHttpContextAccessor,HttpContextAccessor>();  this line is same as above
app.UseAuthentication();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    //    endpoints.MapControllerRoute(
    //name: "Areas",
    //pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    //);
    endpoints.MapControllers();
});

//app.MapRazorPages();

app.Run();
