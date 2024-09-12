using Microservice.Register.Function.Helpers;
using Microservice.Register.Function.Middleware;
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
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>()
                              ?? throw new Exception("Configuration not created.");

        ServiceExtensions.ConfigureApplicationInsights(services);
        ServiceExtensions.ConfigureMediatr(services);
        ServiceExtensions.ConfigureDependencyInjection(services);
        ServiceExtensions.ConfigureMemoryCache(services);
        ServiceExtensions.ConfigureSqlServer(services, configuration);
        ServiceExtensions.ConfigureServiceBusClient(services);
        ServiceExtensions.ConfigureLogging(services);
    })
    .Build();

await host.RunAsync();