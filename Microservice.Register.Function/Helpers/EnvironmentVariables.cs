namespace Microservice.Register.Function.Helpers;

public class EnvironmentVariables
{
    public static string AzureServiceBusConnection => Environment.GetEnvironmentVariable(Constants.AzureServiceBusConnection);
    public static string AzureServiceBusQueueRegisteredUserCustomer => Environment.GetEnvironmentVariable(Constants.AzureServiceBusQueueRegisteredUserCustomer);
    public static string AzureServiceBusQueueRegisteredUserCustomerAddress => Environment.GetEnvironmentVariable(Constants.AzureServiceBusQueueRegisteredUserCustomerAddress);
}