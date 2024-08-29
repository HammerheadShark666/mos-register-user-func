using MediatR;
using Microservice.Register.Function.Helpers.Interfaces;
using Microservice.Register.Function.MediatR.RegisterUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Microservice.Register.Function.Functions;

public class Register(ILogger<Register> logger, IMediator mediator, IJsonHelper jsonHelper)
{

    [Function("register")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest request)
    {
        var registerUserRequest = await jsonHelper.GetRegisterUserRequestAsync<RegisterUserRequest>(request);

        await mediator.Send(registerUserRequest);

        logger.LogInformation("Registered User - {registerUserRequest.FirstName} {registerUserRequest.Surname} - {registerUserRequest.Email}", registerUserRequest.FirstName, registerUserRequest.Surname, registerUserRequest.Email);

        return new OkObjectResult("Registration Successful");
    }
}