using System;
using NServiceBus;

public class OrderCancelled : IEvent
{
    public Guid OrderId { get; set; }
    public string CustomerId { get; set; }
}