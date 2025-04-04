using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using OdataProj.DAL;
using OdataProj.DAL.Repository;
using OdataProj.DAL.Repository.Interface;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddControllers().AddOData(opt =>
    opt.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100));

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.Run();
