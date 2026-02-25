using NUnit.Framework;
using Paywire.NET.Factories;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.GetAuthToken;
using Paywire.NET.Models.Sale;
using Paywire.NET.Models.StoreToken;
using Paywire.NET.Models.TokenSale;
using Paywire.NET.Models.PreAuth;
using Paywire.NET.Models.SearchTransactions;
using Paywire.NET.Models.Verification;
using System;
using System.Threading.Tasks;

namespace Paywire.NET.Tests;

[TestFixture]
[Order(5)]
public class ExpandedIntegrationTests : BaseTests
{
    private static string _storedToken = string.Empty;
    private static string _saleInvoiceNumber = string.Empty;

    [Test, Order(1), Category("Token")]
    public async Task GetAuthTokenNoParamsTest()
    {
        var request = PaywireRequestFactory.GetAuthToken();
        var response = await CLIENT.SendRequest<GetAuthTokenResponse>(request);

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Success),
            $"GetAuthToken (no params) failed: {response.RESTEXT}");
        Assert.That(response.AUTHTOKEN, Is.Not.Null.And.Not.Empty,
            "AUTHTOKEN should be returned");
    }

    [Test, Order(2), Category("Credit Card")]
    public async Task OneTimeCardPaymentTest()
    {
        var request = PaywireRequestFactory.OneTimeCardPayment(
            saleAmount: 10.00,
            cardNumber: 4012301230123010,
            expMm: "12",
            expYy: "27",
            cvv2: "123",
            firstName: "TEST",
            lastName: "USER",
            state: "NY",
            email: "test@test.com");

        var response = await CLIENT.SendRequest<SaleResponse>(request);

        if (response.RESULT == PaywireResult.Approval)
        {
            _saleInvoiceNumber = response.PWINVOICENUMBER;
        }

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval),
            $"OneTimeCardPayment failed: {response.RESTEXT}");
    }

    [Test, Order(3), Category("Token")]
    public async Task TokenSaleSimpleTest()
    {
        // First store a token
        var storeRequest = PaywireRequestFactory.StoreCreditCardToken(
            10.00, 4012301230123010, "12", "27", "123");
        var storeResponse = await CLIENT.SendRequest<StoreTokenResponse>(storeRequest);

        Assert.That(storeResponse.RESULT, Is.EqualTo(PaywireResult.Approval),
            $"StoreCreditCardToken failed: {storeResponse.RESTEXT}");

        _storedToken = storeResponse.PWTOKEN;

        // Now do a token sale using the simple overload
        var saleRequest = PaywireRequestFactory.TokenSale(10.00, _storedToken, "NY");
        var saleResponse = await CLIENT.SendRequest<TokenSaleResponse>(saleRequest);

        Assert.That(saleResponse.RESULT, Is.EqualTo(PaywireResult.Approval),
            $"TokenSale (simple) failed: {saleResponse.RESTEXT}");
    }

    [Test, Order(4), Category("Credit Card")]
    public async Task PreAuthSimpleTest()
    {
        var request = PaywireRequestFactory.PreAuth(
            5.00, "NY", 4012301230123010, "12", "27", "123");
        var response = await CLIENT.SendRequest<PreAuthResponse>(request);

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval),
            $"PreAuth (simple) failed: {response.RESTEXT}");
    }

    [Test, Order(5), Category("Search")]
    public async Task SearchByLastFourTest()
    {
        var request = PaywireRequestFactory.SearchTransactions(
            new TransactionHeader { XOPTION = "TRUE" },
            new SearchCondition
            {
                DATE_FROM = DateTimeOffset.Now.AddDays(-30),
                DATE_TO = DateTimeOffset.Now.AddDays(1),
                COND_LASTFOUR = "3010",
                COND_TRANSTYPE = "ALL",
                COND_CARDTYPE = "ALL"
            });

        var response = await CLIENT.SendRequest<SearchTransactionsResponse>(request);

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Success),
            $"SearchByLastFour failed: {response.RESTEXT}");
        Assert.That(response.SEARCH_RESULTS, Is.Not.Null, "SEARCH_RESULTS should not be null");
        Assert.That(response.SEARCH_RESULTS.PAYMENT_DETAILS, Is.Not.Null.And.Not.Empty,
            "Expected results when searching by last four digits '3010'");

        TestContext.WriteLine(
            $"Found {response.SEARCH_RESULTS.PAYMENT_DETAILS.Length} transactions with last four '3010'");
    }

    [Test, Order(6), Category("Search")]
    public async Task SearchByInvoiceNumberTest()
    {
        // Do a sale first to get a known invoice number
        var saleRequest = PaywireRequestFactory.OneTimeCardPayment(
            saleAmount: 10.00,
            cardNumber: 4012301230123010,
            expMm: "12",
            expYy: "27",
            cvv2: "123",
            firstName: "TEST",
            lastName: "SEARCH",
            state: "NY",
            email: "test@test.com");

        var saleResponse = await CLIENT.SendRequest<SaleResponse>(saleRequest);

        Assert.That(saleResponse.RESULT, Is.EqualTo(PaywireResult.Approval),
            $"Setup sale failed: {saleResponse.RESTEXT}");

        var invoiceNumber = saleResponse.PWINVOICENUMBER;
        Assert.That(invoiceNumber, Is.Not.Null.And.Not.Empty,
            "Sale should return an invoice number");

        // Now search by that invoice number
        var searchRequest = PaywireRequestFactory.SearchTransactions(
            new TransactionHeader { XOPTION = "TRUE" },
            new SearchCondition
            {
                DATE_FROM = DateTimeOffset.Now.AddDays(-1),
                DATE_TO = DateTimeOffset.Now.AddDays(1),
                COND_PWINVOICENUMBER = invoiceNumber,
                COND_TRANSTYPE = "ALL",
                COND_CARDTYPE = "ALL"
            });

        var searchResponse = await CLIENT.SendRequest<SearchTransactionsResponse>(searchRequest);

        Assert.That(searchResponse.RESULT, Is.EqualTo(PaywireResult.Success),
            $"SearchByInvoiceNumber failed: {searchResponse.RESTEXT}");
        Assert.That(searchResponse.SEARCH_RESULTS, Is.Not.Null, "SEARCH_RESULTS should not be null");
        Assert.That(searchResponse.SEARCH_RESULTS.PAYMENT_DETAILS, Is.Not.Null.And.Not.Empty,
            $"Expected results when searching by invoice number '{invoiceNumber}'");

        TestContext.WriteLine(
            $"Found {searchResponse.SEARCH_RESULTS.PAYMENT_DETAILS.Length} transactions for invoice '{invoiceNumber}'");
    }

    [Test, Order(7), Category("Token")]
    public async Task StoreCheckTokenTest()
    {
        var request = PaywireRequestFactory.StoreCheckToken(
            0.00,
            routingNumber: "222224444",
            accountNumber: "222224444",
            bankAccountType: "CHECKING",
            firstName: "TEST",
            lastName: "CHECK",
            state: "NY");

        var response = await CLIENT.SendRequest<StoreTokenResponse>(request);

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval),
            $"StoreCheckToken failed: {response.RESTEXT}");
        Assert.That(response.PWTOKEN, Is.Not.Null.And.Not.Empty,
            "StoreCheckToken should return a token");

        TestContext.WriteLine($"Stored check token: {response.PWTOKEN}");
    }

    [Test, Order(8), Category("ECHECK")]
    public async Task ECheckVerificationTest()
    {
        var request = PaywireRequestFactory.Verification(new Customer
        {
            PWMEDIA = "ECHECK",
            ROUTINGNUMBER = "222224444",
            ACCOUNTNUMBER = "222224444",
            BANKACCTTYPE = "CHECKING",
            FIRSTNAME = "TEST",
            LASTNAME = "CHECK",
            STATE = "NY"
        });

        var response = await CLIENT.SendRequest<VerificationResponse>(request);

        // ECHECK verification may return Approval or Success depending on merchant config
        Assert.That(response.RESULT,
            Is.EqualTo(PaywireResult.Approval).Or.EqualTo(PaywireResult.Success),
            $"ECheck verification failed: {response.RESULT} - {response.RESTEXT}");

        TestContext.WriteLine($"ECheck verification result: {response.RESULT}");
    }
}
