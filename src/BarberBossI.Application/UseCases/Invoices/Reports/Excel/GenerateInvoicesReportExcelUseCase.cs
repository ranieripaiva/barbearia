using BarberBossI.Domain.Extentions;
using BarberBossI.Domain.Reports;
using BarberBossI.Domain.Repositories.Invoices;
using BarberBossI.Domain.Services.LoggedUser;
using ClosedXML.Excel;

namespace BarberBossI.Application.UseCases.Invoices.Reports.Excel;
public class GenerateInvoicesReportExcelUseCase : IGenerateInvoicesReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "R$ ";
    private readonly IInvoicesReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public GenerateInvoicesReportExcelUseCase(IInvoicesReadOnlyRepository repository, ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var loggedUser = await _loggedUser.Get();

        var invoices = await _repository.FilterByMonth(loggedUser, month);

        if (invoices.Count == 0)
        {
            return [];
        }

        var workbook = new XLWorkbook();

        workbook.Author = loggedUser.Name;
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";
        

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        var raw = 2;
        foreach (var invoice in invoices)
        {
            worksheet.Cell($"A{raw}").Value = invoice.Title;
            worksheet.Cell($"B{raw}").Value = invoice.Date ;
            worksheet.Cell($"B{raw}").Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell($"B{raw}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell($"C{raw}").Value = invoice.PaymentType.PaymentTypeToString();
            worksheet.Cell($"C{raw}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            worksheet.Cell($"D{raw}").Value = invoice.Amount;
            worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"{CURRENCY_SYMBOL} #,##0.00";

            worksheet.Cell($"E{raw}").Value = invoice.Description;

            raw++;
        }

        //worksheet.Columns().AdjustToContents();
        worksheet.Column(1).Width = 30;
        worksheet.Column(2).Width = 20;
        worksheet.Column(3).Width = 20;
        worksheet.Column(4).Width = 20;
        worksheet.Column(5).Width = 30;


        var file = new MemoryStream();

        workbook.SaveAs(file);

        return file.ToArray();
    }

    

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Font.FontColor = XLColor.White;

        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#205858");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

        
    }
}
