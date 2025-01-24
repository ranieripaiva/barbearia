using BarberBossI.Domain.Entities;
using BarberBossI.Domain.Repositories.Invoices;
using Moq;

namespace CommonTestUtilities.Repositories;
public class InvoicesUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IInvoicesUpdateOnlyRepository> _repository;

    public InvoicesUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IInvoicesUpdateOnlyRepository>();
    }

    public InvoicesUpdateOnlyRepositoryBuilder GetById(User user, Invoice? invoice)
    {
        if (invoice is not null)
            _repository.Setup(repository => repository.GetById(user, invoice.Id)).ReturnsAsync(invoice);

        return this;
    }

    public IInvoicesUpdateOnlyRepository Build() => _repository.Object;
}
