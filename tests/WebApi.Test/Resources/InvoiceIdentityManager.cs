﻿using BarberBossI.Domain.Entities;

namespace WebApi.Test.Resources;
public class InvoiceIdentityManager
{
    private readonly Invoice _invoice;

    public InvoiceIdentityManager(Invoice invoice)
    {
        _invoice = invoice;
    }

    public long GetId() => _invoice.Id;
    public DateTime GetDate() => _invoice.Date;
}
