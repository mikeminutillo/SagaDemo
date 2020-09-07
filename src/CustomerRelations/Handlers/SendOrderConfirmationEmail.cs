using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

class SendOrderConfirmationEmail : IHandleMessages<OrderAccepted>
{
    public Task Handle(OrderAccepted message, IMessageHandlerContext context)
    {
        log.Info($"Sending order confirmation email to {message.CustomerId} for order {message.OrderId}");

        // TODO: Talk to email service

        return Task.CompletedTask;
    }

    private ILog log = LogManager.GetLogger<SendOrderConfirmationEmail>();
}