using AutoMapper;
using BarberBossI.Communication.Responses;
using BarberBossI.Domain.Repositories.Invoices;
using BarberBossI.Domain.Services.LoggedUser;

namespace BarberBossI.Application.UseCases.Invoices.GetAll;
public class GetAllInvoiceUseCase : IGetAllInvoiceUseCase
{
    private readonly IInvoicesReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetAllInvoiceUseCase(IInvoicesReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseInvoicesJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetAll(loggedUser);

        return new ResponseInvoicesJson
        {
            Invoices = _mapper.Map<List<ResponseShortInvoiceJson>>(result)
        };
    }
}
