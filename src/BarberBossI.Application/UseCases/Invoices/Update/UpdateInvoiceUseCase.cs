using AutoMapper;
using BarberBossI.Communication.Requests;
using BarberBossI.Domain.Repositories;
using BarberBossI.Domain.Repositories.Invoices;
using BarberBossI.Domain.Services.LoggedUser;
using BarberBossI.Exception;
using BarberBossI.Exception.ExceptionsBase;
using PdfSharp.Drawing;

namespace BarberBossI.Application.UseCases.Invoices.Update;
public class UpdateInvoiceUseCase : IUpdateInvoiceUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInvoicesUpdateOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public UpdateInvoiceUseCase(
        IMapper mapper,        
        IUnitOfWork unitOfWork,
        IInvoicesUpdateOnlyRepository repository,
        ILoggedUser loggedUser)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id, RequestInvoiceJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var invoice = await _repository.GetById(loggedUser, id);

        if (invoice is null)
        {
            throw new NotFoundException(ResourceErrorMessages.INVOICE_NOT_FOUND);
        }

        invoice.Tags.Clear();

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
