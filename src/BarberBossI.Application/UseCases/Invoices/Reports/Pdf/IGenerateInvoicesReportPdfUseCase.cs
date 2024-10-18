namespace BarberBossI.Application.UseCases.Invoices.Reports.Pdf;
public interface IGenerateInvoicesReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
