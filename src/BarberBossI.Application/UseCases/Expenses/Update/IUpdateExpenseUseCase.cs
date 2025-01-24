using BarberBossI.Communication.Requests;

namespace BarberBossI.Application.UseCases.Expenses.Update;
public interface IUpdateExpenseUseCase
{
    Task Execute(long id, RequestExpenseJson request);
}
