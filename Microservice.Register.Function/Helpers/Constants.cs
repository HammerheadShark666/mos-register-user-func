namespace Microservice.Register.Function.Helpers;

public class Constants
{
    public const string AzureServiceBusConnectionManagedIdentity = "ServiceBusConnection__fullyQualifiedNamespace";
    public const string AzureServiceBusConnection = "AZURE_SERVICE_BUS_CONNECTION";
    public const string AzureServiceBusQueueRegisteredUserCustomer = "AZURE_SERVICE_BUS_QUEUE_CUSTOMER";
    public const string AzureServiceBusQueueRegisteredUserCustomerAddress = "AZURE_SERVICE_BUS_QUEUE_CUSTOMER_ADDRESS";

    public const string AzureUserAssignedManagedIdentityClientId = "AZURE_USER_ASSIGNED_MANAGED_IDENTITY_CLIENT_ID";
    public const string AzureDatabaseConnectionString = "AZURE_MANAGED_IDENTITY_SQL_CONNECTION";

    public const string AzureLocalDevelopmentClientId = "AZURE_LOCAL_DEVELOPMENT_CLIENT_ID";
    public const string AzureLocalDevelopmentClientSecret = "AZURE_LOCAL_DEVELOPMENT_CLIENT_SECRET";
    public const string AzureLocalDevelopmentTenantId = "AZURE_LOCAL_DEVELOPMENT_TENANT_ID";
    public const string LocalDatabaseConnectionString = "LOCAL_CONNECTION";
}