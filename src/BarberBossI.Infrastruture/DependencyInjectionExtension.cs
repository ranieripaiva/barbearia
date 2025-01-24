using BarberBossI.Domain.Repositories;
using BarberBossI.Domain.Repositories.Expenses;
using BarberBossI.Domain.Repositories.Invoices;
using BarberBossI.Domain.Repositories.User;
using BarberBossI.Domain.Security.Cryptography;
using BarberBossI.Domain.Security.Tokens;
using BarberBossI.Domain.Services.LoggedUser;
using BarberBossI.Infrastructure.DataAccess.Repositories;
using BarberBossI.Infrastruture.DataAccess;
using BarberBossI.Infrastruture.DataAccess.Repositories;
using BarberBossI.Infrastruture.Extensions;
using BarberBossI.Infrastruture.Security.Tokens;
using BarberBossI.Infrastruture.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBossI.Infrastruture;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();
                
        AddToken(services, configuration);
        AddRepositories(services);
        
        if (configuration.IsTestEnvironment() == false)
        {
            AddDbContext(services, configuration);
        }
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IInvoicesReadOnlyRepository, InvoicesRepository>();
        services.AddScoped<IInvoicesWriteOnlyRepository, InvoicesRepository>();
        services.AddScoped<IInvoicesUpdateOnlyRepository, InvoicesRepository>();

        services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();

        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        //services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");
        /*
        var version = new Version(8, 0, 35);
        var serverVersion = new MySqlServerVersion(version);
        */
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<BarberBossIDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
