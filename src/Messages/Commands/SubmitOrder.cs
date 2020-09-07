using System;
using NServiceBus;

public class SubmitOrder : ICommand
{
    public string CustomerId { get; set; }
    public Guid OrderId { get; set; }
    public int Value { get; set; }
}