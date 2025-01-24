using BarberBossI.Communication.Requests;
using BarberBossI.Communication.Responses;

namespace BarberBossI.Application.UseCases.Expenses.Register;
public interface IRegisterExpenseUseCase
{
    Task<ResponseRegisteredExpenseJson> Execute(RequestExpenseJson request);
}
