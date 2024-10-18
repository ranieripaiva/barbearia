﻿using System.Net;

namespace BarberBossI.Exception.ExceptionsBase;
public class NotFoundException : BarberBossIException
{
    public NotFoundException(string message) : base(message)
    {
        
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
