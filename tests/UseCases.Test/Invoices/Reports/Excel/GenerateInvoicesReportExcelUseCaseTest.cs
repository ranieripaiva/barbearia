using BarberBossI.Application.UseCases.Invoices.Reports.Excel;
using BarberBossI.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Invoices.Reports.Excel;
public class GenerateInvoicesReportExcelUseCaseTest
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

        var useCase = CreateUseCase(loggedUser, new List<Invoice>());

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().BeEmpty();
    }

    private GenerateInvoicesReportExcelUseCase CreateUseCase(User user, List<Invoice> invoices)
    {
        var repository = new InvoicesReadOnlyRepositoryBuilder().FilterByMonth(user, invoices).Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GenerateInvoicesReportExcelUseCase(repository, loggedUser);
    }
}
