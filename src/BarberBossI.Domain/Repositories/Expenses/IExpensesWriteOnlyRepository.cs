using BarberBossI.Domain.Entities;

namespace BarberBossI.Domain.Repositories.Expenses;
public interface IExpensesWriteOnlyRepository
{
    Task Add(Expense expense);
    Task Delete(long id);
}
