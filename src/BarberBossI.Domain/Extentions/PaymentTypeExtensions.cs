using BarberBossI.Domain.Enums;
using BarberBossI.Domain.Reports;

namespace BarberBossI.Domain.Extentions;
public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourceReportGenerationMessages.CASH,
            PaymentType.CreditCard => ResourceReportGenerationMessages.CREDIT_CARD,
            PaymentType.DebitCard => ResourceReportGenerationMessages.DEBIT_CARD,
            PaymentType.Pix => ResourceReportGenerationMessages.PIX,
            _ => string.Empty
        };
    }
}
