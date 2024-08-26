using MediatR;
using Microservice.Register.Function.Helpers;
using Microservice.Register.Function.Helpers.Interfaces;
using Microservice.Register.Function.MediatR.RegisterUser;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System.Text;

namespace Microservice.Register.Function.Test.Unit;

public class RegisterAzureFunctionTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly Mock<ILogger<Functions.Register>> _mockLogger;
    private readonly Functions.Register _register;
    private readonly IJsonHelper _jsonHelper;

    public RegisterAzureFunctionTests()
    {
        _jsonHelper = new JsonHelper();
        _mockMediator = new Mock<IMediator>();
        _mockLogger = new Mock<ILogger<Functions.Register>>();
        _register = new Functions.Register(_mockLogger.Object, _mockMediator.Object, _jsonHelper);
    }

    [Test]
    public async Task Azure_function_register_user_return_succeed()
    {
        var registerRequest = new RegisterUserRequest("ValidEmail@hotmail.com", "TestSurname", "TestFirstName",
                                                      "Password1", "Password1",
                                                      new RegisterUserAddress("AddressLine1", "AddressLine2", "AddressLine3",
                                                                              "TownCity1", "County1", "Postcode1", 1));

        using var requestStream = new MemoryStream();
        requestStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(registerRequest)));
        requestStream.Flush();
        requestStream.Position = 0;

        var mockHttpRequest = new Mock<HttpRequest>();
        mockHttpRequest.Setup(x => x.Body).Returns(requestStream);

        Functions.Register registerFunction = new(_mockLogger.Object, _mockMediator.Object, _jsonHelper);
        await registerFunction.Run(mockHttpRequest.Object);
    }
}