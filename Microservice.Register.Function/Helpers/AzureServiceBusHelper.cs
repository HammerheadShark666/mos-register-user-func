using Azure.Messaging.ServiceBus;
using Microservice.Register.Function.Helpers.Interfaces;

namespace Microservice.Register.Function.Helpers;

public class AzureServiceBusHelper(ServiceBusClient serviceBusClient) : IAzureServiceBusHelper
{
    public async Task SendMessage(string queue, string data)
    {
        var sender = serviceBusClient.CreateSender(queue);
        await sender.SendMessageAsync(new ServiceBusMessage(data));
    }
}