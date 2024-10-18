using BarberBossI.Communication.Requests;

namespace BarberBossI.Application.UseCases.Invoices.Update;
public interface IUpdateInvoiceUseCase
{
    Task Execute(long id, RequestInvoiceJson request);
}
