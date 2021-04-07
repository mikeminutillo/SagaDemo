//using System;
//using System.Threading.Tasks;
//using NServiceBus;
//using NServiceBus.Logging;

//public class BuyersRemorsePolicyData : ContainSagaData
//{
//    public Guid OrderId { get; set; }
//    public bool Cancelled { get; set; }
//    public bool Accepted { get; set; }
//}

//public class BuyersRemorseIsOver
//{
//    public string CustomerId { get; set; }
//    public int Value { get; set; }
//}

//public class BuyersRemorsePolicy : Saga<BuyersRemorsePolicyData>,
//    IAmStartedByMessages<SubmitOrder>,
//    IAmStartedByMessages<CancelOrder>,
//    IHandleTimeouts<BuyersRemorseIsOver>
//{
//    private readonly TimeSpan buyersRemorseWindow = TimeSpan.FromSeconds(10);

//    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<BuyersRemorsePolicyData> mapper)
//    {
//        mapper.MapSaga(saga => saga.OrderId)
//            .ToMessage<SubmitOrder>(msg => msg.OrderId)
//            .ToMessage<CancelOrder>(msg => msg.OrderId);
//    }

//    public async Task Handle(SubmitOrder message, IMessageHandlerContext context)
//    {
//        log.Info($"Order submitted: {message.OrderId}. Starting buyer's remorse cooldown.");

//        await RequestTimeout(context, buyersRemorseWindow, new BuyersRemorseIsOver
//        {
//            CustomerId = message.CustomerId,
//            Value = message.Value
//        });
//    }

//    public async Task Timeout(BuyersRemorseIsOver state, IMessageHandlerContext context)
//    {
//        log.Info($"Buyer's Remorse period ended: {Data.OrderId} {state.CustomerId} - ${state.Value}");

//        if (Data.Cancelled)
//        {
//            return;
//        }

//        Data.Accepted = true;

//        #region Nasty entangled mess
//        // Check for stock, maybe backorder?
//        // Charge credit card
//        // Call shipping service
//        // Print shipping label
//        #endregion

//        await context.Publish(new OrderAccepted
//        {
//            CustomerId = state.CustomerId,
//            OrderId = Data.OrderId
//        });

//    }


//    public async Task Handle(CancelOrder message, IMessageHandlerContext context)
//    {
//        if (Data.Accepted)
//        {
//            throw new NotImplementedException("This requires manual intervention for now");
//        }

//        if (Data.Cancelled)
//        {
//            return;
//        }

//        #region Nasty entangled mess
//        // Check if we already shipped it
//        // Check if we already billed it
//        // Check if it's on back order
//        // Notify the customer
//        #endregion

//        Data.Cancelled = true;

//        await context.Publish(new OrderCancelled
//        {
//            OrderId = Data.OrderId
//        });
//    }

//    private readonly ILog log = LogManager.GetLogger<BuyersRemorsePolicy>();
//}