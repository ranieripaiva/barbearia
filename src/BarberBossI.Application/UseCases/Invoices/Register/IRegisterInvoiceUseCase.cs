using BarberBossI.Communication.Requests;
using BarberBossI.Communication.Responses;

namespace BarberBossI.Application.UseCases.Invoices.Register;
public interface IRegisterInvoiceUseCase
{
    Task<ResponseRegisteredInvoiceJson> Execute(RequestInvoiceJson request);
}
