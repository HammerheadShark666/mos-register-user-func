namespace Microservice.Register.Function.Helpers.Interfaces;

public interface IAuthenticationHelper
{
    string CreateRandomToken();
    string CleanToken(byte[] randomNumber);
}
