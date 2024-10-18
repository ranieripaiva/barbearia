using BarberBossI.Domain.Entities;

namespace BarberBossI.Domain.Repositories.Invoices;
public interface IInvoicesUpdateOnlyRepository
{
    Task<Invoice?> GetById(long id);
    void Update(Invoice invoice);
}

