using BarberBossI.Domain.Entities;

namespace BarberBossI.Domain.Repositories.Invoices;
public interface IInvoicesUpdateOnlyRepository
{
    Task<Invoice?> GetById(Entities.User user, long id);
    void Update(Invoice invoice);
}

