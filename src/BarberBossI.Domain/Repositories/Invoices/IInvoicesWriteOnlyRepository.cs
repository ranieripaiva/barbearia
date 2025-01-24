using BarberBossI.Domain.Entities;
namespace BarberBossI.Domain.Repositories.Invoices;

public interface IInvoicesWriteOnlyRepository
{
    Task Add(Invoice invoive);    
    Task Delete(long id);
}

