using MediatR;
using Microservice.Register.Function.Data.Repository.Interfaces;
using Microservice.Register.Function.Helpers;
using Microservice.Register.Function.Helpers.Interfaces;
using System.Text.Json;
using static Microservice.Register.Function.Helpers.Enums;
using BC = BCrypt.Net.BCrypt;

namespace Microservice.Register.Function.MediatR.RegisterUser;

public class RegisterUserCommandHandler(IUserRepository userRepository,
                                        IAzureServiceBusHelper azureServiceBusHelper,
                                        IAuthenticationHelper authenticationHelper) : IRequestHandler<RegisterUserRequest, Unit>
{
    public async Task<Unit> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.AddAsync(CreateUserAsync(request));

        var responses = GetSerialisedRegisteredUserResponses(request, user);

        await azureServiceBusHelper.SendMessage(EnvironmentVariables.AzureServiceBusQueueRegisteredUserCustomer, responses.Item1);
        await azureServiceBusHelper.SendMessage(EnvironmentVariables.AzureServiceBusQueueRegisteredUserCustomerAddress, responses.Item2);

        return Unit.Value;
    }

    private Domain.User CreateUserAsync(RegisterUserRequest registerUserRequest)
    {
        return new Domain.User()
        {
            Id = Guid.NewGuid(),
            Email = registerUserRequest.Email,
            Role = Role.User,
            Verified = DateTime.Now,
            VerificationToken = authenticationHelper.CreateRandomToken(),
            PasswordHash = BC.HashPassword(registerUserRequest.Password),
            Password = registerUserRequest.Password,
            ConfirmPassword = registerUserRequest.ConfirmPassword
        };
    }

    private static Tuple<string, string> GetSerialisedRegisteredUserResponses(RegisterUserRequest request, Domain.User user)
    {
        return new Tuple<string, string>(GetSerializedRegisteredUser(user.Id, request),
                                            GetSerializedRegisteredUserAddress(user.Id, request.Address));
    }

    private static string GetSerializedRegisteredUser(Guid id, RegisterUserRequest request)
    {
        return JsonSerializer.Serialize(new RegisterUserResponse(id,
                                                                 request.Email,
                                                                 request.Surname,
                                                                 request.FirstName));
    }

    private static string GetSerializedRegisteredUserAddress(Guid id, RegisterUserAddress registerUserAddress)
    {
        return JsonSerializer.Serialize(new RegisterUserAddressResponse(Guid.Empty,
                                                                        id,
                                                                        registerUserAddress.AddressLine1,
                                                                        registerUserAddress.AddressLine2,
                                                                        registerUserAddress.AddressLine3,
                                                                        registerUserAddress.TownCity,
                                                                        registerUserAddress.County,
                                                                        registerUserAddress.Postcode,
                                                                        registerUserAddress.CountryId));
    }
}