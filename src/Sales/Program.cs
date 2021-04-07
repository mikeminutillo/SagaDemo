using NServiceBus;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "Sales";

        var endpointConfiguration = new EndpointConfiguration("Sales");

        endpointConfiguration.UseTransport<LearningTransport>();
        endpointConfiguration.UsePersistence<LearningPersistence>();

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.AuditProcessedMessagesTo("audit");
        endpointConfiguration.AuditSagaStateChanges("audit");

        endpointConfiguration.Recoverability().AddUnrecoverableException<NotImplementedException>();

        var endpoint = await Endpoint.Start(endpointConfiguration);

        while (Console.ReadKey(true).Key != ConsoleKey.Escape)
        {
            
        }

        await endpoint.Stop();
    }
}