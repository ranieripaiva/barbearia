using BarberBossI.Domain.Repositories;
using BarberBossI.Exception.ExceptionsBase;
using BarberBossI.Exception;
using BarberBossI.Domain.Repositories.Invoices;
using BarberBossI.Domain.Services.LoggedUser;

namespace BarberBossI.Application.UseCases.Invoices.Delete;
public class DeleteInvoiceUseCase : IDeleteInvoiceUseCase
{
    private readonly IInvoicesReadOnlyRepository _invoicesReadOnly;
    private readonly IInvoicesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteInvoiceUseCase(
        IInvoicesWriteOnlyRepository repository,
        IInvoicesReadOnlyRepository invoicesReadOnly,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _invoicesReadOnly = invoicesReadOnly;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var invoice = await _invoicesReadOnly.GetById(loggedUser, id);

        if (invoice is null)
        {
            throw new NotFoundException(ResourceErrorMessages.INVOICE_NOT_FOUND);
        }

        await _repository.Delete(id);

        await _unitOfWork.Commit();
    }
}
