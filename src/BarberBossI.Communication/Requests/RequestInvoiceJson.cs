using BarberBossI.Communication.Enums;

namespace BarberBossI.Communication.Requests;
public class RequestInvoiceJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public IList<Tag> Tags { get; set; } = [];
    
}
