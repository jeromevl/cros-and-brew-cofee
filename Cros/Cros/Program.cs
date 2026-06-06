using AutoMapper;
using Cros.BusinessLogic.Helpers;
using Cros.BusinessLogic.Services.Concrete;
using Cros.BusinessLogic.Services.Interfaces;
using Cros.DataAccess;
using Cros.DataAccess.Helpers;
using Cros.DataAccess.Repositories.Concrete;
using Cros.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var dbPath = Path.Combine(builder.Environment.ContentRootPath, "data", "cros.db");
Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
builder.Services.AddDbContext<CrosDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

ConfigureTypeMappings(builder);
ConfigureAutoMapper(builder);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Run any unapplied migration
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CrosDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Scripts")),
    RequestPath = "/scripts"
});

app.MapControllers();
app.MapGet("/", () => Results.Redirect("/customers"));

app.Run();
//
// Helper methods
//
void ConfigureTypeMappings(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
}

void ConfigureAutoMapper(WebApplicationBuilder builder)
{
    var config = new MapperConfiguration(c =>
    {
        c.AddProfile(new AutoMapperBusinessLogicProfile());
        c.AddProfile(new AutoMapperDataAccessProfile());
    });

    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}