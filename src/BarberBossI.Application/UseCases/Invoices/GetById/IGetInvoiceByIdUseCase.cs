using BarberBossI.Communication.Responses;

namespace BarberBossI.Application.UseCases.Invoices.GetById;
public interface IGetInvoiceByIdUseCase
{
    Task<ResponseInvoiceJson> Execute(long id);
}
