namespace Microservice.Register.Function.MediatR.RegisterUser;

public record RegisterUserErrorResponse(int Status, string Message, IEnumerable<string> Errors);