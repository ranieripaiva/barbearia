using BarberBossI.Application.UseCases.Invoices.GetById;
using BarberBossI.Domain.Entities;
using BarberBossI.Exception;
using BarberBossI.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Invoices.GetById;
public class GetInvoiceByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var invoice = InvoiceBuilder.Build(loggedUser);

        var useCase = CreateUseCase(loggedUser, invoice);

        var result = await useCase.Execute(invoice.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(invoice.Id);
        result.Title.Should().Be(invoice.Title);
        result.Description.Should().Be(invoice.Description);
        result.Date.Should().Be(invoice.Date);
        result.Amount.Should().Be(invoice.Amount);
        result.PaymentType.Should().Be((BarberBossI.Communication.Enums.PaymentType)invoice.PaymentType);
        result.Tags.Should().NotBeNullOrEmpty().And.BeEquivalentTo(invoice.Tags.Select(tag => tag.Value));
    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var loggedUser = UserBuilder.Build();

        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(id: 1000);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.INVOICE_NOT_FOUND));
    }

    private GetInvoiceByIdUseCase CreateUseCase(User user, Invoice? invoice = null)
    {
        var repository = new InvoicesReadOnlyRepositoryBuilder().GetById(user, invoice).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetInvoiceByIdUseCase(repository, mapper, loggedUser);
    }
}
