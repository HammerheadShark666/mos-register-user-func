using Microservice.Register.Function.Helpers.Exceptions;

namespace Microservice.Register.Function.Helpers;

public class EnvironmentVariables
{
    public static string AzureServiceBusConnection => GetEnvironmentVariable(Constants.AzureServiceBusConnection);
    public static string AzureServiceBusConnectionManagedIdentity => GetEnvironmentVariable(Constants.AzureServiceBusConnectionManagedIdentity);
    public static string AzureServiceBusQueueRegisteredUserCustomer => GetEnvironmentVariable(Constants.AzureServiceBusQueueRegisteredUserCustomer);
    public static string AzureServiceBusQueueRegisteredUserCustomerAddress => GetEnvironmentVariable(Constants.AzureServiceBusQueueRegisteredUserCustomerAddress);

    public static string GetEnvironmentVariable(string name)
    {
        var variable = Environment.GetEnvironmentVariable(name);

        if (string.IsNullOrEmpty(variable))
            throw new EnvironmentVariableNotFoundException($"Environment Variable Not Found: {name}.");

        return variable;
    }
}