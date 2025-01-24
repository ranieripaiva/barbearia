using BarberBossI.Domain.Entities;
using BarberBossI.Domain.Repositories.Invoices;
using Moq;

namespace CommonTestUtilities.Repositories;
public class InvoicesReadOnlyRepositoryBuilder
{
    private readonly Mock<IInvoicesReadOnlyRepository> _repository;

    public InvoicesReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IInvoicesReadOnlyRepository>();
    }

    public InvoicesReadOnlyRepositoryBuilder GetAll(User user, List<Invoice> invoices)
    {
        _repository.Setup(repository => repository.GetAll(user)).ReturnsAsync(invoices);

        return this;
    }

    public InvoicesReadOnlyRepositoryBuilder GetById(User user, Invoice? invoice)
    {
        if (invoice is not null)
            _repository.Setup(repository => repository.GetById(user, invoice.Id)).ReturnsAsync(invoice);

        return this;
    }

    public InvoicesReadOnlyRepositoryBuilder FilterByMonth(User user, List<Invoice> invoices)
    {
        _repository.Setup(repository => repository.FilterByMonth(user, It.IsAny<DateOnly>())).ReturnsAsync(invoices);

        return this;
    }

    public IInvoicesReadOnlyRepository Build() => _repository.Object;
}
