using BarberBossI.Infrastruture.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBossI.Infrastruture.Migrations;
public static class DataBaseMigration
{
    public async static Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<BarberBossIDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
