using Microservice.Register.Function.Helpers.Exceptions;
using Microservice.Register.Function.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Microservice.Register.Function.Helpers;

public class JsonHelper : IJsonHelper
{
    public async Task<T> GetRegisterUserRequestAsync<T>(HttpRequest request) where T : class
    {
        ArgumentNullException.ThrowIfNull("Request is null");

        string requestBody = String.Empty;
        using (StreamReader streamReader = new(request.Body))
        {
            requestBody = await streamReader.ReadToEndAsync();
        }

        return JsonSerializer.Deserialize<T>(requestBody) ?? throw new JsonDeserializeException("Unable to deserialize request body.");
    }
}