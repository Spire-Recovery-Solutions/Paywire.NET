// CreditCardTests.cs
using NUnit.Framework;
using Paywire.NET.Factories;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.Verification;
using Paywire.NET.Models.GetConsumerFee;
using Paywire.NET.Models.Sale;
using Paywire.NET.Models.Credit;
using Paywire.NET.Models.Void;
using Paywire.NET.Models.PreAuth;
using Paywire.NET.Models.Capture;
using Paywire.NET.Models.BinValidation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Paywire.NET.Tests;

[TestFixture]
[Order(2)]
public class CreditCardTests : BaseTests
{
    [Test, Order(1), Category("Customer Verification")]
    public async Task VerificationTest()
    {
        var request = PaywireRequestFactory.Verification(new Customer
        {
            REQUESTTOKEN = "TRUE",
            PWMEDIA = "CC",
            CARDNUMBER = 4012301230123010,
            CVV2 = "123",
            EXP_YY = "27",
            EXP_MM = "07",
            FIRSTNAME = "John",
            LASTNAME = "Doe",
            PRIMARYPHONE = "7168675309",
            EMAIL = "john@doe.com",
            ADDRESS1 = "123 John St",
            ZIP = "14094"
        });

        var response = await CLIENT.SendRequest<VerificationResponse>(request);
        
        if (response.RESULT == PaywireResult.Approval)
        {
            INVOICE_NUMBER = response.PWINVOICENUMBER;
        }
        
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }

    [Test, Order(7), Category("Customer Verification")]
    public async Task VerificationTestNew()
    {
        Assert.That(string.IsNullOrEmpty(SALE_AMOUNT), Is.False, "SALE_AMOUNT is required for this test - previous test may have failed");

        var request = PaywireRequestFactory.Verification(
            Convert.ToDouble(SALE_AMOUNT), 4012301230123010, "07", "27", "123");
        var response = await CLIENT.SendRequest<VerificationResponse>(request);
        
        if (response.RESULT == PaywireResult.Approval)
        {
            INVOICE_NUMBER = response.PWINVOICENUMBER;
        }
        
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }

    [Test, Order(2), Category("BIN Validation")]
    public async Task BinValidationTest()
    {
        // BIN validation uses BINNUMBER (Bank Identification Number), not CARDNUMBER
        var request = PaywireRequestFactory.BinValidation(
            new TransactionHeader(),
            new Customer
            {
                BINNUMBER = "400057"
            });

        var response = await CLIENT.SendRequest<BinValidationResponse>(request);

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Success),
            $"BIN validation failed: {response.RESTEXT}");
        Assert.That(response.BIN_DETAIL, Is.Not.Null, "BIN_DETAIL should not be null");
        Assert.That(response.BIN_DETAIL.BIN, Is.EqualTo("400057"),
            $"Expected BIN '400057', got '{response.BIN_DETAIL.BIN}'");
        Assert.That(response.BIN_DETAIL.BRAND, Is.Not.Null.And.Not.Empty,
            "BRAND should be populated");
        Assert.That(response.BIN_DETAIL.CARDTYPE, Is.Not.Null.And.Not.Empty,
            "CARDTYPE should be populated");
        // SUBTYPE and ISPREPAID are optional fields - may be null
        // We test that the properties exist on the class, not that they're populated

        TestContext.WriteLine(
            $"BIN: {response.BIN_DETAIL.BIN}, " +
            $"Brand: {response.BIN_DETAIL.BRAND}, " +
            $"CardType: {response.BIN_DETAIL.CARDTYPE}, " +
            $"Bank: {response.BIN_DETAIL.BANK ?? "N/A"}, " +
            $"Country: {response.BIN_DETAIL.COUNTRY ?? "N/A"}, " +
            $"IsFSA: {response.BIN_DETAIL.ISFSA ?? "N/A"}, " +
            $"Subtype: {response.BIN_DETAIL.SUBTYPE ?? "N/A"}, " +
            $"IsPrepaid: {response.BIN_DETAIL.ISPREPAID ?? "N/A"}");
    }

    [Test, Order(3), Category("Credit Card")]
    public async Task ConsumerFeeTest()
    {
        // Create card request with fee
        var cardRequestFee = PaywireRequestFactory.GetConsumerFee(
            new TransactionHeader
            {
                PWSALEAMOUNT = 10.00,
                DISABLECF = "FALSE",
                PWINVOICENUMBER = INVOICE_NUMBER
            },
            new Customer
            {
                REQUESTTOKEN = "TRUE",
                PWMEDIA = "CC",
                CARDNUMBER = 4012301230123010,
                CVV2 = "123",
                EXP_YY = "27",
                EXP_MM = "12",
                ADJTAXRATE = Convert.ToDouble("7"),
                FIRSTNAME = "CHRIS",
                LASTNAME = "FROST",
                PRIMARYPHONE = "7035551212",
                EMAIL = "CFFROST@EMAILADDRESS.COM",
                ADDRESS1 = "123",
                CITY = "CAZENOVIA",
                STATE = "NY",
                ZIP = "13035"
            });

        // Create check request with fee
        var checkRequestFee = PaywireRequestFactory.GetConsumerFee(
            new TransactionHeader
            {
                PWSALEAMOUNT = 10.00,
                DISABLECF = "FALSE",
                PWINVOICENUMBER = INVOICE_NUMBER
            },
            new Customer
            {
                REQUESTTOKEN = "TRUE",
                PWMEDIA = "ECHECK",
                BANKACCTTYPE = "CHECKING",
                ROUTINGNUMBER = "222224444",
                ACCOUNTNUMBER = "222224444",
                ADJTAXRATE = Convert.ToDouble("7"),
                FIRSTNAME = "CHRIS",
                LASTNAME = "FROST",
                PRIMARYPHONE = "7035551212",
                EMAIL = "CFFROST@EMAILADDRESS.COM",
                ADDRESS1 = "123",
                CITY = "CAZENOVIA",
                STATE = "NY",
                ZIP = "13035"
            });

        // Create card request without fee
        var cardRequestNoFee = PaywireRequestFactory.GetConsumerFee(
            new TransactionHeader
            {
                PWSALEAMOUNT = 10.00,
                DISABLECF = "FALSE",
                PWINVOICENUMBER = INVOICE_NUMBER
            },
            new Customer
            {
                REQUESTTOKEN = "TRUE",
                PWMEDIA = "CC",
                CARDNUMBER = 4012301230123010,
                CVV2 = "123",
                EXP_YY = "27",
                EXP_MM = "12",
                ADJTAXRATE = Convert.ToDouble("7"),
                FIRSTNAME = "CHRIS",
                LASTNAME = "FROST",
                PRIMARYPHONE = "7035551212",
                EMAIL = "CFFROST@EMAILADDRESS.COM",
                ADDRESS1 = "123",
                CITY = "Norwalk",
                STATE = "CT",
                ZIP = "06850"
            });

        // Create check request without fee
        var checkRequestNoFee = PaywireRequestFactory.GetConsumerFee(
            new TransactionHeader
            {
                PWSALEAMOUNT = 10.00,
                DISABLECF = "FALSE",
                PWINVOICENUMBER = INVOICE_NUMBER
            },
            new Customer
            {
                REQUESTTOKEN = "TRUE",
                PWMEDIA = "ECHECK",
                BANKACCTTYPE = "CHECKING",
                ROUTINGNUMBER = "222224444",
                ACCOUNTNUMBER = "222224444",
                ADJTAXRATE = Convert.ToDouble("7"),
                FIRSTNAME = "CHRIS",
                LASTNAME = "FROST",
                PRIMARYPHONE = "7035551212",
                EMAIL = "CFFROST@EMAILADDRESS.COM",
                ADDRESS1 = "123",
                CITY = "Norwalk",
                STATE = "CT",
                ZIP = "06850"
            });

        // Execute requests
        var cardFeeResponse = await CLIENT.SendRequest<GetConsumerFeeResponse>(cardRequestFee);
        var checkFeeResponse = await CLIENT.SendRequest<GetConsumerFeeResponse>(checkRequestFee);
        var cardNoFeeResponse = await CLIENT.SendRequest<GetConsumerFeeResponse>(cardRequestNoFee);
        var checkNoFeeResponse = await CLIENT.SendRequest<GetConsumerFeeResponse>(checkRequestNoFee);

        // Assert results
        Assert.Multiple(() =>
        {
            Assert.That(cardFeeResponse.RESULT, Is.EqualTo(PaywireResult.Approval));
            Assert.That(cardFeeResponse.PWADJAMOUNT, Is.Not.EqualTo(cardFeeResponse.AMOUNT));

            Assert.That(cardNoFeeResponse.RESULT, Is.EqualTo(PaywireResult.Approval));
            Assert.That(cardNoFeeResponse.PWADJAMOUNT, Is.EqualTo(0));

            Assert.That(checkFeeResponse.RESULT, Is.EqualTo(PaywireResult.Approval));
            Assert.That(checkFeeResponse.PWADJAMOUNT, Is.Not.EqualTo(checkFeeResponse.AMOUNT));

            Assert.That(checkNoFeeResponse.RESULT, Is.EqualTo(PaywireResult.Approval));
            Assert.That(checkNoFeeResponse.PWADJAMOUNT, Is.EqualTo(0));
        });
    }

    [Test, Order(4), Category("Credit Card")]
    public async Task ConsumerFeeDisableCF_Test()
    {
        // Create card request with fee disabled
        var cardRequestFee = PaywireRequestFactory.GetConsumerFee(
            new TransactionHeader
            {
                PWSALEAMOUNT = 10.00,
                DISABLECF = "TRUE",
                PWINVOICENUMBER = INVOICE_NUMBER
            },
            new Customer
            {
                REQUESTTOKEN = "TRUE",
                PWMEDIA = "CC",
                CARDNUMBER = 4012301230123010,
                CVV2 = "123",
                EXP_YY = "27",
                EXP_MM = "12",
                ADJTAXRATE = Convert.ToDouble("7"),
                FIRSTNAME = "CHRIS",
                LASTNAME = "FROST",
                PRIMARYPHONE = "7035551212",
                EMAIL = "CFFROST@EMAILADDRESS.COM",
                ADDRESS1 = "123",
                CITY = "CAZENOVIA",
                STATE = "NY",
                ZIP = "13035"
            });

        // Create check request with fee disabled
        var checkRequestFee = PaywireRequestFactory.GetConsumerFee(
            new TransactionHeader
            {
                PWSALEAMOUNT = 10.00,
                DISABLECF = "TRUE",
                PWINVOICENUMBER = INVOICE_NUMBER
            },
            new Customer
            {
                REQUESTTOKEN = "TRUE",
                PWMEDIA = "ECHECK",
                BANKACCTTYPE = "CHECKING",
                ROUTINGNUMBER = "222224444",
                ACCOUNTNUMBER = "222224444",
                ADJTAXRATE = Convert.ToDouble("7"),
                FIRSTNAME = "CHRIS",
                LASTNAME = "FROST",
                PRIMARYPHONE = "7035551212",
                EMAIL = "CFFROST@EMAILADDRESS.COM",
                ADDRESS1 = "123",
                CITY = "CAZENOVIA",
                STATE = "NY",
                ZIP = "13035"
            });

        // Execute requests
        var cardFeeResponse = await CLIENT.SendRequest<GetConsumerFeeResponse>(cardRequestFee);
        var checkFeeResponse = await CLIENT.SendRequest<GetConsumerFeeResponse>(checkRequestFee);

        // Assert results
        Assert.That(cardFeeResponse.RESULT, Is.EqualTo(PaywireResult.Approval));
        Assert.That(cardFeeResponse.PWADJAMOUNT, Is.EqualTo(0));

        Assert.That(checkFeeResponse.RESULT, Is.EqualTo(PaywireResult.Approval));
        Assert.That(checkFeeResponse.PWADJAMOUNT, Is.EqualTo(0));
    }

    [Test, Order(5), Category("Credit Card")]
    public async Task SaleWithConsumerFeeTest()
    {
        // Test credit card sale with consumer fee enabled
        var cardSaleRequest = PaywireRequestFactory.Sale(
            new TransactionHeader
            {
                PWSALEAMOUNT = 20.00,
                DISABLECF = "FALSE"
            },
            new Customer
            {
                REQUESTTOKEN = "FALSE",
                DESCRIPTION = "Description",
                PWMEDIA = "CC",
                CARDNUMBER = 4012301230123010,
                CVV2 = "123",
                EXP_YY = "27",
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
                PWCUSTOMID1 = "123Test"
            });

        // Test check (ACH) sale with consumer fee enabled
        var checkSaleRequest = PaywireRequestFactory.Sale(
            new TransactionHeader
            {
                PWSALEAMOUNT = 15.00,
                DISABLECF = "FALSE"
            },
            new Customer
            {
                REQUESTTOKEN = "FALSE",
                DESCRIPTION = "Description",
                PWMEDIA = "ECHECK",
                BANKACCTTYPE = "CHECKING",
                ROUTINGNUMBER = "222224444",
                ACCOUNTNUMBER = "222224444",
                FIRSTNAME = "CHRIS",
                LASTNAME = "FROSTY",
                PRIMARYPHONE = "7035551212",
                EMAIL = "CFFROST@EMAILADDRESS.COM",
                ADDRESS1 = "123",
                ADDRESS2 = "",
                CITY = "LOCKPORT",
                STATE = "NY",
                COUNTRY = "US",
                ZIP = "14094"
            });

        // Execute requests
        var cardSaleResponse = await CLIENT.SendRequest<SaleResponse>(cardSaleRequest);
        var checkSaleResponse = await CLIENT.SendRequest<SaleResponse>(checkSaleRequest);

        // Store for subsequent tests
        if (cardSaleResponse.RESULT == PaywireResult.Approval)
        {
            UNIQUE_ID = cardSaleResponse.PWUNIQUEID;
            INVOICE_NUMBER = cardSaleResponse.PWINVOICENUMBER;
            BATCH_ID = cardSaleResponse.BATCHID;
            SALE_AMOUNT = cardSaleResponse.PWSALEAMOUNT;
            TOKEN = cardSaleResponse.PWTOKEN;
        }

        // Assertions
        Assert.That(cardSaleResponse.RESULT, Is.EqualTo(PaywireResult.Approval),
            $"Card sale failed: {cardSaleResponse.RESTEXT}");
        Assert.That(checkSaleResponse.RESULT, Is.EqualTo(PaywireResult.Approval),
            $"Check sale failed: {checkSaleResponse.RESTEXT}");

        // Verify consumer fee was applied (PWADJAMOUNT should be non-"0" for this merchant)
        Assert.That(cardSaleResponse.PWADJAMOUNT, Is.Not.EqualTo("0"),
            $"Expected consumer fee to be applied to card sale, got PWADJAMOUNT: {cardSaleResponse.PWADJAMOUNT}");
    }

    [Test, Order(6), Category("Credit Card")]
    public async Task SaleWithoutConsumerFeeTest()
    {
        // Test sale with consumer fee disabled
        var request = PaywireRequestFactory.Sale(
            new TransactionHeader
            {
                PWSALEAMOUNT = 10.00,
                DISABLECF = "TRUE"
            },
            new Customer
            {
                REQUESTTOKEN = "FALSE",
                PWMEDIA = "CC",
                CARDNUMBER = 4012301230123010,
                CVV2 = "123",
                EXP_YY = "27",
                EXP_MM = "12",
                FIRSTNAME = "CHRIS",
                LASTNAME = "FROSTY",
                EMAIL = "CFFROST@EMAILADDRESS.COM",
                ADDRESS1 = "123",
                CITY = "NORWALK",
                STATE = "CT",
                ZIP = "06850"
            });

        var response = await CLIENT.SendRequest<SaleResponse>(request);

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval),
            $"Sale without consumer fee failed: {response.RESTEXT}");
        // With DISABLECF=TRUE, PWADJAMOUNT should be "0" or "0.00"
        var hasNoFee = response.PWADJAMOUNT == "0" || response.PWADJAMOUNT == "0.00";
        Assert.That(hasNoFee, Is.True,
            $"Expected no consumer fee when DISABLECF=TRUE, got PWADJAMOUNT: {response.PWADJAMOUNT}");
    }

    [Test, Order(8), Category("Credit Card")]
    public async Task CheckSaleTest()
    {
        var request = PaywireRequestFactory.OneTimeCheckPayment(
            saleAmount: 10.00,
            routingNumber: "222224444",
            accountNumber: "222224444",
            bankAccountType: "CHECKING",
            companyName: "",
            firstName: "CHRIS",
            lastName: "FROST",
            email: "CFFROST@EMAILADDRESS.COM",
            address: "123",
            address2: "",
            city: "CAZENOVIA",
            state: "NY",
            country: "",
            zip: "13035",
            primaryPhone: "7035551212",
            workPhone: ""
        );

        var stopwatch = Stopwatch.StartNew();
        var response = await CLIENT.SendRequest<SaleResponse>(request);
        stopwatch.Stop();

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }

    [Test, Order(10), Category("Credit Card")]
    public async Task CardnumberCreditTest()
    {
        var request = PaywireRequestFactory.Credit(
            new TransactionHeader
            {
                PWSALEAMOUNT = 0.1,
                PWINVOICENUMBER = INVOICE_NUMBER,
                CARDNUMBER = "4012301230123010",
                EXP_MM = "12",
                EXP_YY = "27"
            },
            new Customer
            {
                PWMEDIA = "CC"
            });
            
        var response = await CLIENT.SendRequest<CreditResponse>(request);
        
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }

    [Test, Order(9), Category("Credit Card")]
    public async Task AccountNumberCreditTest()
    {
        var request = PaywireRequestFactory.Credit(
            new TransactionHeader
            {
                PWSALEAMOUNT = 0.1,
                PWINVOICENUMBER = INVOICE_NUMBER,
                BANKACCTTYPE = "CHECKING",
                ROUTINGNUMBER = "222224444",
                ACCOUNTNUMBER = "222224444"
            },
            new Customer
            {
                PWMEDIA = "ECHECK"
            });
            
        var response = await CLIENT.SendRequest<CreditResponse>(request);
        
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }

    [Test, Order(11), Category("Credit Card")]
    public async Task VoidTest()
    {
        Assert.That(string.IsNullOrEmpty(SALE_AMOUNT), Is.False, "SALE_AMOUNT is required for this test - previous test may have failed");
        Assert.That(string.IsNullOrEmpty(UNIQUE_ID), Is.False, "UNIQUE_ID is required for this test - previous test may have failed");

        var request = PaywireRequestFactory.Void(
            Convert.ToDouble(SALE_AMOUNT), INVOICE_NUMBER, UNIQUE_ID);
        var response = await CLIENT.SendRequest<VoidResponse>(request);

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }

    [Test, Order(14), Category("Credit Card")]
    public async Task DoubleVoidTest()
    {
        Assert.That(string.IsNullOrEmpty(UNIQUE_ID), Is.False, "UNIQUE_ID required");
        // Try voiding the already-voided transaction
        var request = PaywireRequestFactory.Void(
            Convert.ToDouble(SALE_AMOUNT), INVOICE_NUMBER, UNIQUE_ID);
        var response = await CLIENT.SendRequest<VoidResponse>(request);
        Assert.That(response.RESULT, Is.Not.EqualTo(PaywireResult.Approval),
            "Double void should not succeed");
    }

    [Test, Order(12), Category("Credit Card")]
    public async Task PreAuthTest()
    {
        var request = PaywireRequestFactory.PreAuth(
            new TransactionHeader
            {
                PWSALEAMOUNT = 5.0,
                DISABLECF = "FALSE",
                PWINVOICENUMBER = INVOICE_NUMBER
            },
            new Customer
            {
                REQUESTTOKEN = "FALSE",
                DESCRIPTION = "Description",
                PWMEDIA = "CC",
                CARDNUMBER = 4012301230123010,
                CVV2 = "123",
                EXP_YY = "27",
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
                ZIP = "14094"
            });
            
        var response = await CLIENT.SendRequest<PreAuthResponse>(request);
        
        if (response.RESULT == PaywireResult.Approval)
        {
            PRE_AUTH_UNIQUE_ID = response.PWUNIQUEID;
            PRE_AUTH_INVOICE_NUMBER = response.PWINVOICENUMBER;
        }
        
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }

    [Test, Order(13), Category("Credit Card")]
    public async Task CaptureTest()
    {
        var request = PaywireRequestFactory.Capture(
            Convert.ToDouble(0), PRE_AUTH_INVOICE_NUMBER, PRE_AUTH_UNIQUE_ID);
        var response = await CLIENT.SendRequest<CaptureResponse>(request);

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }
}