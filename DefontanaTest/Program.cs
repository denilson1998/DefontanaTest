

using System.Configuration;
using ApplicationLayer;
using DefontanaTest.Database;
using DomainLayer.Interfaces.Repositories;
using InfrastructureLayer.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

string connectionString = @"Data source=lab-defontana.caporvnn6sbh.us-east-1.rds.amazonaws.com;Initial Catalog=Prueba;User Id=ReadOnly;Password=d*3PSf2MmRX9vJtA5sgwSphCVQ26*T53uU;Integrated Security=true;Trusted_Connection=false; MultipleActiveResultSets=true; TrustServerCertificate=True";

IHost _host = Host.CreateDefaultBuilder().ConfigureServices(
    services =>
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
        services.AddSingleton<IApplication, Application>();
        services.AddTransient<ISaleDetailRepository, SaleDetailRepository>();
    }).Build();

var app = _host.Services.GetRequiredService<IApplication>();
app.Run();


