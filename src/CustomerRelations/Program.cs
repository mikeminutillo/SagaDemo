using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NServiceBus;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "Customer Relations";

        var config = new ConfigurationBuilder().AddJsonFile("local.settings.json", false)
            .Build();

        var endpointConfiguration = new EndpointConfiguration("CustomerRelations");
        endpointConfiguration.EnableInstallers();

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        transport.ConnectionString(config.GetValue<string>("Values:AzureWebJobsServiceBus"));
        
        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(OfferDiscountCode), "CustomerRelations");
        routing.RouteToEndpoint(typeof(OfferGoldMembership), "CustomerRelations");

        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.SqlDialect<SqlDialect.MsSqlServer>();
        persistence.ConnectionBuilder(() => new SqlConnection(config.GetValue<string>("Values:NServiceBusData")));

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.AuditProcessedMessagesTo("audit");

        var endpoint = await Endpoint.Start(endpointConfiguration);

        while (Console.ReadKey(true).Key != ConsoleKey.Escape)
        {

        }

        await endpoint.Stop();
    }
}