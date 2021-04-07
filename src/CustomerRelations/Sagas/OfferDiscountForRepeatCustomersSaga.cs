//using System.Threading.Tasks;
//using NServiceBus;
//using NServiceBus.Logging;

//class OfferDiscountForRepeatCustomersData : ContainSagaData
//{
//    public string CustomerId { get; set; }
//    public bool DiscountOfferSent { get; set; }
//    public int OrderCount { get; set; }
//}

//class OfferDiscountForRepeatCustomersSaga : Saga<OfferDiscountForRepeatCustomersData>, 
//    IAmStartedByMessages<OrderAccepted>
//{
//    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OfferDiscountForRepeatCustomersData> mapper)
//    {
//        mapper.MapSaga(saga => saga.CustomerId)
//            .ToMessage<OrderAccepted>(msg => msg.CustomerId);
//    }

//    public async Task Handle(OrderAccepted message, IMessageHandlerContext context)
//    {
//        Data.OrderCount++;
//        log.Info($"Accepted {Data.OrderCount} orders from {Data.CustomerId}");

//        if (Data.OrderCount >= 5 && Data.DiscountOfferSent == false)
//        {
//            log.Info($"{Data.CustomerId} has exceeded the threshold required to receive a discount offer. Sending...");
//            await context.Send(new OfferDiscountCode { CustomerId = Data.CustomerId });
//            Data.DiscountOfferSent = true;
//        }
//    }

//    private readonly ILog log = LogManager.GetLogger<OfferDiscountForRepeatCustomersSaga>();
//}