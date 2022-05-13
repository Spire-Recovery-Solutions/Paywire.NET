using System.Threading.Tasks;
using NUnit.Framework;
using Paywire.NET.Factories;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.GetAuthToken;
using Paywire.NET.Models.GetConsumerFee;
using Paywire.NET.Models.Sale;
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
            

            Assert.True(response.RESULT == "SUCCESS");
            Assert.True(response.AUTHTOKEN != null);
        }

        [Test]
        public async Task VerificationTest()
        {
            var request = PaywireRequestFactory.Verification();

            var response = await _client.SendRequest<VerificationResponse>(request);

            Assert.True(response.RESULT == "APPROVAL");

        }

        [Test]
        public async Task ConsumerFeeTest()
        {
            var response = await _client.SendRequest<GetConsumerFeeResponse>(
                new GetConsumerFeeRequest
                {
                    TransactionHeader = new TransactionHeader()
                    {
                        PWSALEAMOUNT = 125.99,
                    },
                    Customer = new Customer
                    {
                        PWMEDIA = "CC",
                        ADJTAXRATE = 0.00,
                        //PWTOKEN = null,
                        STATE = "TX"
                    }
                });

            Assert.True(response.RESULT == "APPROVAL");
        }

        [Test]
        public async Task OneTimeSaleTest()
        {
            var request = PaywireRequestFactory.Sale();

            var response = await _client.SendRequest<SaleResponse>(request);

            Assert.True(response.RESULT == "APPROVAL");
        }
    }
}