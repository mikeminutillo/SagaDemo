using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

class SendGoldMembershipOffer : IHandleMessages<OfferGoldMembership>
{
    public Task Handle(OfferGoldMembership message, IMessageHandlerContext context)
    {
        log.Info($"Sending gold membership offer to {message.CustomerId}");

        // TODO: Send gold membership offer email

        return Task.CompletedTask;
    }

    private readonly ILog log = LogManager.GetLogger<SendGoldMembershipOffer>();
}