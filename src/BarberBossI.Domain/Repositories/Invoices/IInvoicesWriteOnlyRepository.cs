using BarberBossI.Domain.Entities;
namespace BarberBossI.Domain.Repositories.Invoices;

public interface IInvoicesWriteOnlyRepository
{
    Task Add(Invoice invoive);
    /// <summary>
    /// Thos function return TRUE if the deletion was succesfull
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Delete(long id);
}

