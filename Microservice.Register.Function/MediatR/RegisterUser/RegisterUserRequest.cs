using MediatR;

namespace Microservice.Register.Function.MediatR.RegisterUser;

public record RegisterUserRequest(string Email, string Surname, string FirstName, string Password, string ConfirmPassword, RegisterUserAddress Address) : IRequest<Unit>;

public record RegisterUserAddress(string AddressLine1, string AddressLine2, string AddressLine3, string TownCity, string County, string Postcode, int CountryId);