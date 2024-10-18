using BarberBossI.Domain.Repositories;

namespace BarberBossI.Infrastruture.DataAccess;
internal class UnitOfWork : IUnitOfWork
{
    private readonly BarberBossIDbContext _dbContext;
    public UnitOfWork(BarberBossIDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
