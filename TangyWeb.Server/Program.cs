using Microsoft.EntityFrameworkCore;
using Tangy.Business.Respositories;
using Tangy.Business.Respositories.Interface;
using Tangy.Data;
using TangyWeb.Server.Data;
using TangyWeb.Server.Service;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Njg2NzU5QDMyMzAyZTMyMmUzMFZCelk1d0tiR1lYTFI2RTYxK3pqck1TTXpDY0gxWVdrWkRpR1M0dmVrNGs9");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSyncfusionBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
    app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

SeedDatabase();

app.UseAuthentication(); ;

app.UseAuthorization();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        initializer.Initialize();
    }
}
