using BarberBossI.Application.AutoMapper;
using BarberBossI.Application.UseCases.Invoices.Delete;
using BarberBossI.Application.UseCases.Invoices.GetAll;
using BarberBossI.Application.UseCases.Invoices.GetById;
using BarberBossI.Application.UseCases.Invoices.Register;
using BarberBossI.Application.UseCases.Invoices.Reports.Excel;
using BarberBossI.Application.UseCases.Invoices.Reports.Pdf;
using BarberBossI.Application.UseCases.Invoices.Update;
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
    }
}
