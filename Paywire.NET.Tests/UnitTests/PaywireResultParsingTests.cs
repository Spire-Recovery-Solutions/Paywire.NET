using NUnit.Framework;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Tests.UnitTests;

[TestFixture]
public class PaywireResultParsingTests
{
    private static BasePaywireResponse CreateResponse(string rawResult)
    {
        return new BasePaywireResponse { RAW_RESULT = rawResult };
    }

    [TestCase("ERROR", PaywireResult.Error)]
    [TestCase("APPROVAL", PaywireResult.Approval)]
    [TestCase("DECLINED", PaywireResult.Declined)]
    [TestCase("CAPTURED", PaywireResult.Captured)]
    [TestCase("CHARGEBACK", PaywireResult.Chargeback)]
    [TestCase("SETTLED", PaywireResult.Settled)]
    [TestCase("VOIDED", PaywireResult.Voided)]
    [TestCase("REJECTED", PaywireResult.Rejected)]
    [TestCase("PENDING", PaywireResult.Pending)]
    [TestCase("SUCCESS", PaywireResult.Success)]
    public void ResultParsing_KnownValues(string raw, PaywireResult expected)
    {
        var response = CreateResponse(raw);
        Assert.That(response.RESULT, Is.EqualTo(expected));
    }

    [Test]
    public void ResultParsing_ApprovedFallback()
    {
        var response = CreateResponse("APPROVED");
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }

    [Test]
    public void ResultParsing_CaseInsensitive()
    {
        var response = CreateResponse("approval");
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }

    [Test]
    public void ResultParsing_UnknownValue()
    {
        var response = CreateResponse("FOOBAR");
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Unknown));
    }

    [Test]
    public void ResultParsing_EmptyString()
    {
        var response = CreateResponse("");
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Unknown));
    }
}
