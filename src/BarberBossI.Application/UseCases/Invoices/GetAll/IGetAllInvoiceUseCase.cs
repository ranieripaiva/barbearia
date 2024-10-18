using BarberBossI.Communication.Responses;

namespace BarberBossI.Application.UseCases.Invoices.GetAll;
public interface IGetAllInvoiceUseCase
{
    Task<ResponseInvoicesJson> Execute();
}
