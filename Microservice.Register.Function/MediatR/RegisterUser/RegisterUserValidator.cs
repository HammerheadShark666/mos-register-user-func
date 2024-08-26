using FluentValidation;
using Microservice.Register.Function.Data.Repository.Interfaces;

namespace Microservice.Register.Function.MediatR.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(registerUserRequest => registerUserRequest.Email)
                .NotEmpty().WithMessage("Email is required.")
                .Length(8, 150).WithMessage("Email length between 8 and 150.")
                .EmailAddress().WithMessage("Invalid Email.");

        RuleFor(registerUserRequest => registerUserRequest.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(8, 50).WithMessage("Password length between 8 and 50.");

        RuleFor(registerUserRequest => registerUserRequest.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password is required.")
            .Length(8, 50).WithMessage("Confirm Password length between 8 and 50.");

        RuleFor(registerUserRequest => registerUserRequest.Password)
            .Equal(registerUserRequest => registerUserRequest.ConfirmPassword)
            .WithMessage("Password and Confirm Password must be same");

        RuleFor(registerUserRequest => registerUserRequest).MustAsync(async (registerUserRequest, cancellation) =>
        {
            return await EmailExists(registerUserRequest);
        }).WithMessage("User with this email already exists");

        RuleFor(registerUserRequest => registerUserRequest.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .Length(1, 30).WithMessage("Surname length between 1 and 30.");

        RuleFor(registerUserRequest => registerUserRequest.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(1, 30).WithMessage("First name length between 1 and 30.");

        RuleFor(registerUserRequest => registerUserRequest.Address.AddressLine1)
            .NotEmpty().WithMessage("Address Line 1 is required.")
            .Length(1, 50).WithMessage("Address Line 1 length between 1 and 50.");

        RuleFor(registerUserRequest => registerUserRequest.Address.TownCity)
            .NotEmpty().WithMessage("TownCity is required.")
            .Length(1, 50).WithMessage("TownCity length between 1 and 50.");

        RuleFor(registerUserRequest => registerUserRequest.Address.County)
            .NotEmpty().WithMessage("County is required.")
            .Length(1, 50).WithMessage("County length between 1 and 50.");

        RuleFor(registerUserRequest => registerUserRequest.Address.Postcode)
            .NotEmpty().WithMessage("Postcode is required.")
            .Length(6, 8).WithMessage("Postcode length between 6 and 8.");

        RuleFor(registerUserRequest => registerUserRequest.Address.CountryId)
            .NotEmpty().WithMessage("Country Id is required.")
            .GreaterThan(0).WithMessage("Country Id is invalid.");
    }

    protected async Task<bool> EmailExists(RegisterUserRequest registerUserRequest)
    {
        return !await _userRepository.UserExistsAsync(registerUserRequest.Email);
    }
}