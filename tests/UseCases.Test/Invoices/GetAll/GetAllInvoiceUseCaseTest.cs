using BarberBossI.Application.UseCases.Invoices.GetAll;
using BarberBossI.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Invoices.GetAll;

public class GetAllInvoiceUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var invoices = InvoiceBuilder.Collection(loggedUser);

        var useCase = CreateUseCase(loggedUser, invoices);

        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Invoices.Should().NotBeNullOrEmpty().And.AllSatisfy(invoice =>
        {
            invoice.Id.Should().BeGreaterThan(0);
            invoice.Title.Should().NotBeNullOrEmpty();
            invoice.Amount.Should().BeGreaterThan(0);
        });
    }

    private GetAllInvoiceUseCase CreateUseCase(User user, List<Invoice> invoices)
    {
        var repository = new InvoicesReadOnlyRepositoryBuilder().GetAll(user, invoices).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetAllInvoiceUseCase(repository, mapper, loggedUser);
    }
}
