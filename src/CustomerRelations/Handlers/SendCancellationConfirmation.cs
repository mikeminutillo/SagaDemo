using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

class SendCancellationConfirmation : IHandleMessages<OrderCancelled>
{
    public Task Handle(OrderCancelled message, IMessageHandlerContext context)
    {
        log.Info($"Sending confirmation of order cancellation to {message.CustomerId} for order {message.OrderId}");

        // TODO: Actually send the email

        return Task.CompletedTask;
    }

    private readonly ILog log = LogManager.GetLogger<SendCancellationConfirmation>();
}