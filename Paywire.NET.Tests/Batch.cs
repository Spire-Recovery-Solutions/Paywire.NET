// BatchTests.cs
using NUnit.Framework;
using Paywire.NET.Factories;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.BatchInquiry;
using Paywire.NET.Models.CloseBatch;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Paywire.NET.Tests;

[TestFixture]
[Order(4)]
public class BatchTests : BaseTests
{
    [Test, Order(1), Category("Enquiry")]
    public async Task BatchInquiryTest()
    {
        var request = PaywireRequestFactory.BatchInquiry(new TransactionHeader());

        var stopwatch = Stopwatch.StartNew();
        var response = await CLIENT.SendRequest<BatchInquiryResponse>(request);
        stopwatch.Stop();
        
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Success));
    }
    
    [Test, Order(2), Category("Close")]
    public async Task CloseBatchTest()
    {
        var request = PaywireRequestFactory.CloseBatch(
            new TransactionHeader
            {
                PWINVOICENUMBER = INVOICE_NUMBER
            }
        );

        var stopwatch = Stopwatch.StartNew();
        var response = await CLIENT.SendRequest<CloseBatchResponse>(request);
        stopwatch.Stop();
        
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Success));
    }
}