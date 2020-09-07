using System;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "Customer Relations";

        var endpointConfiguration = new EndpointConfiguration("CustomerRelations");

        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        
        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(OfferDiscountCode), "CustomerRelations");
        routing.RouteToEndpoint(typeof(OfferGoldMembership), "CustomerRelations");

        endpointConfiguration.UsePersistence<LearningPersistence>();

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.AuditProcessedMessagesTo("audit");

        var endpoint = await Endpoint.Start(endpointConfiguration);

        while (Console.ReadKey(true).Key != ConsoleKey.Escape)
        {

        }

        await endpoint.Stop();
    }
}