using BarberBossI.Application.UseCases.Invoices.Reports.Pdf;
using BarberBossI.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Expenses.Reports.Pdf;
public class GenerateInvoicesReportPdfUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var invoices = InvoiceBuilder.Collection(loggedUser);

        var useCase = CreateUseCase(loggedUser, invoices);

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Success_Empty()
    {
        var loggedUser = UserBuilder.Build();

        var useCase = CreateUseCase(loggedUser, []);

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().BeEmpty();
    }

    private GenerateInvoicesReportPdfUseCase CreateUseCase(User user, List<Invoice> invoices)
    {
        var repository = new InvoicesReadOnlyRepositoryBuilder().FilterByMonth(user, invoices).Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GenerateInvoicesReportPdfUseCase(repository, loggedUser);
    }
}
