namespace Microservice.Register.Function.MediatR.RegisterUser;
public record RegisterUserErrorResponse(int status, string Message, IEnumerable<string> Errors);