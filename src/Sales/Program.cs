using NServiceBus;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "Sales";

        var config = new ConfigurationBuilder().AddJsonFile("local.settings.json", false)
            .Build();

        var endpointConfiguration = new EndpointConfiguration("Sales");
        endpointConfiguration.EnableInstallers();

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        transport.ConnectionString(config.GetValue<string>("Values:AzureWebJobsServiceBus"));

        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.SqlDialect<SqlDialect.MsSqlServer>();
        persistence.ConnectionBuilder(() => new SqlConnection(config.GetValue<string>("Values:NServiceBusData")));

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.AuditProcessedMessagesTo("audit");

        endpointConfiguration.Recoverability().AddUnrecoverableException<NotImplementedException>();

        var endpoint = await Endpoint.Start(endpointConfiguration);

        while (Console.ReadKey(true).Key != ConsoleKey.Escape)
        {
            
        }

        await endpoint.Stop();
    }
}