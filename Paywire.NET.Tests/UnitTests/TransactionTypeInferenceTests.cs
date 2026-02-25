using NUnit.Framework;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.BatchInquiry;
using Paywire.NET.Models.BinValidation;
using Paywire.NET.Models.Capture;
using Paywire.NET.Models.CloseBatch;
using Paywire.NET.Models.Credit;
using Paywire.NET.Models.GetAuthToken;
using Paywire.NET.Models.GetConsumerFee;
using Paywire.NET.Models.PreAuth;
using Paywire.NET.Models.Receipt;
using Paywire.NET.Models.RemoveToken;
using Paywire.NET.Models.Sale;
using Paywire.NET.Models.SearchChargebacks;
using Paywire.NET.Models.SearchTransactions;
using Paywire.NET.Models.StoreToken;
using Paywire.NET.Models.TokenSale;
using Paywire.NET.Models.Verification;
using Paywire.NET.Models.Void;

namespace Paywire.NET.Tests.UnitTests;

[TestFixture]
public class TransactionTypeInferenceTests
{
    /// <summary>
    /// Mirrors the switch expression in PaywireClient.SendRequest (lines 84-104)
    /// to verify the expected transaction type mapping for each request type.
    /// </summary>
    private static string? GetExpectedTransactionType(BasePaywireRequest request) => request switch
    {
        GetAuthTokenRequest => PaywireTransactionType.GetAuthToken,
        VerificationRequest => PaywireTransactionType.Verification,
        GetConsumerFeeRequest => PaywireTransactionType.GetConsumerFee,
        SaleRequest => PaywireTransactionType.Sale,
        BatchInquiryRequest => PaywireTransactionType.BatchInquiry,
        SearchTransactionsRequest => PaywireTransactionType.SearchTransactions,
        CreditRequest => PaywireTransactionType.Credit,
        PreAuthRequest => PaywireTransactionType.PreAuth,
        VoidRequest => PaywireTransactionType.Void,
        StoreTokenRequest => PaywireTransactionType.StoreToken,
        TokenSaleRequest => PaywireTransactionType.Sale,
        SendReceiptRequest => PaywireTransactionType.SendReceipt,
        SearchChargebackRequest => PaywireTransactionType.SearchChargeback,
        BinValidationRequest => PaywireTransactionType.BinValidation,
        CloseBatchRequest => PaywireTransactionType.CloseBatch,
        RemoveTokenRequest => PaywireTransactionType.RemoveToken,
        CaptureRequest => PaywireTransactionType.Capture,
        _ => null
    };

    private static TransactionHeader MakeHeader() => new();

    [Test]
    public void AllRequestTypes_HaveCorrectMapping()
    {
        var testCases = new (BasePaywireRequest request, string expected)[]
        {
            (new GetAuthTokenRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.GetAuthToken),
            (new VerificationRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.Verification),
            (new GetConsumerFeeRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.GetConsumerFee),
            (new SaleRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.Sale),
            (new BatchInquiryRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.BatchInquiry),
            (new SearchTransactionsRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.SearchTransactions),
            (new CreditRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.Credit),
            (new PreAuthRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.PreAuth),
            (new VoidRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.Void),
            (new StoreTokenRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.StoreToken),
            (new TokenSaleRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.Sale),
            (new SendReceiptRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.SendReceipt),
            (new SearchChargebackRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.SearchChargeback),
            (new BinValidationRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.BinValidation),
            (new CloseBatchRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.CloseBatch),
            (new RemoveTokenRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.RemoveToken),
            (new CaptureRequest { TRANSACTION_HEADER = MakeHeader() }, PaywireTransactionType.Capture),
        };

        foreach (var (request, expected) in testCases)
        {
            var actual = GetExpectedTransactionType(request);
            Assert.That(actual, Is.EqualTo(expected),
                $"Mapping failed for {request.GetType().Name}: expected {expected}, got {actual}");
        }
    }

    [Test]
    public void TokenSaleRequest_MapsToSale_NotTokenSale()
    {
        // TokenSaleRequest maps to PaywireTransactionType.Sale (not a separate "TOKENSALE" type)
        var request = new TokenSaleRequest { TRANSACTION_HEADER = MakeHeader() };
        Assert.That(GetExpectedTransactionType(request), Is.EqualTo("SALE"));
    }

    [Test]
    public void TransactionTypeConstants_HaveExpectedValues()
    {
        Assert.That(PaywireTransactionType.Sale, Is.EqualTo("SALE"));
        Assert.That(PaywireTransactionType.Void, Is.EqualTo("VOID"));
        Assert.That(PaywireTransactionType.Credit, Is.EqualTo("CREDIT"));
        Assert.That(PaywireTransactionType.PreAuth, Is.EqualTo("PREAUTH"));
        Assert.That(PaywireTransactionType.GetAuthToken, Is.EqualTo("GETAUTHTOKEN"));
        Assert.That(PaywireTransactionType.GetConsumerFee, Is.EqualTo("GETCONSUMERFEE"));
        Assert.That(PaywireTransactionType.StoreToken, Is.EqualTo("STORETOKEN"));
        Assert.That(PaywireTransactionType.RemoveToken, Is.EqualTo("REMOVETOKEN"));
        Assert.That(PaywireTransactionType.Verification, Is.EqualTo("VERIFICATION"));
        Assert.That(PaywireTransactionType.BatchInquiry, Is.EqualTo("BATCHINQUIRY"));
        Assert.That(PaywireTransactionType.CloseBatch, Is.EqualTo("CLOSE"));
        Assert.That(PaywireTransactionType.SearchTransactions, Is.EqualTo("SEARCHTRANS"));
        Assert.That(PaywireTransactionType.SendReceipt, Is.EqualTo("SENDRECEIPT"));
        Assert.That(PaywireTransactionType.SearchChargeback, Is.EqualTo("SEARCHCB"));
        Assert.That(PaywireTransactionType.BinValidation, Is.EqualTo("BINVALIDATION"));
        Assert.That(PaywireTransactionType.Capture, Is.EqualTo("CAPTURE"));
    }
}
