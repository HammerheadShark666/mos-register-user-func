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
    private ILogger<Register> _logger { get; set; } = logger;
    private IMediator _mediator { get; set; } = mediator;
    private IJsonHelper _jsonHelper { get; set; } = jsonHelper;

    [Function("register")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest request)
    {
        var registerUserRequest = await _jsonHelper.GetRegisterUserRequestAsync<RegisterUserRequest>(request);

        await _mediator.Send(registerUserRequest);

        _logger.LogInformation(string.Format("Registered User - {0} {1} - {2}", registerUserRequest.FirstName, registerUserRequest.Surname, registerUserRequest.Email));

        return new OkObjectResult("Registration Successful");
    }
}