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
            EXP_YY = "25",
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

    [Test, Order(2), Category("Customer Verification")]
    public async Task VerificationTestNew()
    {
        var request = PaywireRequestFactory.Verification(
            Convert.ToDouble(SALE_AMOUNT), 4012301230123010, "07", "25", "123");
        var response = await CLIENT.SendRequest<VerificationResponse>(request);
        
        if (response.RESULT == PaywireResult.Approval)
        {
            INVOICE_NUMBER = response.PWINVOICENUMBER;
        }
        
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
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
                EXP_YY = "25",
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
                EXP_YY = "25",
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
                EXP_YY = "25",
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
    public async Task OneTimeSaleTest()
    {
        // Create request for credit card fee
        var feeRequest = PaywireRequestFactory.Sale(
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
                PWCUSTOMID1 = "123Test"
            });

        // Create request for check fee
        var feeRequestCheck = PaywireRequestFactory.Sale(
            new TransactionHeader
            {
                PWSALEAMOUNT = 15,
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

        // Create free request
        var freeRequest = PaywireRequestFactory.Sale(
            new TransactionHeader
            {
                PWSALEAMOUNT = 0.05,
                DISABLECF = "FALSE"
            },
            new Customer
            {
                REQUESTTOKEN = "FALSE",
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
                CITY = "NORWALK",
                STATE = "CT",
                ZIP = "06850"
            });

        // Execute requests with timing
        var stopwatch = Stopwatch.StartNew();
        var res = await CLIENT.SendRequest<SaleResponse>(feeRequestCheck);
        var feeResult = await CLIENT.SendRequest<SaleResponse>(feeRequest);
        
        if (feeResult.RESULT == PaywireResult.Approval)
        {
            UNIQUE_ID = feeResult.PWUNIQUEID;
            INVOICE_NUMBER = feeResult.PWINVOICENUMBER;
            BATCH_ID = feeResult.BATCHID;
            SALE_AMOUNT = feeResult.PWSALEAMOUNT;
            TOKEN = feeResult.PWTOKEN;
        }
        
        // Create and execute pre-auth request
        var preAuthRequest = PaywireRequestFactory.PreAuth(
            new TransactionHeader
            {
                PWSALEAMOUNT = 10.0,
                PWINVOICENUMBER = INVOICE_NUMBER
            },
            new Customer
            {
                PWMEDIA = "CC",
                CARDNUMBER = 4012301230123010,
                EXP_YY = "25",
                EXP_MM = "12"
            });

        var preAuthResponse = await CLIENT.SendRequest<PreAuthResponse>(preAuthRequest);
        var freeResult = await CLIENT.SendRequest<SaleResponse>(freeRequest);
        stopwatch.Stop();

        // Assertions
        Assert.That(feeResult.RESULT, Is.EqualTo(PaywireResult.Approval));
        Assert.That(freeResult.RESULT, Is.EqualTo(PaywireResult.Declined));
    }

    [Test, Order(6), Category("Credit Card")]
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

    [Test, Order(7), Category("Credit Card")]
    public async Task CardnumberCreditTest()
    {
        var request = PaywireRequestFactory.Credit(
            new TransactionHeader
            {
                PWSALEAMOUNT = 0.1,
                PWINVOICENUMBER = INVOICE_NUMBER,
                CARDNUMBER = "4012301230123010",
                EXP_MM = "12",
                EXP_YY = "25"
            },
            new Customer
            {
                PWMEDIA = "CC"
            });
            
        var response = await CLIENT.SendRequest<CreditResponse>(request);
        
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }

    [Test, Order(8), Category("Credit Card")]
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

    [Test, Order(9), Category("Credit Card")]
    public async Task VoidTest()
    {
        var request = PaywireRequestFactory.Void(
            Convert.ToDouble(SALE_AMOUNT), INVOICE_NUMBER, UNIQUE_ID);
        var response = await CLIENT.SendRequest<VoidResponse>(request);

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }

    [Test, Order(10)]
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

    [Test, Order(11), Category("Credit Card")]
    public async Task CaptureTest()
    {
        var request = PaywireRequestFactory.Capture(
            Convert.ToDouble(0), PRE_AUTH_INVOICE_NUMBER, PRE_AUTH_UNIQUE_ID);
        var response = await CLIENT.SendRequest<CaptureResponse>(request);

        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
    }
}