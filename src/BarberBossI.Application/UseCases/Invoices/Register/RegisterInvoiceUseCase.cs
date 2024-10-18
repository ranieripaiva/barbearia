using AutoMapper;
using BarberBossI.Communication.Requests;
using BarberBossI.Communication.Responses;
using BarberBossI.Domain.Entities;
using BarberBossI.Domain.Repositories;
using BarberBossI.Domain.Repositories.Invoices;
using BarberBossI.Exception.ExceptionsBase;

namespace BarberBossI.Application.UseCases.Invoices.Register;
public class RegisterInvoiceUseCase : IRegisterInvoiceUseCase
{
    private readonly IInvoicesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterInvoiceUseCase(
        IInvoicesWriteOnlyRepository repository, 
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }

    public async Task<ResponseRegisteredInvoiceJson> Execute(RequestInvoiceJson request)
    {
        Validate(request);
        
        var entity = _mapper.Map<Invoice>(request);
        /*{
            Amount = request.Amount,
            Date = request.Date,
            Description = request.Description,
            Title = request.Title,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType,
        };*/

        await _repository.Add(entity);

        await _unitOfWork.Commit();

        //return new ResponseRegisteredInvoiceJson();
        return _mapper.Map<ResponseRegisteredInvoiceJson>(entity);
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
