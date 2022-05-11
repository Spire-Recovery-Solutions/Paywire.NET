using System.Threading.Tasks;
using NUnit.Framework;
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
            var response = await _client.SendRequest<VerificationResponse>(
                new VerificationRequest
                {
                    Customer = new Customer()
                    {
                        REQUESTTOKEN = "FALSE",
                        PWMEDIA = "CC",
                        CARDNUMBER = 4761739001010267,
                        CVV2 = 999,
                        EXP_YY = "22",
                        EXP_MM = "07",
                        FIRSTNAME = "John",
                        LASTNAME = "Doe",
                        PRIMARYPHONE = "7168675309",
                        EMAIL = "john@doe.com",
                        ADDRESS1 = "123 John St",
                        ZIP = "14094",
                    }
                });

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
            var response = await _client.SendRequest<SaleResponse>(
                new SaleRequest()
                {
                    TransactionHeader = new TransactionHeader()
                    {
                        PWSALEAMOUNT = 125.99,
                    },
                    Customer = new Customer()
                    {
                        REQUESTTOKEN = "FALSE",
                        PWMEDIA = "CC",
                        CARDNUMBER = 4761739001010267,
                        CVV2 = 999,
                        EXP_YY = "22",
                        EXP_MM = "07",
                        FIRSTNAME = "John",
                        LASTNAME = "Doe",
                        PRIMARYPHONE = "7168675309",
                        EMAIL = "john@doe.com",
                        ADDRESS1 = "123 John St",
                        ZIP = "14094",
                    }
                });

            Assert.True(response.RESULT == "APPROVAL");
        }
    }
}