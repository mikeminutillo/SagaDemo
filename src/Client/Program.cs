using System;
using System.IO;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "Client";

        var endpointConfiguration = new EndpointConfiguration("Client");
        endpointConfiguration.SendOnly();
        endpointConfiguration.EnableInstallers();

        var transport = endpointConfiguration.UseTransport<LearningTransport>();

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(SubmitOrder), "Sales");
        routing.RouteToEndpoint(typeof(CancelOrder), "Sales");

        var endpoint = await Endpoint.Start(endpointConfiguration);

        PrintMenu(Console.Out);
        
        var done = false;
        while (!done)
        {
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.A:
                    AliceLastOrder = await SendOrder(endpoint, "Alice", 100);
                    break;
                case ConsoleKey.B:
                    BobLastOrder = await SendOrder(endpoint, "Bob", 50);
                    break;
                case ConsoleKey.C:
                    await CancelOrder(endpoint, AliceLastOrder);
                    AliceLastOrder = null;
                    break;
                case ConsoleKey.D:
                    await CancelOrder(endpoint, BobLastOrder);
                    BobLastOrder = null;
                    break;
                case ConsoleKey.R:
                    Console.Clear();
                    PrintMenu(Console.Out);
                    break;
                case ConsoleKey.Escape:
                    done = true;
                    break;
            }
        }

        await endpoint.Stop();
    }

    private static Guid? AliceLastOrder;
    private static Guid? BobLastOrder;

    static async Task<Guid> SendOrder(IMessageSession endpoint, string customerId, int amount)
    {
        var submitOrder = new SubmitOrder
        {
            CustomerId = customerId,
            OrderId = Guid.NewGuid(),
            Value = amount
        };

        await endpoint.Send(submitOrder);

        Console.WriteLine($"Submit ${amount} order for {customerId} - OrderId: {submitOrder.OrderId}");
        
        return submitOrder.OrderId;
    }

    static async Task CancelOrder(IMessageSession endpoint, Guid? orderId)
    {
        if (orderId.HasValue == false)
        {
            return;
        }

        var cancelOrder = new CancelOrder
        {
            OrderId = orderId.Value
        };

        await endpoint.Send(cancelOrder);

        Console.WriteLine($"Cancelled OrderId: {orderId}");
    }

    static void PrintMenu(TextWriter writer)
    {
        writer.WriteLine("A. Send a $100 order for Alice");
        writer.WriteLine("B. Send a $50 order for Bob");

        if (AliceLastOrder.HasValue)
        {
            writer.WriteLine("C. Cancel Alice's last order");
        }

        if (BobLastOrder.HasValue)
        {
            writer.WriteLine("D. Cancel Bob's last order");
        }
        writer.WriteLine("R. Refresh the screen");
        writer.WriteLine("ESC. Quit");
    }
}