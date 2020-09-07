using NServiceBus;

public class OfferDiscountCode : ICommand
{
    public string CustomerId { get; set; }
}

public class OfferGoldMembership : ICommand
{
    public string CustomerId { get; set; }
}