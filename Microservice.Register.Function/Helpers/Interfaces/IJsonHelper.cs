using Microsoft.AspNetCore.Http;

namespace Microservice.Register.Function.Helpers.Interfaces;

public interface IJsonHelper
{
    Task<T> GetRegisterUserRequestAsync<T>(HttpRequest request) where T : class;
}
