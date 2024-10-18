using BarberBossI.Domain.Entities;

namespace BarberBossI.Domain.Repositories.Invoices;
public interface IInvoicesReadOnlyRepository
{
    Task<List<Invoice>> GetAll();
    Task<Invoice?> GetById(long id);
    Task<List<Invoice>> FilterByMonth(DateOnly month);
}
