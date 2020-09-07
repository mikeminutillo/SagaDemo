using System;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus.Testing;
using NUnit.Framework;

[TestFixture]
public class During_buyers_remorse
{
    //[Test]
    //public async Task Can_cancel_before_timeout()
    //{
    //    var context = new TestableMessageHandlerContext();

    //    var orderId = Guid.NewGuid();

    //    var policy = new BuyersRemorsePolicy
    //    {
    //        Data = new BuyersRemorsePolicyData
    //        {
    //            OrderId = orderId
    //        }
    //    };

    //    await policy.Handle(new CancelOrder {OrderId = orderId}, context);

    //    Assert.IsTrue(policy.Data.Cancelled, "Order should be cancelled");
    //    Assert.IsFalse(policy.Data.Accepted, "Order should not be accepted");
    //}

    //[Test]
    //public void Cannot_cancel_after_accepted()
    //{
    //    var context = new TestableMessageHandlerContext();

    //    var orderId = Guid.NewGuid();

    //    var policy = new BuyersRemorsePolicy
    //    {
    //        Data = new BuyersRemorsePolicyData
    //        {
    //            OrderId = orderId, 
    //            Accepted = true
    //        }
    //    };

    //    var exception = Assert.ThrowsAsync<NotImplementedException>(
    //        async () => await policy.Handle(new CancelOrder() {OrderId = orderId}, context)
    //    );
    //}
}
