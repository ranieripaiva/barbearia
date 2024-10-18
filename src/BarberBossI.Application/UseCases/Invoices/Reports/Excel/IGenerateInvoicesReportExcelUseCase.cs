namespace BarberBossI.Application.UseCases.Invoices.Reports.Excel;
public interface IGenerateInvoicesReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
