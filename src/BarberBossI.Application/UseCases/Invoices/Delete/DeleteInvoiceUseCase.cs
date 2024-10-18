using BarberBossI.Domain.Repositories;
using BarberBossI.Exception.ExceptionsBase;
using BarberBossI.Exception;
using BarberBossI.Domain.Repositories.Invoices;

namespace BarberBossI.Application.UseCases.Invoices.Delete;
public class DeleteInvoiceUseCase : IDeleteInvoiceUseCase
{
    private readonly IInvoicesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInvoiceUseCase(
        IInvoicesWriteOnlyRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var result = await _repository.Delete(id);

        if (result == false)
        {
            throw new NotFoundException(ResourceErrorMessages.INVOICE_NOT_FOUND);
        }

        await _unitOfWork.Commit();
    }
}
