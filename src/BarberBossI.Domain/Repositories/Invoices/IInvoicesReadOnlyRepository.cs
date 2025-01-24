using BarberBossI.Domain.Entities;

namespace BarberBossI.Domain.Repositories.Invoices;
public interface IInvoicesReadOnlyRepository
{
    Task<List<Invoice>> GetAll(Entities.User user);
    Task<Invoice?> GetById(Entities.User user, long id);
    Task<List<Invoice>> FilterByMonth(Entities.User user, DateOnly month);
}
