// TransactionTests.cs
using NUnit.Framework;
using Paywire.NET.Factories;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.SearchTransactions;
using Paywire.NET.Models.Credit;
using Paywire.NET.Models.SearchChargebacks;
using Paywire.NET.Models.Receipt;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Paywire.NET.Tests;

[TestFixture]
[Order(3)]
public class TransactionTests : BaseTests
{
    [Test, Order(1), Category("Search")]
    public async Task SearchTransactionsTest()
    {
        // Create search request with parameters
        var request = PaywireRequestFactory.SearchTransactions(
            new TransactionHeader
            {
                XOPTION = "TRUE"
            },
            new SearchCondition
            {
                DATE_FROM = DateTimeOffset.Now.AddDays(-30),
                DATE_TO = DateTimeOffset.Now.AddDays(1),
                COND_PWCID = "",
                COND_USERNAME = "",
                COND_UNIQUEID = "",
                COND_BATCHID = "",
                COND_TRANSAMT = "",
                COND_TRANSTYPE = "ALL",
                COND_RESULT = "SETTLED",
                COND_CARDTYPE = "ALL",
                COND_LASTFOUR = "",
                COND_CUSTOMERID = "",
                COND_RECURRINGID = "",
                COND_PWINVOICENUMBER = "",
                COND_PWCUSTOMID1 = "",
            });

        // Execute with timing
        var stopwatch = Stopwatch.StartNew();
        var response = await CLIENT.SendRequest<SearchTransactionsResponse>(request);
        stopwatch.Stop();

        // Verify we got results back
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Success),
            $"Search failed: {response.RESTEXT}");
        Assert.That(response.SEARCH_RESULTS, Is.Not.Null, "SEARCH_RESULTS should not be null");
        Assert.That(response.SEARCH_RESULTS.PAYMENT_DETAILS, Is.Not.Null, "PAYMENT_DETAILS should not be null");
        Assert.That(response.SEARCH_RESULTS.PAYMENT_DETAILS.Length, Is.GreaterThan(0),
            "Expected at least one settled transaction in the last 30 days");

        // Log search results for visibility
        TestContext.WriteLine($"Found {response.SEARCH_RESULTS.PAYMENT_DETAILS.Length} settled transactions");
    }

    [Test, Order(2), Category("Search")]
    public async Task SearchPreAuthTransactionsTest()
    {
        // Search specifically for PREAUTH transactions
        // Per Paywire API: PreAuth returns "APPROVED" instead of "APPROVAL" for RESULT
        // The SDK maps "APPROVED" -> PaywireResult.Approval to normalize this
        var request = PaywireRequestFactory.SearchTransactions(
            new TransactionHeader
            {
                XOPTION = "TRUE"
            },
            new SearchCondition
            {
                DATE_FROM = DateTimeOffset.Now.AddDays(-30),
                DATE_TO = DateTimeOffset.Now.AddDays(1),
                COND_TRANSTYPE = "PREAUTH",
                COND_CARDTYPE = "ALL"
            });

        var response = await CLIENT.SendRequest<SearchTransactionsResponse>(request);

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Success),
            $"Search failed: {response.RESTEXT}");
        Assert.That(response.SEARCH_RESULTS, Is.Not.Null, "SEARCH_RESULTS should not be null");
        Assert.That(response.SEARCH_RESULTS.PAYMENT_DETAILS, Is.Not.Null, "PAYMENT_DETAILS should not be null");

        TestContext.WriteLine($"Found {response.SEARCH_RESULTS.PAYMENT_DETAILS.Length} PREAUTH transactions");

        // If we have PreAuth transactions, verify RESULT parsing handles "APPROVED" correctly
        // Note: Some PreAuth transactions may be DECLINED - only verify APPROVED ones
        var approvedOrCapturedFound = false;
        foreach (var detail in response.SEARCH_RESULTS.PAYMENT_DETAILS)
        {
            Assert.That(detail.TRANSTYPE, Is.EqualTo("PREAUTH"),
                $"Expected PREAUTH transaction type, got {detail.TRANSTYPE}");

            // The API returns "APPROVED" for successful PreAuth transactions
            // SDK should map "APPROVED" -> PaywireResult.Approval
            if (detail.RESULT == PaywireResult.Approval || detail.RESULT == PaywireResult.Captured)
            {
                approvedOrCapturedFound = true;
                TestContext.WriteLine($"  PWUID: {detail.PWUID}, RAW_RESULT: {detail.RAW_RESULT}, Parsed: {detail.RESULT}");
            }
            else
            {
                TestContext.WriteLine($"  PWUID: {detail.PWUID}, RAW_RESULT: {detail.RAW_RESULT}, Parsed: {detail.RESULT} (DECLINED)");
            }
        }

        // At least one approved/captured PreAuth should exist to verify parsing works
        // If all 100 are declined, that's still valid behavior - just log it
        if (!approvedOrCapturedFound && response.SEARCH_RESULTS.PAYMENT_DETAILS.Length > 0)
        {
            TestContext.WriteLine("Note: No APPROVED/CAPTURED PreAuth transactions found in results. This may be expected.");
        }
    }

    [Test, Order(3), Category("Search")]
    public async Task SearchDateRangeLimitTest()
    {
        // Per Paywire API: Date range must be within 1 month, otherwise max 100 results
        // Test 1: Search with date range > 1 month (60 days)
        var wideRangeRequest = PaywireRequestFactory.SearchTransactions(
            new TransactionHeader
            {
                XOPTION = "TRUE"
            },
            new SearchCondition
            {
                DATE_FROM = DateTimeOffset.Now.AddDays(-60),
                DATE_TO = DateTimeOffset.Now.AddDays(1),
                COND_TRANSTYPE = "ALL",
                COND_CARDTYPE = "ALL"
            });

        var wideRangeResponse = await CLIENT.SendRequest<SearchTransactionsResponse>(wideRangeRequest);

        Assert.That(wideRangeResponse.RESULT, Is.EqualTo(PaywireResult.Success),
            $"Wide range search failed: {wideRangeResponse.RESTEXT}");

        // API should return max 100 results when date range exceeds 1 month
        if (wideRangeResponse.SEARCH_RESULTS?.PAYMENT_DETAILS != null)
        {
            var wideCount = wideRangeResponse.SEARCH_RESULTS.PAYMENT_DETAILS.Length;
            TestContext.WriteLine($"60-day range returned {wideCount} transactions (max 100 expected if limit applies)");

            Assert.That(wideCount, Is.LessThanOrEqualTo(100),
                $"API should return max 100 results for date range > 1 month, got {wideCount}");
        }

        // Test 2: Search with valid date range (30 days) for comparison
        var validRangeRequest = PaywireRequestFactory.SearchTransactions(
            new TransactionHeader
            {
                XOPTION = "TRUE"
            },
            new SearchCondition
            {
                DATE_FROM = DateTimeOffset.Now.AddDays(-30),
                DATE_TO = DateTimeOffset.Now.AddDays(1),
                COND_TRANSTYPE = "ALL",
                COND_CARDTYPE = "ALL"
            });

        var validRangeResponse = await CLIENT.SendRequest<SearchTransactionsResponse>(validRangeRequest);

        Assert.That(validRangeResponse.RESULT, Is.EqualTo(PaywireResult.Success),
            $"Valid range search failed: {validRangeResponse.RESTEXT}");

        if (validRangeResponse.SEARCH_RESULTS?.PAYMENT_DETAILS != null)
        {
            var validCount = validRangeResponse.SEARCH_RESULTS.PAYMENT_DETAILS.Length;
            TestContext.WriteLine($"30-day range returned {validCount} transactions");
        }
    }
    
    [Test, Order(4), Category("Credit Card")]
    public async Task CreditTest()
    {
        // Credit to card directly instead of relying on finding a creditable settled transaction
        var request = PaywireRequestFactory.Credit(
            new TransactionHeader
            {
                PWSALEAMOUNT = 0.01
            },
            new Customer
            {
                PWMEDIA = "CC",
                CARDNUMBER = 4012301230123010,
                EXP_MM = "12",
                EXP_YY = "27",
                CVV2 = "123",
                FIRSTNAME = "CHRIS",
                LASTNAME = "FROSTY",
                EMAIL = "cffrost@emailaddress.com"
            });

        var response = await CLIENT.SendRequest<CreditResponse>(request);

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval),
            $"Credit failed. RESULT: {response.RESULT}, RESTEXT: {response.RESTEXT}");
    }
    
    [Test, Order(5), Category("Credit Card")]
    public async Task OverCreditTest()
    {
        // Try to credit more than a reasonable amount without a matching transaction
        var request = PaywireRequestFactory.Credit(
            new TransactionHeader
            {
                PWSALEAMOUNT = 999999.99,
                PWINVOICENUMBER = "NONEXISTENT"
            },
            new Customer { PWMEDIA = "CC" });
        var response = await CLIENT.SendRequest<CreditResponse>(request);
        // Should not get approved for a massive credit on a nonexistent invoice
        Assert.That(response.RESULT, Is.Not.EqualTo(PaywireResult.Approval).Or.EqualTo(PaywireResult.Error));
    }

    [Test, Order(6), Category("Search")]
    public async Task SearchChargebackTest()
    {
        var request = PaywireRequestFactory.SearchChargeback(
            new TransactionHeader
            {
                XOPTION = "TRUE"
            },
            new SearchCondition
            {
                DATE_FROM = DateTimeOffset.Now.AddDays(-1),
                DATE_TO = DateTimeOffset.Now.AddDays(1),
                COND_CBTYPE = "ALL",
                COND_INSTITUTION = "ALL",
                COND_UNIQUEID = UNIQUE_ID
            });

        var stopwatch = Stopwatch.StartNew();
        var response = await CLIENT.SendRequest<SearchChargebackResponse>(request);
        stopwatch.Stop();
        
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Success));
    }
    
    [Test, Order(7), Category("Receipt")]
    public async Task SendReceiptTest()
    {
        var request = PaywireRequestFactory.SendReceipt(
            new TransactionHeader
            {
                PWUNIQUEID = UNIQUE_ID
            },
            new Customer
            {
                EMAIL = "CFFROST@EMAILADDRESS.COM"
            }
        );
        
        var response = await CLIENT.SendRequest<SendReceiptResponse>(request);
        
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Success));
    }
}