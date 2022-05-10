using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NUnit.Framework;
using Paywire.NET.Models.GetAuthToken;
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
            var response = await _client.SendRequest<GetAuthTokenResponse>(new GetAuthTokenRequest());
            Assert.True(response.RESULT == "SUCCESS");
            Assert.True(response.AUTHTOKEN != null);
        }

        [Test]
        public async Task VerificationTest()
        {
            var response = await _client.SendRequest<VerificationResponse>(new VerificationRequest
            {
                Customer = new Customer()
                {
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