﻿namespace BarberBossI.Communication.Responses;
public class ResponseShortInvoiceJson
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
