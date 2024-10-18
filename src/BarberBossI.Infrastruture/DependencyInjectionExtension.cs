using BarberBossI.Domain.Repositories;
using BarberBossI.Domain.Repositories.Invoices;
using BarberBossI.Infrastruture.DataAccess;
using BarberBossI.Infrastruture.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBossI.Infrastruture;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IInvoicesReadOnlyRepository, InvoicesRepository>();
        services.AddScoped<IInvoicesWriteOnlyRepository, InvoicesRepository>();
        services.AddScoped<IInvoicesUpdateOnlyRepository, InvoicesRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        var version = new Version(8, 0, 35);
        var serverVersion = new MySqlServerVersion(version);

        services.AddDbContext<BarberBossIDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
