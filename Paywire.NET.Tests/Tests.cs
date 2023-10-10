using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using Paywire.NET.Factories;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.BatchInquiry;
using Paywire.NET.Models.BinValidation;
using Paywire.NET.Models.Credit;
using Paywire.NET.Models.GetAuthToken;
using Paywire.NET.Models.GetConsumerFee;
using Paywire.NET.Models.PreAuth;
using Paywire.NET.Models.Receipt;
using Paywire.NET.Models.Sale;
using Paywire.NET.Models.SearchTransactions;
using Paywire.NET.Models.StoreToken;
using Paywire.NET.Models.TokenSale;
using Paywire.NET.Models.Verification;
using Paywire.NET.Models.Void;

namespace Paywire.NET.Tests
{
    public class Tests
    {
        private PaywireClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new PaywireClient(new PaywireClientOptions()
            {
                AuthenticationClientId = "",
                AuthenticationUsername = "",
                AuthenticationKey = "",
                AuthenticationPassword = "",
                Endpoint = PaywireEndpoint.Staging
            });
        }

        [Test]
        public async Task GetAuthTokenTest()
        {
            var response = await _client.SendRequest<GetAuthTokenResponse>(
                new GetAuthTokenRequest());

            Assert.True(response.Result == PaywireResult.Success);
            Assert.True(response.AUTHTOKEN != null);
        }

        [Test]
        public async Task VerificationTest()
        {
            var request = PaywireRequestFactory.Verification(new Customer()
            {
                REQUESTTOKEN = "TRUE",
                PWMEDIA = "CC",
                //CARDNUMBER = 4761739001010267,
                //CVV2 = 999,
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

            var response = await _client.SendRequest<VerificationResponse>(request);
            
            Assert.True(response.Result == PaywireResult.Approval);

        }

        [Test]
        public async Task ConsumerFeeTest()
        {
            var cardRequestFee = PaywireRequestFactory.GetConsumerFee(new TransactionHeader()
                {
                    PWSALEAMOUNT = 10.00,
                    DISABLECF = "FALSE"
                },
                new Customer()
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
                    REQUESTTOKEN = "TRUE",
                    PWMEDIA = "CC",
                    CARDNUMBER = 4012301230123010,
                    CVV2 = 123,
                    EXP_YY = "22",
                    EXP_MM = "12",
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
                    DISABLECF = "FALSE"
                },
                new Customer()
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
                    REQUESTTOKEN = "TRUE",
                    PWMEDIA = "ECHECK",
                    BANKACCTTYPE = "CHECKING",
                    ROUTINGNUMBER = "222224444",
                    ACCOUNTNUMBER = "222224444",
                    FIRSTNAME = "CHRIS",
                    LASTNAME = "FROST",
                    PRIMARYPHONE = "7035551212",
                    EMAIL = "CFFROST@EMAILADDRESS.COM",
                    ADDRESS1 = "123",
                    CITY = "CAZENOVIA",
                    STATE = "NY",
                    ZIP = "13035",
                });

            //var cardRequestNoFee = PaywireRequestFactory.GetConsumerFee(new TransactionHeader()
            //    {
            //        PWSALEAMOUNT = 10.00,
            //        DISABLECF = "FALSE"
            //    },
            //    new Customer()
            //    {
            //        //4111 1111 1111 1111, cvv 123, exp 12/25
            //        REQUESTTOKEN = "TRUE",
            //        PWMEDIA = "CC",
            //        CARDNUMBER = 4012301230123010,
            //        CVV2 = 123,
            //        EXP_YY = "22",
            //        EXP_MM = "12",
            //        FIRSTNAME = "CHRIS",
            //        LASTNAME = "FROST",
            //        PRIMARYPHONE = "7035551212",
            //        EMAIL = "CFFROST@EMAILADDRESS.COM",
            //        ADDRESS1 = "123",
            //        CITY = "Norwalk",
            //        STATE = "CT",
            //        ZIP = "06850",
            //    });

            var cardRequestNoFee = PaywireRequestFactory.GetConsumerFee(10.00, "CC", "CT");

            var checkRequestNoFee = PaywireRequestFactory.GetConsumerFee(new TransactionHeader()
                {
                    PWSALEAMOUNT = 10.00,
                    DISABLECF = "FALSE"
                },
                new Customer()
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
                    REQUESTTOKEN = "TRUE",
                    PWMEDIA = "ECHECK",
                    BANKACCTTYPE = "CHECKING",
                    ROUTINGNUMBER = "222224444",
                    ACCOUNTNUMBER = "222224444",
                    FIRSTNAME = "CHRIS",
                    LASTNAME = "FROST",
                    PRIMARYPHONE = "7035551212",
                    EMAIL = "CFFROST@EMAILADDRESS.COM",
                    ADDRESS1 = "123",
                    CITY = "Norwalk",
                    STATE = "CT",
                    ZIP = "06850",
                });

            var cardFeeResponse = await _client.SendRequest<GetConsumerFeeResponse>(cardRequestFee);
            var checkFeeResponse = await _client.SendRequest<GetConsumerFeeResponse>(checkRequestFee);
            var cardNoFeeResponse = await _client.SendRequest<GetConsumerFeeResponse>(cardRequestNoFee);
            var checkNoFeeResponse = await _client.SendRequest<GetConsumerFeeResponse>(checkRequestNoFee);
            
            Assert.True(cardFeeResponse.Result == PaywireResult.Approval && cardFeeResponse.PWADJAMOUNT != cardFeeResponse.AMOUNT);
            Assert.True(cardNoFeeResponse.Result == PaywireResult.Approval && cardNoFeeResponse.PWADJAMOUNT == cardNoFeeResponse.AMOUNT);
            Assert.True(checkFeeResponse.Result == PaywireResult.Approval && checkFeeResponse.PWADJAMOUNT != checkFeeResponse.AMOUNT);
            Assert.True(checkNoFeeResponse.Result == PaywireResult.Approval && checkNoFeeResponse.PWADJAMOUNT == checkNoFeeResponse.AMOUNT);
        }

        [Test]
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
                    //4111 1111 1111 1111, cvv 123, exp 12/25
                    REQUESTTOKEN = "FALSE",
                    DESCRIPTION = "Description",
                    PWMEDIA = "CC",
                    CARDNUMBER = 349999999999991,
                    CVV2 = 5678,
                    EXP_YY = "22",
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
            var freeRequest = PaywireRequestFactory.CardSale(
                new TransactionHeader()
                {
                    PWSALEAMOUNT = 0.05,
                    DISABLECF = "FALSE",
                    //PWINVOICENUMBER = "TEST001"
                },
                new Customer()
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
                    REQUESTTOKEN = "FALSE",
                    PWMEDIA = "CC",
                    CARDNUMBER = 4012301230123010,
                    CVV2 = 123,
                    EXP_YY = "22",
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
            var feeResult = await _client.SendRequest<SaleResponse>(feeRequest);
            var freeResult = await _client.SendRequest<SaleResponse>(freeRequest);

            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;

            Assert.True(feeResult.Result == PaywireResult.Approval);
            Assert.True(freeResult.Result == PaywireResult.Declined);
        }


        [Test]
        public async Task BatchInquriyTest()
        {
            var request = PaywireRequestFactory.BatchInquiry();

            var sw = Stopwatch.StartNew();
            var response = await _client.SendRequest<BatchInquiryResponse>(request);
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;

            Assert.True(response.Result == PaywireResult.Success);
        }

        [Test]
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
                COND_CARDTYPE = "",                    //COND_CARDTYPE			string	    Search by the card type used for the transaction.	Fixed options: ALL, VISA, MC, DISC, AMEX, ACH, REMOTE
                COND_LASTFOUR = "",                    //COND_LASTFOUR			int	        Search by the last four digits of the account or card used in the transaction searched.	4/4
                COND_CUSTOMERID = "",                  //COND_CUSTOMERID		string	    Search by the Paywire customer identifier returned when creating a token.	
                COND_RECURRINGID = "",                 //COND_RECURRINGID		int	        Search by the periodic identifier returned when creating a periodic plan.	
                COND_PWINVOICENUMBER = "",             //COND_PWINVOICENUMBER	string	    Search by the merchant-submitted or Paywire-generated unique invoice number associated with the transaction.	0/20, Alphanumeric
                COND_PWCUSTOMID1 = "",                 //COND_PWCUSTOMID1		string	    Search by the custom third-party identifier associated with the transaction.	
            });

            var sw = Stopwatch.StartNew();
            var response = await _client.SendRequest<SearchTransactionsResponse>(request);
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;

            Assert.True(response.Result == PaywireResult.Success);
        }

        [Test]
        public async Task PreAuthTest()
        {
            var request = PaywireRequestFactory.PreAuth(10.00, "CT", 4012301230123010, "12", "22", 123);
            var response = await _client.SendRequest<PreAuthResponse>(request);

            Assert.True(response.Result == PaywireResult.Approval);
        }

        [Test]
        public async Task CreditTest()
        {
            // TODO: Find what data can make this a valid unit test
            var request = PaywireRequestFactory.Credit(10.00, "12345", "12345");
            var response = await _client.SendRequest<CreditResponse>(request);

            Assert.True(response.Result == PaywireResult.Success);
        }

        [Test]
        public async Task VoidTest()
        {
            // TODO: Find what data can make this a valid unit test
            var request = PaywireRequestFactory.Void(10.00, "12345", "12345");
            var response = await _client.SendRequest<VoidResponse>(request);

            Assert.True(response.Result == PaywireResult.Success);
        }

        [Test]
        public async Task VerificationTestNew()
        {
            var request = PaywireRequestFactory.Verification(10.00, 4761739001010267, "12", "25", 999);
            var response = await _client.SendRequest<VerificationResponse>(request);

            Assert.True(response.Result == PaywireResult.Approval);
        }

        [Test]
        public async Task StoreTokenTest()
        {
            var request = PaywireRequestFactory.StoreCreditCardToken(10.00, 4012301230123010, "12", "22", 123);
            var response = await _client.SendRequest<StoreTokenResponse>(request);

            Assert.True(response.Result == PaywireResult.Approval);
        }

        [Test]
        public async Task TokenSaleTest()
        {
            var request = PaywireRequestFactory.TokenSale(10.00, "", "CT");
            var response = await _client.SendRequest<TokenSaleResponse>(request);

            Assert.True(response.Result == PaywireResult.Approval);
        }

        [Test]
        public async Task SendReceipt()
        {
            var request = PaywireRequestFactory.SendReceipt(
                 new TransactionHeader()
                 {
                 },
                 new Customer()
                 {
                     EMAIL = "CFFROST@EMAILADDRESS.COM"
                 }
             );
            var response = await _client.SendRequest<SendReceiptResponse>(request);

            Assert.True(response.Result == PaywireResult.Success);
        }

        [Test]
        public async Task BinValidationTest()
        {
            var request = PaywireRequestFactory.BinValidation(
                 new TransactionHeader()
                 {
                 },
                 new Customer()
                 {
                    BINNUMBER = ""
                 }
             );
            var response = await _client.SendRequest<BinValidationResponse>(request);

            Assert.True(response.Result == PaywireResult.Success);
        }
    }
}