using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

class OrderHandler : IHandleMessages<SubmitOrder>, IHandleMessages<CancelOrder>
{
    public async Task Handle(SubmitOrder message, IMessageHandlerContext context)
    {
        log.Info($"Order submitted: {message.OrderId} {message.CustomerId} - ${message.Value}");

        #region Nasty entangled mess
        // Check for stock, maybe backorder?
        // Charge credit card
        // Call shipping service
        // Print shipping label
        #endregion

        await context.Publish(new OrderAccepted
        {
            CustomerId = message.CustomerId,
            OrderId = message.OrderId,
            Value = message.Value
        });
    }

    public Task Handle(CancelOrder message, IMessageHandlerContext context)
    {
        #region Nasty entangled mess
        // Check if we already shipped it
        // Check if we already billed it
        // Check if it's on back order
        // Notify the customer
        #endregion

        throw new NotImplementedException("Help! someone cancelled!");
    }

    private readonly ILog log = LogManager.GetLogger<OrderHandler>();
}


