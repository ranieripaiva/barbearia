using AutoMapper;
using BarberBossI.Communication.Requests;
using BarberBossI.Domain.Repositories;
using BarberBossI.Domain.Repositories.Invoices;
using BarberBossI.Exception;
using BarberBossI.Exception.ExceptionsBase;

namespace BarberBossI.Application.UseCases.Invoices.Update;
public class UpdateInvoiceUseCase : IUpdateInvoiceUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInvoicesUpdateOnlyRepository _repository;

    public UpdateInvoiceUseCase(IMapper mapper, IMapper invoiceUseCase, IUnitOfWork unitOfWork, IInvoicesUpdateOnlyRepository repository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task Execute(long id, RequestInvoiceJson request)
    {
        Validate(request);

        var invoice = await _repository.GetById(id);

        if (invoice is null)
        {
            throw new NotFoundException(ResourceErrorMessages.INVOICE_NOT_FOUND);
        }

        _mapper.Map(request, invoice);

        _repository.Update(invoice);

        await _unitOfWork.Commit();
    }

    public void Validate(RequestInvoiceJson request) 
    {
        var validator = new InvoiceValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false) 
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
