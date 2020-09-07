using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

class SendDiscountCodeOffer : IHandleMessages<OfferDiscountCode>
{
    public Task Handle(OfferDiscountCode message, IMessageHandlerContext context)
    {
        log.Info($"Sending discount offer to {message.CustomerId}");

        // TODO: Send discount offer email

        return Task.CompletedTask;
    }

    private readonly ILog log = LogManager.GetLogger<SendDiscountCodeOffer>();
}