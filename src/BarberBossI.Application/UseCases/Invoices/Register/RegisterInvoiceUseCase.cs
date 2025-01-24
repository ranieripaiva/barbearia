using AutoMapper;
using BarberBossI.Communication.Requests;
using BarberBossI.Communication.Responses;
using BarberBossI.Domain.Entities;
using BarberBossI.Domain.Repositories;
using BarberBossI.Domain.Repositories.Invoices;
using BarberBossI.Domain.Services.LoggedUser;
using BarberBossI.Exception.ExceptionsBase;

namespace BarberBossI.Application.UseCases.Invoices.Register;
public class RegisterInvoiceUseCase : IRegisterInvoiceUseCase
{
    private readonly IInvoicesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public RegisterInvoiceUseCase(
        IInvoicesWriteOnlyRepository repository, 
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;

    }

    public async Task<ResponseRegisteredInvoiceJson> Execute(RequestInvoiceJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var invoice = _mapper.Map<Invoice>(request);
        invoice.UserId = loggedUser.Id;

        await _repository.Add(invoice);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredInvoiceJson>(invoice);
    }
    private void Validate(RequestInvoiceJson request)
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
