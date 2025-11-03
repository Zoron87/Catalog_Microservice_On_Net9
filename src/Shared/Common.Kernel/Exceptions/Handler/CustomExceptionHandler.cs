using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Kernel.Exceptions.Handler;

public class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken ct)
    {
        var (statusCode, title, detail) = exception switch
        {
            NotFoundException nfe => (StatusCodes.Status404NotFound, "Ресурс не найден", nfe.Message),
            ValidationException ve => (StatusCodes.Status400BadRequest, "Ошибка валидации", ve.Message),
            _ => (StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера", "Произошла непредвиденная ошибка")
        };

        httpContext.Response.StatusCode = statusCode;
        var problemDetails = new ProblemDetails 
        {
            Title = title,
            Detail = detail,
            Status = statusCode,
            Instance = httpContext.Request.Path
        };
        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);


        if (exception is ValidationException vException)
        {
            var errors = vException.Errors?.Select(x => new { x.PropertyName, x.ErrorMessage }).ToList();

            if (errors is not null && errors.Count > 0)
                problemDetails.Extensions.Add("ValidationException", errors);
        }
        else
        {
            problemDetails.Extensions.Add("errorMessage", exception.Message);
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, ct);
        return true;
    }
}
