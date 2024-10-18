using BarberBossI.Domain.Entities;
using BarberBossI.Domain.Repositories.Invoices;
using Microsoft.EntityFrameworkCore;

namespace BarberBossI.Infrastruture.DataAccess.Repositories;
internal class InvoicesRepository : IInvoicesReadOnlyRepository, IInvoicesWriteOnlyRepository, IInvoicesUpdateOnlyRepository
{
    private readonly BarberBossIDbContext _dbContext;
    public InvoicesRepository(BarberBossIDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Invoice invoice)
    {      
        await _dbContext.Invoices.AddAsync(invoice);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Invoices.FirstOrDefaultAsync(invoice => invoice.Id == id);
        if (result is null)
        {
            return false;
        }

        _dbContext.Invoices.Remove(result);

        return true;
    }
    public async Task<List<Invoice>> GetAll()
    {
        return await _dbContext.Invoices.AsNoTracking().ToListAsync();
    }

    async Task<Invoice?> IInvoicesReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.Invoices.AsNoTracking().FirstOrDefaultAsync(invoice => invoice.Id == id);
    }

    async Task<Invoice?> IInvoicesUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Invoices.FirstOrDefaultAsync(invoice => invoice.Id == id);
    }
    public void Update(Invoice invoice)
    {        
        _dbContext.Invoices.Update(invoice);
    }

    public async Task<List<Invoice>> FilterByMonth(DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;
        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59).Date;

        return await _dbContext
            .Invoices
            .AsNoTracking()
            .Where(invoice => invoice.Date >= startDate && invoice.Date <= endDate )
            .OrderBy(invoice => invoice.Date)
            .ThenBy(invoice => invoice.Title)
            .ToListAsync();
    }
}
