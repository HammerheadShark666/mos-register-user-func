using Azure.Identity;
using FluentValidation;
using MediatR;
using Microservice.Register.Function.Data.Context;
using Microservice.Register.Function.Data.Repository;
using Microservice.Register.Function.Data.Repository.Interfaces;
using Microservice.Register.Function.Helpers.Interfaces;
using Microservice.Register.Function.MediatR.RegisterUser;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Microservice.Register.Function.Helpers;

public static class ServiceExtensions
{
    public static void ConfigureDependencyInjection(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAzureServiceBusHelper, AzureServiceBusHelper>();
        services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
        services.AddScoped<IJsonHelper, JsonHelper>();
        services.AddSingleton<DefaultData>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    public static void ConfigureApplicationInsights(IServiceCollection services)
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.AddApplicationInsightsTelemetry();
        services.ConfigureFunctionsApplicationInsights();
    }

    public static void ConfigureMediatr(IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
        services.AddMediatR(_ => _.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
    }

    public static void ConfigureSqlServer(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<UserDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(Constants.DatabaseConnectionString))
        );
    }

    public static void ConfigureMemoryCache(IServiceCollection services)
    {
        services.AddMemoryCache();
    }

    public static void ConfigureServiceBusClient(IServiceCollection services)
    {
        if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == null)
        {
            services.AddAzureClients(builder =>
            {
                builder.AddServiceBusClientWithNamespace(EnvironmentVariables.GetEnvironmentVariable(Constants.AzureServiceBusConnectionManagedIdentity));
                builder.UseCredential(new ManagedIdentityCredential());
            });
        }
        else
        {
            services.AddAzureClients(builder =>
            {
                builder.AddServiceBusClient(EnvironmentVariables.GetEnvironmentVariable(Constants.AzureServiceBusConnection));
            });
        }
    }

    public static void ConfigureLogging(IServiceCollection services)
    {
        services.AddLogging(logging =>
        {
            logging.AddConsole();
        });
    }
}