using FluentValidation;
using MediatR;
using Microservice.Register.Function.Data.Repository.Interfaces;
using Microservice.Register.Function.Domain;
using Microservice.Register.Function.Helpers;
using Microservice.Register.Function.Helpers.Interfaces;
using Microservice.Register.Function.MediatR.RegisterUser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Microservice.Register.Function.Test.Unit;

[TestFixture]
public class RegisterMediatrTests
{
    private readonly Mock<IUserRepository> userRepositoryMock = new();
    private readonly Mock<IAzureServiceBusHelper> azureServiceBusHelperMock = new();
    private readonly ServiceCollection services = new();
    private ServiceProvider serviceProvider;
    private IMediator mediator;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RegisterUserCommandHandler).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        services.AddScoped<IUserRepository>(sp => userRepositoryMock.Object);
        services.AddScoped<IAzureServiceBusHelper>(sp => azureServiceBusHelperMock.Object);
        services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
        services.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));

        serviceProvider = services.BuildServiceProvider();
        mediator = serviceProvider.GetRequiredService<IMediator>();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        services.Clear();
        serviceProvider.Dispose();
    }

    [Test]
    public async Task User_registered_return_success_message()
    {
        var user = new Domain.User()
        {
            Id = Guid.NewGuid(),
            Email = "ValidEmail@hotmail.com",
            Password = "Password1",
            ConfirmPassword = "Password1"
        };

        userRepositoryMock
                .Setup(x => x.UserExistsAsync("ValidEmail@hotmail.com"))
                .Returns(Task.FromResult(false));

        userRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<User>()))
                .Returns(Task.FromResult(user));

        azureServiceBusHelperMock
            .Setup(x => x.SendMessage(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.FromResult(default(object)));

        var registerUserRequest = new RegisterUserRequest("ValidEmail@hotmail.com", "TestSurname", "TestFirstName",
                                                          "Password1", "Password1",
                                                           new RegisterUserAddress("AddressLine1", "AddressLine2", "AddressLine3",
                                                                                   "TownCity1", "County1", "Pcode1", 1));

        await mediator.Send(registerUserRequest);
    }

    [Test]
    public void User_not_added_email_exists_return_exception_fail_message()
    {
        userRepositoryMock
                .Setup(x => x.UserExistsAsync("InvalidEmail@hotmail.com"))
                .Returns(Task.FromResult(true));

        var command = new RegisterUserRequest("InvalidEmail@hotmail.com", "TestSurname", "TestFirstName",
                                              "Password1", "Password1",
                                              new RegisterUserAddress("AddressLine1", "AddressLine2", "AddressLine3",
                                                                      "TownCity1", "County1", "Pcode1", 1));

        var validationException = Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await mediator.Send(command);
        });

        Assert.That(validationException.Errors.Count, Is.EqualTo(1));
        Assert.That(validationException.Errors.ElementAt(0).ErrorMessage, Is.EqualTo("User with this email already exists"));
    }

    [Test]
    public void User_not_added_invalid_email_return_exception_fail_message()
    {
        userRepositoryMock
                .Setup(x => x.UserExistsAsync("InvalidEmail"))
                .Returns(Task.FromResult(false));

        var command = new RegisterUserRequest("InvalidEmail", "TestSurname", "TestFirstName",
                                              "Password1", "Password1",
                                              new RegisterUserAddress("AddressLine1", "AddressLine2", "AddressLine3",
                                                                      "TownCity1", "County1", "Pcode1", 1));

        var validationException = Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await mediator.Send(command);
        });

        Assert.That(validationException.Errors.Count, Is.EqualTo(1));
        Assert.That(validationException.Errors.ElementAt(0).ErrorMessage, Is.EqualTo("Invalid Email."));
    }

    [Test]
    public void User_not_added_invalid_surname_firstname_return_exception_fail_message()
    {
        userRepositoryMock
                .Setup(x => x.UserExistsAsync("ValidEmail@hotmail.com"))
                .Returns(Task.FromResult(false));

        var command = new RegisterUserRequest("ValidEmail@hotmail.com", "TestSurnameTestSurnameTestSurnameTestSurnameTestSurnameTestSurnameTestSurname",
                                              "TestFirstNameTestSurnameTestSurnameTestSurnameTestSurnameTestSurnameTestSurnameTestSurname",
                                              "Password1", "Password1",
                                              new RegisterUserAddress("AddressLine1", "AddressLine2", "AddressLine3",
                                                                      "TownCity1", "County1", "Pcode1", 1));

        var validationException = Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await mediator.Send(command);
        });

        Assert.Multiple(() =>
        {
            Assert.That(validationException.Errors.Count, Is.EqualTo(2));
            Assert.That(validationException.Errors.ElementAt(0).ErrorMessage, Is.EqualTo("Surname length between 1 and 30."));
            Assert.That(validationException.Errors.ElementAt(1).ErrorMessage, Is.EqualTo("First name length between 1 and 30."));
        });
    }

    [Test]
    public void User_not_added_no_email_surname_firstname_return_exception_fail_message()
    {
        userRepositoryMock
                .Setup(x => x.UserExistsAsync("ValidEmail@hotmail.com"))
                .Returns(Task.FromResult(false));

        var command = new RegisterUserRequest("", "", "", "", "",
                                              new RegisterUserAddress("AddressLine1", "AddressLine2", "AddressLine3",
                                                                      "TownCity1", "County1", "Pcode1", 1));

        var validationException = Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await mediator.Send(command);
        });

        Assert.Multiple(() =>
        {
            Assert.That(validationException.Errors.Count, Is.EqualTo(11));
            Assert.That(validationException.Errors.ElementAt(0).ErrorMessage, Is.EqualTo("Email is required."));
            Assert.That(validationException.Errors.ElementAt(1).ErrorMessage, Is.EqualTo("Email length between 8 and 150."));
            Assert.That(validationException.Errors.ElementAt(2).ErrorMessage, Is.EqualTo("Invalid Email."));
            Assert.That(validationException.Errors.ElementAt(3).ErrorMessage, Is.EqualTo("Password is required."));
            Assert.That(validationException.Errors.ElementAt(4).ErrorMessage, Is.EqualTo("Password length between 8 and 50."));
            Assert.That(validationException.Errors.ElementAt(5).ErrorMessage, Is.EqualTo("Confirm Password is required."));
            Assert.That(validationException.Errors.ElementAt(6).ErrorMessage, Is.EqualTo("Confirm Password length between 8 and 50."));
            Assert.That(validationException.Errors.ElementAt(7).ErrorMessage, Is.EqualTo("Surname is required."));
            Assert.That(validationException.Errors.ElementAt(8).ErrorMessage, Is.EqualTo("Surname length between 1 and 30."));
            Assert.That(validationException.Errors.ElementAt(9).ErrorMessage, Is.EqualTo("First name is required."));
            Assert.That(validationException.Errors.ElementAt(10).ErrorMessage, Is.EqualTo("First name length between 1 and 30."));
        });
    }

    [Test]
    public void User_not_added_no_address_return_exception_fail_message()
    {
        userRepositoryMock
                .Setup(x => x.UserExistsAsync("ValidEmail@hotmail.com"))
                .Returns(Task.FromResult(false));

        var command = new RegisterUserRequest("ValidEmail@hotmail.com", "TestSurname", "TestFirstName",
                                              "Password1", "Password1",
                                              new RegisterUserAddress("", "", "",
                                                                      "", "", "", 0));

        var validationException = Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await mediator.Send(command);
        });

        Assert.Multiple(() =>
        {
            Assert.That(validationException.Errors.Count, Is.EqualTo(10));
            Assert.That(validationException.Errors.ElementAt(0).ErrorMessage, Is.EqualTo("Address Line 1 is required."));
            Assert.That(validationException.Errors.ElementAt(1).ErrorMessage, Is.EqualTo("Address Line 1 length between 1 and 50."));
            Assert.That(validationException.Errors.ElementAt(2).ErrorMessage, Is.EqualTo("TownCity is required."));
            Assert.That(validationException.Errors.ElementAt(3).ErrorMessage, Is.EqualTo("TownCity length between 1 and 50."));
            Assert.That(validationException.Errors.ElementAt(4).ErrorMessage, Is.EqualTo("County is required."));
            Assert.That(validationException.Errors.ElementAt(5).ErrorMessage, Is.EqualTo("County length between 1 and 50."));
            Assert.That(validationException.Errors.ElementAt(6).ErrorMessage, Is.EqualTo("Postcode is required."));
            Assert.That(validationException.Errors.ElementAt(7).ErrorMessage, Is.EqualTo("Postcode length between 6 and 8."));
            Assert.That(validationException.Errors.ElementAt(8).ErrorMessage, Is.EqualTo("Country Id is required."));
            Assert.That(validationException.Errors.ElementAt(9).ErrorMessage, Is.EqualTo("Country Id is invalid."));
        });

    }

    [Test]
    public void User_not_added_invalid_address_return_exception_fail_message()
    {
        userRepositoryMock
                .Setup(x => x.UserExistsAsync("ValidEmail@hotmail.com"))
                .Returns(Task.FromResult(false));

        var command = new RegisterUserRequest("ValidEmail@hotmail.com", "TestSurname", "TestFirstName",
                                              "Password1", "Password1",
                                              new RegisterUserAddress("AddressLine1AddressLine1AddressLine1AddressLine1AddressLine1",
                                                                      "AddressLine2", "AddressLine3",
                                                                      "TownCityTownCityTownCityTownCityTownCityTownCityTownCityTownCity",
                                                                      "CountyCountyCountyCountyCountyCountyCountyCountyCounty",
                                                                      "PostcodePostcodePostcodePostcodePostcodePostcodePostcode", 16));

        var validationException = Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await mediator.Send(command);
        });

        Assert.Multiple(() =>
        {
            Assert.That(validationException.Errors.Count, Is.EqualTo(4));
            Assert.That(validationException.Errors.ElementAt(0).ErrorMessage, Is.EqualTo("Address Line 1 length between 1 and 50."));
            Assert.That(validationException.Errors.ElementAt(1).ErrorMessage, Is.EqualTo("TownCity length between 1 and 50."));
            Assert.That(validationException.Errors.ElementAt(2).ErrorMessage, Is.EqualTo("County length between 1 and 50."));
            Assert.That(validationException.Errors.ElementAt(3).ErrorMessage, Is.EqualTo("Postcode length between 6 and 8."));
        });
    }
}