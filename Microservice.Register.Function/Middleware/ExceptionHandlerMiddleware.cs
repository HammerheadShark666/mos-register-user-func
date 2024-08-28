using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Microservice.Register.Function.Middleware;

public class ExceptionHandlerMiddleware() : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            var logger = context.GetLogger<ExceptionHandlerMiddleware>();

            var req = await context.GetHttpRequestDataAsync();
            var res = req!.CreateResponse();

            switch (exception)
            {
                case ValidationException:

                    var response = new
                    {
                        detail = GetMessage(exception),
                        errors = GetErrors(exception)
                    };

                    req = await context.GetHttpRequestDataAsync();
                    res = req!.CreateResponse();
                    res.StatusCode = HttpStatusCode.BadRequest;
                    await res.WriteStringAsync(JsonSerializer.Serialize(response));
                    context.GetInvocationResult().Value = res;
                    break;

                default:
                    logger.LogError(exception.Message);

                    req = await context.GetHttpRequestDataAsync();
                    res = req!.CreateResponse();
                    res.StatusCode = HttpStatusCode.InternalServerError;

                    await res.WriteStringAsync("Internal service error. Please contact an administrator.");
                    context.GetInvocationResult().Value = res;
                    break;
            }
        }
    }

    private static string GetMessage(Exception exception) =>
        exception switch
        {
            ValidationException => "Validation Error",
            _ => exception.Message
        };

    private static IEnumerable<string> GetErrors(Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            foreach (var error in validationException.Errors)
            {
                yield return error.ErrorMessage;
            }
        }
    }
}