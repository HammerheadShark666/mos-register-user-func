using Microservice.Register.Function.Helpers.Exceptions;
using Microservice.Register.Function.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Microservice.Register.Function.Helpers;

public class JsonHelper : IJsonHelper
{
    public async Task<T> GetRegisterUserRequestAsync<T>(HttpRequest request) where T : class
    {
        string requestBody = String.Empty;
        using (StreamReader streamReader = new(request.Body))
        {
            requestBody = await streamReader.ReadToEndAsync();
        }

        var registerUserRequest = JsonSerializer.Deserialize<T>(requestBody);

        if (registerUserRequest == null)
        {
            throw new BadRequestException("Unable to deserialize request body.");
        }
        else
        {
            return registerUserRequest;
        }
    }
}