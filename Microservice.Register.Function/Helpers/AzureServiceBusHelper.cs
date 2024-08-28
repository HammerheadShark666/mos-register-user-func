using Azure.Messaging.ServiceBus;
using Microservice.Register.Function.Helpers.Interfaces;

namespace Microservice.Register.Function.Helpers;

public class AzureServiceBusHelper() : IAzureServiceBusHelper
{
    public async Task SendMessage(string queue, string data)
    {
        var client = new ServiceBusClient(EnvironmentVariables.AzureServiceBusConnection);
        var sender = client.CreateSender(queue);

        await sender.SendMessageAsync(new ServiceBusMessage(data));
    }
}