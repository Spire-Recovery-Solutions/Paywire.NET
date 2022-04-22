using System.Threading.Tasks;
using NUnit.Framework;

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
                AuthenticationClientId =  "",
                AuthenticationKey = "",
                AuthenticationUsername = "",
                AuthenticationPassword = "",
                Endpoint = PaywireEndpoint.Staging
            });
        }

        [Test]
        public async Task GetAuthTokenTest()
        {
            var response = await _client.SendRequest(new GetAuthTokenRequest());
            Assert.True(response.RESULT == "SUCCESS");
        }
    }
}