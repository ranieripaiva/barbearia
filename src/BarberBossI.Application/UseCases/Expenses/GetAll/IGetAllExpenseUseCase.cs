using BarberBossI.Communication.Responses;

namespace BarberBossI.Application.UseCases.Expenses.GetAll;
public interface IGetAllExpenseUseCase
{
    Task<ResponseExpensesJson> Execute();
}
