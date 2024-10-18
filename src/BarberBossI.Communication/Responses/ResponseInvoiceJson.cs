﻿using BarberBossI.Communication.Enums;

namespace BarberBossI.Communication.Responses;
public class ResponseInvoiceJson
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
}
