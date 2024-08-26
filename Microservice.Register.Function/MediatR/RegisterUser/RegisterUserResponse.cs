using MediatR;

namespace Microservice.Register.Function.MediatR.RegisterUser;

public record RegisterUserResponse(Guid Id, string Email, string Surname, string FirstName) : IRequest<RegisterUserResponse>;

public record RegisterUserAddressResponse(Guid Id, Guid CustomerId, string AddressLine1, string AddressLine2, string AddressLine3, string TownCity, string County, string Postcode, int CountryId);