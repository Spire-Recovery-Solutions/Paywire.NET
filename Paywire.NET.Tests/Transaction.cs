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
        
        // Store last transaction details for next tests
        CREDIT_UNIQUE_ID = response.SEARCH_RESULTS.PAYMENT_DETAILS
            .Select(s => s.PWUID)
            .LastOrDefault() ?? string.Empty;
            
        CREDIT_INVOICE_NUMBER = response.SEARCH_RESULTS.PAYMENT_DETAILS
            .Select(s => s.PWINVOICENUMBER)
            .LastOrDefault() ?? string.Empty;
        
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Success));
    }
    
    [Test, Order(2), Category("Credit Card")]
    public async Task CreditTest()
    {
        Assert.That(string.IsNullOrEmpty(CREDIT_UNIQUE_ID), Is.False, "CREDIT_UNIQUE_ID is required for this test - no settled transactions found");

        var request = PaywireRequestFactory.Credit(
            Convert.ToDouble("00.01"),
            CREDIT_INVOICE_NUMBER,
            CREDIT_UNIQUE_ID);

        var response = await CLIENT.SendRequest<CreditResponse>(request);

        // If the transaction is not eligible for credit, that's expected for integration tests
        // The test verifies we can make the API call correctly
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval).Or.EqualTo(PaywireResult.Error),
            $"Credit API call failed. RESTEXT: {response.RESTEXT}");

        // Additional info if it failed
        if (response.RESULT == PaywireResult.Error)
        {
            Assert.That(response.RESTEXT, Does.Contain("INVALID FOR CREDIT").Or.Contain("SETTLED"),
                "Expected credit to fail with 'INVALID FOR CREDIT' or settlement-related message, but got: " + response.RESTEXT);
        }
    }
    
    [Test, Order(3), Category("Search")]
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
    
    [Test, Order(4), Category("Receipt")]
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