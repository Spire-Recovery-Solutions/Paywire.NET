// TokenTests.cs

using NUnit.Framework;
using Paywire.NET.Factories;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.GetAuthToken;
using Paywire.NET.Models.RemoveToken;
using Paywire.NET.Models.StoreToken;
using Paywire.NET.Models.TokenSale;
using Paywire.NET.Models.Credit;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Paywire.NET.Tests;

[TestFixture]
[Order(1)]
public class TokenTests : BaseTests
{
    [Test, Order(1), Category("Token")]
    public async Task GetAuthTokenTest()
    {
        var response = await CLIENT.SendRequest<GetAuthTokenResponse>(
            PaywireRequestFactory.GetAuthToken(new TransactionHeader()));

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Success));
        Assert.That(response.AUTHTOKEN, Is.Not.Null);
    }

    [Test, Order(2), Category("Token")]
    public async Task StoreTokenTest()
    {
        var request = PaywireRequestFactory.StoreCreditCardToken(10.00, 4012301230123010, "12", "25", "123");
        var response = await CLIENT.SendRequest<StoreTokenResponse>(request);

        if (response.RESULT == PaywireResult.Approval)
        {
            TOKEN = response.PWTOKEN;
        }

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }

    [Test, Order(3), Category("Token")]
    public async Task TokenSaleTest()
    {
        var requestForTokenSale = PaywireRequestFactory.TokenSale(
            new TransactionHeader
            {
                PWSALEAMOUNT = 10.00
            },
            new Customer
            {
                PWTOKEN = TOKEN,
                PWMEDIA = "CC",
                CARDNUMBER = 4012301230123010,
                CVV2 = "123",
                EXP_YY = "25",
                EXP_MM = "12",
                FIRSTNAME = "CHRIS",
                LASTNAME = "FROSTY",
                PRIMARYPHONE = "7035551212",
                EMAIL = "CFFROST@EMAILADDRESS.COM",
                ADDRESS1 = "123",
                ADDRESS2 = "",
                CITY = "LOCKPORT",
                STATE = "NY",
                COUNTRY = "US",
                ZIP = "14094",
                PWCUSTOMID1 = "Test123"
            });

        var responseFromTokenSale = await CLIENT.SendRequest<TokenSaleResponse>(requestForTokenSale);

        if (responseFromTokenSale.RESULT == PaywireResult.Approval)
        {
            UNIQUE_ID = responseFromTokenSale.PWUNIQUEID;
            INVOICE_NUMBER = responseFromTokenSale.PWINVOICENUMBER;
            BATCH_ID = responseFromTokenSale.BATCHID;
            SALE_AMOUNT = responseFromTokenSale.PWSALEAMOUNT;
        }

        Assert.That(responseFromTokenSale.RESULT, Is.EqualTo(PaywireResult.Approval));
    }

    [Test, Order(4), Category("Token")]
    public async Task TokenCreditTest()
    {
        // Log test start
        TestContext.WriteLine("Starting TokenCreditTest");

        // Verify prerequisites
        if (string.IsNullOrEmpty(TOKEN))
        {
            TestContext.WriteLine("ERROR: Token is empty or null - previous test may have failed");
            Assert.Fail("Token is required for this test - previous test may have failed");
        }

        if (string.IsNullOrEmpty(INVOICE_NUMBER))
        {
            TestContext.WriteLine("ERROR: InvoiceNumber is empty or null - previous test may have failed");
            Assert.Fail("InvoiceNumber is required for this test - previous test may have failed");
        }

        // Log the request details
        TestContext.WriteLine(
            $"Sending Credit request with InvoiceNumber: {INVOICE_NUMBER}, Token: {TOKEN}, Amount: 0.1");

        // Create credit request
        var request = PaywireRequestFactory.Credit(
            new TransactionHeader
            {
                PWSALEAMOUNT = 0.1,
                PWINVOICENUMBER = INVOICE_NUMBER,
                PWTOKEN = TOKEN
            },
            new Customer
            {
                PWMEDIA = "CC"
            });

        // Execute request
        var response = await CLIENT.SendRequest<CreditResponse>(request);

        // Log ALL response details for debugging
        TestContext.WriteLine($"API Response - Result: {response.RESULT}, Message: {response.RESTEXT ?? "None"}");
        TestContext.WriteLine($"Response details - PWUNIQUEID: {response.PWUNIQUEID ?? "None"}, " +
                              $"PWINVOICENUMBER: {response.PWINVOICENUMBER ?? "None"}");

        // Check for error first and log it prominently
        if (response.RESULT == PaywireResult.Error || response.RESULT == PaywireResult.Declined)
        {
            TestContext.WriteLine($"========= API ERROR =========");
            TestContext.WriteLine($"Error Result: {response.RESULT}");
            TestContext.WriteLine($"Error Message: {response.RESTEXT}");
            TestContext.WriteLine($"=============================");
        }

        // Now do the assertion - using custom message to include API error details
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval),
            $"Credit request failed with: {response.RESULT} - {response.RESTEXT}");
    }

    [Test, Order(5), Category("Token")]
    public async Task RemoveTokenTest()
    {
        var requestToRemoveToken = PaywireRequestFactory.RemoveToken(
            new TransactionHeader(),
            new Customer
            {
                PWMEDIA = "CC",
                PWTOKEN = TOKEN
            }
        );

        var stopwatch = Stopwatch.StartNew();
        var responseFromRemoveToken = await CLIENT.SendRequest<RemoveTokenResponse>(requestToRemoveToken);
        stopwatch.Stop();

        Assert.That(responseFromRemoveToken.RESULT, Is.EqualTo(PaywireResult.Approval));
    }
}