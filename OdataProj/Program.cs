using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using OdataProj.DAL;
using OdataProj.DAL.Repository;
using OdataProj.DAL.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Migrations and Database Setup - can comment out after database is created
// Temporary build a service provider for running database migrations
var tempServiceProvider = builder.Services.BuildServiceProvider();

// Create a scope to get the service provider
using (var scope = tempServiceProvider.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DataContext>();
    dbContext.Database.Migrate();  // This applies pending migrations or creates the database if it does not exist
}

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntityType<Order>();
modelBuilder.EntitySet<User>("User");
modelBuilder.EntitySet<Product>("Product");

builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
    "odata", modelBuilder.GetEdmModel()));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.Run();
