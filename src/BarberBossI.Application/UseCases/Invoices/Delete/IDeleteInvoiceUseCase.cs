﻿namespace BarberBossI.Application.UseCases.Invoices.Delete;
public interface IDeleteInvoiceUseCase
{
    Task Execute(long id);
}
