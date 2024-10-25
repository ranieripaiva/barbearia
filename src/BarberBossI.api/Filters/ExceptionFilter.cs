using BarberBossI.Communication.Responses;
using BarberBossI.Exception;
using BarberBossI.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BarberBossI.api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BarberBossIException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnkowError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var barberBossIException = context.Exception as BarberBossIException;
        var errorResponse = new ResponseErrorJson(barberBossIException!.GetErrors());

        context.HttpContext.Response.StatusCode = barberBossIException.StatusCode;

        context.Result = new ObjectResult(errorResponse);        
    }

    private void ThrowUnkowError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
