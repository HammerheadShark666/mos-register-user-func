using Microservice.Register.Function.Helpers.Extensions;
using Microservice.Register.Function.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(builder => builder.UseMiddleware<ExceptionHandlerMiddleware>())
    .ConfigureAppConfiguration(c =>
    {
        c.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
         .AddEnvironmentVariables();
    })
    .ConfigureServices(services =>
    {
        var builder = WebApplication.CreateBuilder(args);
        var environment = builder.Environment;

        var configuration = services.BuildServiceProvider().GetService<IConfiguration>()
                              ?? throw new Exception("Configuration not created.");

        ServiceExtensions.ConfigureApplicationInsights(services);
        ServiceExtensions.ConfigureMediatr(services);
        ServiceExtensions.ConfigureDependencyInjection(services);
        ServiceExtensions.ConfigureMemoryCache(services);
        ServiceExtensions.ConfigureSqlServer(services, configuration, environment);
        ServiceExtensions.ConfigureServiceBusClient(services, environment);
        ServiceExtensions.ConfigureLogging(services);
    })
    .Build();

await host.RunAsync();