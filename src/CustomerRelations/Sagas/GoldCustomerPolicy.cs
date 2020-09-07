//using System;
//using System.Threading.Tasks;
//using NServiceBus;
//using NServiceBus.Logging;

//class GoldCustomerPolicyData : ContainSagaData
//{
//    public string CustomerId { get; set; }
//    public int ValueOfOrdersInWindow { get; set; }
//    public bool OfferSent { get; set; }
//}

//class GoldCustomerPolicyWindowClosed
//{
//    public int Value { get; set; }
//    public Guid OrderId { get; set; }
//}

//class GoldCustomerPolicy : Saga<GoldCustomerPolicyData>, IAmStartedByMessages<OrderAccepted>, IHandleTimeouts<GoldCustomerPolicyWindowClosed>
//{
//    private readonly TimeSpan orderWindow = TimeSpan.FromMinutes(1);
//    private readonly int threshold = 400;

//    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<GoldCustomerPolicyData> mapper)
//    {
//        mapper.MapSaga(saga => saga.CustomerId)
//            .ToMessage<OrderAccepted>(msg => msg.CustomerId);
//    }

//    public async Task Handle(OrderAccepted message, IMessageHandlerContext context)
//    {
//        Data.ValueOfOrdersInWindow += message.Value;

//        log.Info($"Window starting on order {message.OrderId}. Current level for {Data.CustomerId} {Data.ValueOfOrdersInWindow}");

//        if (Data.ValueOfOrdersInWindow >= threshold && Data.OfferSent == false)
//        {
//            log.Info($"Offer Gold Membership to {Data.CustomerId}");
//            Data.OfferSent = true;
//            await context.Send(new OfferGoldMembership
//            {
//                CustomerId = Data.CustomerId
//            });
//        }

//        await RequestTimeout(context, orderWindow, new GoldCustomerPolicyWindowClosed
//        {
//            OrderId = message.OrderId,
//            Value = message.Value
//        });
//    }

//    public Task Timeout(GoldCustomerPolicyWindowClosed state, IMessageHandlerContext context)
//    {
//        Data.ValueOfOrdersInWindow -= state.Value;
//        log.Info($"Window ending on order {state.OrderId}. Current level {Data.CustomerId} {Data.ValueOfOrdersInWindow}");
//        return Task.CompletedTask;
//    }

//    private readonly ILog log = LogManager.GetLogger<GoldCustomerPolicy>();
//}