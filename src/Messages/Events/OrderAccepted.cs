using System;
using NServiceBus;

public class OrderAccepted : IEvent
{
    public Guid OrderId { get; set; }
    public string CustomerId { get; set; }
    public int Value { get; set; }
}