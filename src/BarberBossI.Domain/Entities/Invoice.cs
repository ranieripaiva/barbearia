﻿using BarberBossI.Domain.Enums;

namespace BarberBossI.Domain.Entities;
public class Invoice
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
}