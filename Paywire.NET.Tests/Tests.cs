using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using Paywire.NET.Factories;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.BatchInquiry;
using Paywire.NET.Models.GetAuthToken;
using Paywire.NET.Models.GetConsumerFee;
using Paywire.NET.Models.Sale;
using Paywire.NET.Models.SearchTransactions;
using Paywire.NET.Models.Verification;

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
            var request = PaywireRequestFactory.Verification();

            var response = await _client.SendRequest<VerificationResponse>(request);
            
            Assert.True(response.Result == PaywireResult.Approval);

        }

        [Test]
        public async Task ConsumerFeeTest()
        {
            var request = PaywireRequestFactory.GetConsumerFee();

            var response = await _client.SendRequest<GetConsumerFeeResponse>(request);
            
            Assert.True(response.Result == PaywireResult.Approval);
        }

        [Test]
        public async Task OneTimeSaleTest()
        {
            var approvalRequest = PaywireRequestFactory.CardSale();
            var nsfRequest = new SaleRequest()
            {
                TransactionHeader = new TransactionHeader()
                {
                    PWSALEAMOUNT = 0.51,
                    DISABLECF = "FALSE",
                    //PWINVOICENUMBER = "TEST001"
                },
                Customer = new Customer()
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
                    REQUESTTOKEN = "FALSE",
                    PWMEDIA = "CC",
                    CARDNUMBER = 4012301230123010,
                    CVV2 = "123",
                    EXP_YY = "33",
                    EXP_MM = "11",
                    FIRSTNAME = "CHRIS",
                    LASTNAME = "FROSTY",
                    PRIMARYPHONE = "7035551212",
                    EMAIL = "CFFROST@EMAILADDRESS.COM",
                    ADDRESS1 = "123",
                    CITY = "LOCKPORT",
                    STATE = "NY",
                    ZIP = "55555",
                }
            };
            var sw = Stopwatch.StartNew();
            var approval = await _client.SendRequest<SaleResponse>(approvalRequest);
            var decline = await _client.SendRequest<SaleResponse>(nsfRequest);

            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;

            Assert.True(approval.Result == PaywireResult.Approval);
            Assert.True(decline.Result == PaywireResult.Declined);
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
            var request = PaywireRequestFactory.SearchTransactions();

            var sw = Stopwatch.StartNew();
            var response = await _client.SendRequest<SearchTransactionsResponse>(request);
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;

            Assert.True(response.Result == PaywireResult.Success);
        }
    }
}