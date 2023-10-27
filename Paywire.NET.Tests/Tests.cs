using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Helpers;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Paywire.NET.Factories;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.BatchInquiry;
using Paywire.NET.Models.BinValidation;
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

namespace Paywire.NET.Tests
{
    public class Shared
    {
        public static PaywireClient? Client { get; set; }
        public static string Token {get; set; }
        public static string UniqueID { get; set; }
        public static string InvoiceNumber { get; set; }
        public static string BatchID { get; set; }
        public static string SaleAmount { get; set;}
    }

    [Order(1)]
    public class TokenTests : Shared
    {
        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets<TokenTests>()
                .Build();

            Client = new PaywireClient(new PaywireClientOptions()
            {
                AuthenticationClientId = config["PWCLIENTID"],
                AuthenticationUsername = config["PWUSER"],
                AuthenticationKey = config["PWKEY"],
                AuthenticationPassword = config["PWPASS"],
                Endpoint = PaywireEndpoint.Staging
            });

            //Client = new PaywireClient(new PaywireClientOptions()
            //{
            //    AuthenticationClientId = config["PWCLIENTID"],
            //    AuthenticationUsername = config["PWUSER"],
            //    AuthenticationKey = config["PWKEY"],
            //    AuthenticationPassword = config["PWPASS"],
            //    Endpoint = PaywireEndpoint.Staging
            //},
            //    true,
            //    true,
            //    30000
            //);

        }

        [Test, Order(1), Category("Token")]
        public async Task GetAuthTokenTest()
        {
            var response = await Client.SendRequest<GetAuthTokenResponse>(
                new GetAuthTokenRequest());

            Assert.True(response.Result == PaywireResult.Success);
            Assert.True(response.AUTHTOKEN != null);
        }
        [Test, Order(2), Category("Token")]
        public async Task StoreTokenTest()
        {
            var request = PaywireRequestFactory.StoreCreditCardToken(10.00, 4012301230123010, "12", "25", 123);
            var response = await Client.SendRequest<StoreTokenResponse>(request);
            if (response.Result == PaywireResult.Approval)
            {
                Token = response.PWTOKEN;
            }
            Assert.True(response.Result == PaywireResult.Approval);
        }
        [Test, Order(3), Category("Token")]
        public async Task TokenSaleTest()
        {
            //var requestForTokenSale = PaywireRequestFactory.TokenSale(10.00, token, "CT");
            var requestForTokenSale = PaywireRequestFactory.TokenSale(new TransactionHeader
            {
                PWSALEAMOUNT = 10.00
            },
                new Customer
                { 
                    PWTOKEN = Token,
                    PWMEDIA = "CC",
                    CARDNUMBER = 4012301230123010,
                    CVV2 = 123,
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
            var responseFromTokenSale = await Client.SendRequest<TokenSaleResponse>(requestForTokenSale);

            if (responseFromTokenSale.Result == PaywireResult.Approval)
            {
                UniqueID = responseFromTokenSale.PWUNIQUEID;
                InvoiceNumber = responseFromTokenSale.PWINVOICENUMBER;
                BatchID = responseFromTokenSale.BATCHID;
                SaleAmount = responseFromTokenSale.PWSALEAMOUNT;
            }
            Assert.True(responseFromTokenSale.Result == PaywireResult.Approval);
        }
        [Test, Order(4), Category("Token")]
        public async Task RemoveTokenTest()
        {
            var requestToRemoveToken = PaywireRequestFactory.RemoveToken(
                new TransactionHeader()
                {

                },
                new Customer()
                {
                    PWMEDIA = "CC",
                    PWTOKEN = Token
                }
            );

            var sw = Stopwatch.StartNew();
            var responseFromRemoveToken = await Client.SendRequest<RemoveTokenResponse>(requestToRemoveToken);
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;
            Assert.True(responseFromRemoveToken.Result == PaywireResult.Approval);
        }
    }

    [Order(2)]
    public class CreditCard : Shared
    {
        [Test, Order(1), Category("Customer Verification")]
        public async Task VerificationTest()
        {
            var request = PaywireRequestFactory.Verification(new Customer()
            {
                REQUESTTOKEN = "TRUE",
                PWMEDIA = "CC",
                CARDNUMBER = 4012301230123010,
                CVV2 = 123,
                EXP_YY = "25",
                EXP_MM = "07",
                FIRSTNAME = "John",
                LASTNAME = "Doe",
                PRIMARYPHONE = "7168675309",
                EMAIL = "john@doe.com",
                ADDRESS1 = "123 John St",
                ZIP = "14094",
            });

            var response = await Client.SendRequest<VerificationResponse>(request);
            if (response.Result == PaywireResult.Approval)
            {
                InvoiceNumber = response.PWINVOICENUMBER;
            }
            Assert.True(response.Result == PaywireResult.Approval);

        }
        [Test, Order(2), Category("Customer Verification")]
        public async Task VerificationTestNew()
        {
            var request = PaywireRequestFactory.Verification(Convert.ToDouble(SaleAmount), 4761739001010267, "12", "23", 999);
            var response = await Client.SendRequest<VerificationResponse>(request);
            if (response.Result == PaywireResult.Approval)
            {
                InvoiceNumber = response.PWINVOICENUMBER;
            }
            Assert.True(response.Result == PaywireResult.Approval);
        }
        [Test, Order(3), Category("Credit Card")]
        public async Task ConsumerFeeTest()
        {
            var cardRequestFee = PaywireRequestFactory.GetConsumerFee(new TransactionHeader()
                {
                    PWSALEAMOUNT = 10.00,
                    DISABLECF = "FALSE",
                    PWINVOICENUMBER = InvoiceNumber
                },
                new Customer()
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
                    REQUESTTOKEN = "TRUE",
                    PWMEDIA = "CC",
                    CARDNUMBER = 4012301230123010,
                    CVV2 = 123,
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
                    ZIP = "13035",
                });

            var checkRequestFee = PaywireRequestFactory.GetConsumerFee(new TransactionHeader()
                {
                    PWSALEAMOUNT = 10.00,
                    DISABLECF = "FALSE",
                    PWINVOICENUMBER = InvoiceNumber
                },
                new Customer()
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
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
                    ZIP = "13035",
                });

            
            var cardRequestNoFee = PaywireRequestFactory.GetConsumerFee(new TransactionHeader()
                {
                    PWSALEAMOUNT = 10.00,
                    DISABLECF = "FALSE",
                    PWINVOICENUMBER = InvoiceNumber
                },
                new Customer()
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
                    REQUESTTOKEN = "TRUE",
                    PWMEDIA = "CC",
                    CARDNUMBER = 4012301230123010,
                    CVV2 = 123,
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
                    ZIP = "06850",
                }); 
            //PaywireRequestFactory.GetConsumerFee(10.00, "CC", "CT");

            var checkRequestNoFee = PaywireRequestFactory.GetConsumerFee(new TransactionHeader()
                {
                    PWSALEAMOUNT = 10.00,
                    DISABLECF = "FALSE",
                    PWINVOICENUMBER = InvoiceNumber
                },
                new Customer()
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
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
                    ZIP = "06850",
                });

            var cardFeeResponse = await Client.SendRequest<GetConsumerFeeResponse>(cardRequestFee);
            var checkFeeResponse = await Client.SendRequest<GetConsumerFeeResponse>(checkRequestFee);
            var cardNoFeeResponse = await Client.SendRequest<GetConsumerFeeResponse>(cardRequestNoFee);
            var checkNoFeeResponse = await Client.SendRequest<GetConsumerFeeResponse>(checkRequestNoFee);
            
            Assert.True(cardFeeResponse.Result == PaywireResult.Approval && cardFeeResponse.PWADJAMOUNT != cardFeeResponse.AMOUNT);
            Assert.True(cardNoFeeResponse.Result == PaywireResult.Approval && cardNoFeeResponse.PWADJAMOUNT == 0);
            Assert.True(checkFeeResponse.Result == PaywireResult.Approval && checkFeeResponse.PWADJAMOUNT != checkFeeResponse.AMOUNT);
            Assert.True(checkNoFeeResponse.Result == PaywireResult.Approval && checkNoFeeResponse.PWADJAMOUNT == 0);
        }
         [Test, Order(4), Category("Credit Card")]
        public async Task ConsumerFeeDisableCF_Test()
        {
            var cardRequestFee = PaywireRequestFactory.GetConsumerFee(new TransactionHeader()
                {
                    PWSALEAMOUNT = 10.00,
                    DISABLECF = "TRUE",
                    PWINVOICENUMBER = InvoiceNumber
                },
                new Customer()
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
                    REQUESTTOKEN = "TRUE",
                    PWMEDIA = "CC",
                    CARDNUMBER = 4012301230123010,
                    CVV2 = 123,
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
                    ZIP = "13035",
                });

            var checkRequestFee = PaywireRequestFactory.GetConsumerFee(new TransactionHeader()
                {
                    PWSALEAMOUNT = 10.00,
                    DISABLECF = "TRUE",
                    PWINVOICENUMBER = InvoiceNumber
                },
                new Customer()
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
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
                    ZIP = "13035",
                });


            var cardFeeResponse = await Client.SendRequest<GetConsumerFeeResponse>(cardRequestFee);
            var checkFeeResponse = await Client.SendRequest<GetConsumerFeeResponse>(checkRequestFee);
            
            Assert.True(cardFeeResponse.Result == PaywireResult.Approval && cardFeeResponse.PWADJAMOUNT == 0);
            Assert.True(checkFeeResponse.Result == PaywireResult.Approval && checkFeeResponse.PWADJAMOUNT == 0);
        }
        [Test, Order(5), Category("Credit Card")]
        public async Task OneTimeSaleTest()
        {
            var feeRequest = PaywireRequestFactory.CardSale(new TransactionHeader()
                {
                    PWSALEAMOUNT = 0.15,
                    DISABLECF = "FALSE",
                    //PWINVOICENUMBER = "TEST001"
                },
                new Customer
                {
                    REQUESTTOKEN = "FALSE",
                    DESCRIPTION = "Description",
                    PWMEDIA = "CC",
                    CARDNUMBER = 4012301230123010,
                    CVV2 = 123,
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
                });

            var feeRequestCheck = PaywireRequestFactory.CardSale(new TransactionHeader()
                {
                    PWSALEAMOUNT = 0.15,
                    DISABLECF = "FALSE",
                    //PWINVOICENUMBER = "TEST001"
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
                    ZIP = "14094",
                });
            var freeRequest = PaywireRequestFactory.CardSale(
                new TransactionHeader()
                {
                    PWSALEAMOUNT = 0.05,
                    DISABLECF = "FALSE"
                },
                new Customer()
                {
                    REQUESTTOKEN = "TRUE",
                    PWMEDIA = "CC",
                    CARDNUMBER = 4012301230123010,
                    CVV2 = 123,
                    EXP_YY = "25",
                    EXP_MM = "12",
                    FIRSTNAME = "CHRIS",
                    LASTNAME = "FROSTY",
                    PRIMARYPHONE = "7035551212",
                    EMAIL = "CFFROST@EMAILADDRESS.COM",
                    ADDRESS1 = "123",
                    CITY = "NORWALK",
                    STATE = "CT",
                    ZIP = "06850",
                }
            );
            var sw = Stopwatch.StartNew();
            var res = await Client.SendRequest<SaleResponse>(feeRequestCheck);
            var feeResult = await Client.SendRequest<SaleResponse>(feeRequest);
            if (feeResult.Result == PaywireResult.Approval)
            {
                UniqueID = feeResult.PWUNIQUEID;
                InvoiceNumber = feeResult.PWINVOICENUMBER;
                BatchID = feeResult.BATCHID;
                SaleAmount = feeResult.PWSALEAMOUNT;
            }
            var freeResult = await Client.SendRequest<SaleResponse>(freeRequest);
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;

            Assert.True(feeResult.Result == PaywireResult.Approval);
            Assert.True(freeResult.Result == PaywireResult.Declined);
        }
        [Test, Order(6), Category("Credit Card")]
        public async Task VoidTest()
        {
            // TODO: Find what data can make this a valid unit test
            var request = PaywireRequestFactory.Void(Convert.ToDouble(SaleAmount), InvoiceNumber, UniqueID);
            var response = await Client.SendRequest<VoidResponse>(request);

            Assert.True(response.Result == PaywireResult.Approval);
        }
        [Test, Order(7), Category("Credit Card")]
        public async Task CreditTest()
        {
            // TODO: Find what data can make this a valid unit test
            var request = PaywireRequestFactory.Credit(Convert.ToDouble("00.1"), "23289092537421643", "5001754");
            var response = await Client.SendRequest<CreditResponse>(request);

            Assert.True(response.Result == PaywireResult.Approval);
        }
        
        //[Test, Order(8)]
        //public async Task BinValidationTest()
        //{
        //    var request = PaywireRequestFactory.BinValidation(
        //        new TransactionHeader()
        //        {
        //        },
        //        new Customer()
        //        {
        //            BINNUMBER = "401230"
        //        }
        //    );
            
        //    var sw = Stopwatch.StartNew();
        //    var response = await Client.SendRequest<BinValidationResponse>(request);
        //    sw.Stop();
        //    var elapsed = sw.ElapsedMilliseconds;
        //    Assert.True(response.Result == PaywireResult.Success);
        //}
        //[Test, Order(9)]
        //public async Task PreAuthTest()
        //{
        //    var request = PaywireRequestFactory.PreAuth(10.00, "CT", 4012301230123010, "12", "25", 123);
        //    var response = await _client.SendRequest<PreAuthResponse>(request);

        //    Assert.True(response.Result == PaywireResult.Approval);
        //}
    }

    [Order(3)]
    public class Transaction : Shared
    {
        [Test, Order(1), Category("Search")]
        public async Task SearchTransactionsTest()
        {
            var request = PaywireRequestFactory.SearchTransactions(new TransactionHeader()
            {
                XOPTION = "TRUE"
            },
            new SearchCondition()
            {
                DateFrom = DateTime.Now.AddDays(-1),   //COND_DATEFROM			DateTime	Search date range from.	Date Format yyyy-mm-dd HH:MM.
                DateTo = DateTime.Now.AddDays(1),      //COND_DATETO			DateTime	Search date range to.	Date Format yyyy-mm-dd HH:MM.
                COND_PWCID = "",                       //COND_PWCID			    string	    Paywire Customer Identifier. When submitted, the created token will be associated with this customer.
                COND_USERNAME = "",                    //COND_USERNAME			String	    Search by the USERNAME initiating the transaction.	
                COND_UNIQUEID = "",                    //COND_UNIQUEID			int	        Search by transaction Unique ID returned by the gateway.	
                COND_BATCHID = "",                     //COND_BATCHID			string	    Search by Batch ID returned by the gateway.	
                COND_TRANSAMT = "",                    //COND_TRANSAMT			int/decimal	Search by transaction amount.	
                COND_TRANSTYPE = "ALL",                //COND_TRANSTYPE         string	    Search by transaction type.	Fixed options: ALL, SALE, CREDIT, VOID
                COND_RESULT = "",                      //COND_RESULT			string	    Search by transaction result returned by the gateway.	See Transaction Result values.
                COND_CARDTYPE = "ACH",                    //COND_CARDTYPE			string	    Search by the card type used for the transaction.	Fixed options: ALL, VISA, MC, DISC, AMEX, ACH, REMOTE
                COND_LASTFOUR = "",                    //COND_LASTFOUR			int	        Search by the last four digits of the account or card used in the transaction searched.	4/4
                COND_CUSTOMERID = "",                  //COND_CUSTOMERID		string	    Search by the Paywire customer identifier returned when creating a token.	
                COND_RECURRINGID = "",                 //COND_RECURRINGID		int	        Search by the periodic identifier returned when creating a periodic plan.	
                COND_PWINVOICENUMBER = "",             //COND_PWINVOICENUMBER	string	    Search by the merchant-submitted or Paywire-generated unique invoice number associated with the transaction.	0/20, Alphanumeric
                COND_PWCUSTOMID1 = "",                 //COND_PWCUSTOMID1		string	    Search by the custom third-party identifier associated with the transaction.	
            });

            var sw = Stopwatch.StartNew();
            var response = await Client.SendRequest<SearchTransactionsResponse>(request);
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;

            Assert.True(response.Result == PaywireResult.Success);
        }
        [Test, Order(2), Category("Search")]
        public async Task SearchChargebackTest()
        {
            var request = PaywireRequestFactory.SearchChargeback(new TransactionHeader()
                {
                    XOPTION = "TRUE"
                },
                new SearchCondition()
                {
                    DateFrom = DateTime.Now.AddDays(-1),   //COND_DATEFROM			DateTime	Search date range from.	Date Format yyyy-mm-dd HH:MM.
                    DateTo = DateTime.Now.AddDays(1),      //COND_DATETO			DateTime	Search date range to.	Date Format yyyy-mm-dd HH:MM.
                    COND_CBTYPE = "ALL",
                    COND_INSTITUTION = "ALL",
                    COND_UNIQUEID = UniqueID
                });

            var sw = Stopwatch.StartNew();
            var response = await Client.SendRequest<SearchChargebackResponse>(request);
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;

            Assert.True(response.Result == PaywireResult.Success);
        }
        [Test, Order(3), Category("Receipt")]
        public async Task SendReceiptTest()
        {
            var request = PaywireRequestFactory.SendReceipt(
                new TransactionHeader()
                {
                    PWUNIQUEID= UniqueID
                },
                new Customer()
                {
                    EMAIL = "CFFROST@EMAILADDRESS.COM"
                }
            );
            var response = await Client.SendRequest<SendReceiptResponse>(request);

            Assert.True(response.Result == PaywireResult.Success);
        }
    }

    [Order(4)]
    public class Batch : Shared
    {
        [Test, Order(1), Category("Enquiry")]
        public async Task BatchInquriyTest()
        {
            var request = PaywireRequestFactory.BatchInquiry();

            var sw = Stopwatch.StartNew();
            var response = await Client.SendRequest<BatchInquiryResponse>(request);
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;

            Assert.True(response.Result == PaywireResult.Success);
        }
        [Test, Order(2), Category("Close")]
        public async Task CloseBatchTest()
        {
            var request = PaywireRequestFactory.CloseBatch(
                new TransactionHeader()
                {
                    PWINVOICENUMBER = InvoiceNumber
                }
            );
            
            var sw = Stopwatch.StartNew();
            var response = await Client.SendRequest<CloseBatchResponse>(request);
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;
            Assert.True(response.Result == PaywireResult.Success);
        }
    }
}