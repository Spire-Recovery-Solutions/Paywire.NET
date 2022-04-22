using RestSharp;

namespace Paywire.NET
{
    public class PaywireClient
    {
        private readonly PaywireClientOptions _options;
        private string _endpointUrl;
        private RestClient _restClient;
        private readonly RestClientOptions _restClientOptions;
        private readonly TransactionHeader _transactionHeader;

        public PaywireClient(PaywireClientOptions options)
        {
            _options = options;
            _endpointUrl = GetEndpointUrl(options.Endpoint);
            _restClientOptions = new RestClientOptions(_endpointUrl)
            {
                ThrowOnAnyError = true,
                ThrowOnDeserializationError = true,
                Timeout = 2000
            };
            _restClient = new RestClient(_restClientOptions);
            _transactionHeader = new TransactionHeader()
            {
                PWCLIENTID = options.AuthenticationClientId, PWKEY = options.AuthenticationKey,
                PWUSER = options.AuthenticationUsername, PWPASS = options.AuthenticationPassword
            };
        }

        public async Task<RestResponse> Test(BasePaymentRequest input)
        {
            var request = new RestRequest("/API/pwapi", Method.Post);
            request.AddHeader("Content-Type", "text/xml");
            request.AddHeader("Content-Type", "text/xml");
            request.AddXmlBody(input);

            var response = await _restClient.PostAsync(request);
            return response;
        }
        

        private static string GetEndpointUrl(PaywireEndpoint endpoint)
        {
            return endpoint switch
            {
                PaywireEndpoint.Staging => "https://dbstage1.paywire.com",
                PaywireEndpoint.Production => "https://dbtranz.paywire.com",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, null)
            };
        }
    }

    public class PaywireClientOptions
    {
        public PaywireEndpoint Endpoint { get; set; }
        public string AuthenticationClientId { get; set; }
        public string AuthenticationKey { get; set; }
        public string AuthenticationUsername { get; set; }
        public string AuthenticationPassword { get; set; }
    }

    public enum PaywireEndpoint
    {
        Staging, //Staging     https://dbstage1.paywire.com
        Production //Production    https://dbtranz.paywire.com
    }

    public class TransactionHeader
    {
        public string PWCLIENTID { get; set; }
        public string PWKEY { get; set; }
        public string PWUSER { get; set; }
        public string PWPASS { get; set; }
        public string PWTRANSACTIONTYPE { get; set; }
    }

    public class BasePaymentRequest
    {
        public TransactionHeader TransactionHeader { get; set; }
    }

    public class GetAuthTokenRequest : BasePaymentRequest
    {
        public GetAuthTokenRequest()
        {
            TransactionHeader.PWTRANSACTIONTYPE = "GETAUTHTOKEN";
        }
    }
}