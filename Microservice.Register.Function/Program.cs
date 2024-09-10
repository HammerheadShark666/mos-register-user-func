using Azure.Identity;
using FluentValidation;
using MediatR;
using Microservice.Register.Function.Data.Context;
using Microservice.Register.Function.Data.Repository;
using Microservice.Register.Function.Data.Repository.Interfaces;
using Microservice.Register.Function.Helpers;
using Microservice.Register.Function.Helpers.Interfaces;
using Microservice.Register.Function.MediatR.RegisterUser;
using Microservice.Register.Function.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(builder => builder.UseMiddleware<ExceptionHandlerMiddleware>())
    .ConfigureAppConfiguration(c =>
    {
        c.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
         .AddEnvironmentVariables();
    })
    .ConfigureServices(services =>
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>()
                              ?? throw new Exception("Configuration not created.");

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
        services.AddMediatR(_ => _.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAzureServiceBusHelper, AzureServiceBusHelper>();
        services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
        services.AddScoped<IJsonHelper, JsonHelper>();
        services.AddSingleton<DefaultData>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddMemoryCache();
        services.AddApplicationInsightsTelemetry();
        services.AddDbContextFactory<UserDbContext>(options =>

        options.UseSqlServer(configuration.GetConnectionString(Constants.DatabaseConnectionString),
            options => options.EnableRetryOnFailure()));

        services.AddAzureClients(builder =>
        {
            builder.AddServiceBusClientWithNamespace(EnvironmentVariables.GetEnvironmentVariable(Constants.AzureServiceBusConnection));
            builder.UseCredential(new ManagedIdentityCredential());
        });

        services.AddLogging(logging =>
        {
            logging.AddConsole();
        });
    })
    .Build();

await host.RunAsync();
