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
            
        var response = await CLIENT.SendRequest<CreditResponse>(request);

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
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