using BarberBossI.Domain.Entities;

namespace BarberBossI.Domain.Repositories.Expenses;
public interface IExpensesUpdateOnlyRepository
{
    Task<Expense?> GetById(Entities.User user, long id);
    void Update(Expense expense);
}
