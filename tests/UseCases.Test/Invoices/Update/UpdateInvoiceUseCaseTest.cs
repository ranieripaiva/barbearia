using BarberBossI.Application.UseCases.Invoices.Update;
using BarberBossI.Domain.Entities;
using BarberBossI.Exception;
using BarberBossI.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Test.Expenses.Update;
public class UpdateInvoiceUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestInvoiceJsonBuilder.Build();
        var invoice = InvoiceBuilder.Build(loggedUser);

        var useCase = CreateUseCase(loggedUser, invoice);

        var act = async () => await useCase.Execute(invoice.Id, request);

        await act.Should().NotThrowAsync();

        invoice.Title.Should().Be(request.Title);
        invoice.Description.Should().Be(request.Description);
        invoice.Date.Should().Be(request.Date);
        invoice.Amount.Should().Be(request.Amount);
        invoice.PaymentType.Should().Be((BarberBossI.Domain.Enums.PaymentType)request.PaymentType);
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        var loggedUser = UserBuilder.Build();
        var invoice = InvoiceBuilder.Build(loggedUser);

        var request = RequestInvoiceJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(loggedUser, invoice);

        var act = async () => await useCase.Execute(invoice.Id, request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.TITLE_REQUIRED));
    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var loggedUser = UserBuilder.Build();

        var request = RequestInvoiceJsonBuilder.Build();

        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(id: 1000, request);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.INVOICE_NOT_FOUND));
    }

    private UpdateInvoiceUseCase CreateUseCase(User user, Invoice? invoice = null)
    {
        var repository = new InvoicesUpdateOnlyRepositoryBuilder().GetById(user, invoice).Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new UpdateInvoiceUseCase(mapper, unitOfWork, repository, loggedUser);
    }
}
