using Azure.Identity;
using FluentValidation;
using MediatR;
using Microservice.Register.Function.Data.Context;
using Microservice.Register.Function.Data.Repository;
using Microservice.Register.Function.Data.Repository.Interfaces;
using Microservice.Register.Function.Helpers.Exceptions;
using Microservice.Register.Function.Helpers.Interfaces;
using Microservice.Register.Function.Helpers.Providers;
using Microservice.Register.Function.MediatR.RegisterUser;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Microservice.Register.Function.Helpers.Extensions;

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

    public static void ConfigureSqlServer(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        if (environment.IsProduction())
        {
            var connectionString = configuration.GetConnectionString(Constants.AzureDatabaseConnectionString)
                    ?? throw new DatabaseConnectionStringNotFound("Production database connection string not found.");

            AddDbContextFactory(services, SqlAuthenticationMethod.ActiveDirectoryManagedIdentity, new ProductionAzureSQLProvider(), connectionString);
        }
        else if (environment.IsDevelopment())
        {
            var connectionString = configuration.GetConnectionString(Constants.LocalDatabaseConnectionString)
                    ?? throw new DatabaseConnectionStringNotFound("Development database connection string not found.");

            AddDbContextFactory(services, SqlAuthenticationMethod.ActiveDirectoryServicePrincipal, new DevelopmentAzureSQLProvider(), connectionString);
        }
    }

    public static void ConfigureMemoryCache(IServiceCollection services)
    {
        services.AddMemoryCache();
    }

    public static void ConfigureServiceBusClient(IServiceCollection services, IWebHostEnvironment environment)
    {
        if (environment.IsProduction())
        {
            services.AddAzureClients(builder =>
            {
                builder.AddServiceBusClientWithNamespace(EnvironmentVariables.AzureServiceBusConnectionManagedIdentity);
                builder.UseCredential(new ManagedIdentityCredential());
            });
        }
        else if (environment.IsDevelopment())
        {
            services.AddAzureClients(builder =>
            {
                builder.AddServiceBusClient(EnvironmentVariables.AzureServiceBusConnection);
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

    private static void AddDbContextFactory(IServiceCollection services, SqlAuthenticationMethod sqlAuthenticationMethod, SqlAuthenticationProvider sqlAuthenticationProvider, string connectionString)
    {
        services.AddDbContextFactory<UserDbContext>(options =>
        {
            SqlAuthenticationProvider.SetProvider(
                    sqlAuthenticationMethod,
                    sqlAuthenticationProvider);
            var sqlConnection = new SqlConnection(connectionString);
            options.UseSqlServer(sqlConnection);
        });
    }
}