using BarberBossI.Application.AutoMapper;
using BarberBossI.Application.UseCases.Expenses.Delete;
using BarberBossI.Application.UseCases.Expenses.GetAll;
using BarberBossI.Application.UseCases.Expenses.GetById;
using BarberBossI.Application.UseCases.Expenses.Register;
using BarberBossI.Application.UseCases.Expenses.Update;
using BarberBossI.Application.UseCases.Invoices.Delete;
using BarberBossI.Application.UseCases.Invoices.GetAll;
using BarberBossI.Application.UseCases.Invoices.GetById;
using BarberBossI.Application.UseCases.Invoices.Register;
using BarberBossI.Application.UseCases.Invoices.Reports.Excel;
using BarberBossI.Application.UseCases.Invoices.Reports.Pdf;
using BarberBossI.Application.UseCases.Invoices.Update;
using BarberBossI.Application.UseCases.Login.DoLogin;
using BarberBossI.Application.UseCases.Users.ChangePassword;
using BarberBossI.Application.UseCases.Users.Delete;
using BarberBossI.Application.UseCases.Users.Profile;
using BarberBossI.Application.UseCases.Users.Register;
using BarberBossI.Application.UseCases.Users.Update;
using BarberBossI.Application.UseCases.Expenses.Reports.Excel;
using BarberBossI.Application.UseCases.Expenses.Reports.Pdf;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBossI.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterInvoiceUseCase, RegisterInvoiceUseCase>();
        services.AddScoped<IGetAllInvoiceUseCase, GetAllInvoiceUseCase>();
        services.AddScoped<IGetInvoiceByIdUseCase, GetInvoiceByIdUseCase>();
        services.AddScoped<IDeleteInvoiceUseCase, DeleteInvoiceUseCase>();
        services.AddScoped<IUpdateInvoiceUseCase, UpdateInvoiceUseCase>();
        services.AddScoped<IGenerateInvoicesReportExcelUseCase, GenerateInvoicesReportExcelUseCase>();
        services.AddScoped<IGenerateInvoicesReportPdfUseCase, GenerateInvoicesReportPdfUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();

        services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
        services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
        services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
        services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
        services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
        services.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>();
        services.AddScoped<IGenerateExpensesReportPdfUseCase, GenerateExpensesReportPdfUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();

        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IDeleteUserAccountUseCase, DeleteUserAccountUseCase>();
    }
}
