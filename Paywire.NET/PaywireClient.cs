using System.Xml.Serialization;
using RestSharp;
using RestSharp.Serializers.Xml;

namespace Paywire.NET
{
    public class PaywireClient
    {
        private readonly PaywireClientOptions _options;
        private string _endpointUrl;
        private RestClient _restClient;
        private readonly RestClientOptions _restClientOptions;

        public PaywireClient(PaywireClientOptions options)
        {
            _options = options;
            _endpointUrl = GetEndpointUrl(options.Endpoint);
            _restClientOptions = new RestClientOptions(_endpointUrl)
            {
                ThrowOnAnyError = true,
                ThrowOnDeserializationError = true,
                Timeout = 20000,

            };
            _restClient = new RestClient(_restClientOptions);
        }

        public async Task<BasePaywireResponse?> SendRequest(BasePaywireRequest input)
        {
            input.TransactionHeader.PWCLIENTID = _options.AuthenticationClientId;
            input.TransactionHeader.PWUSER = _options.AuthenticationUsername;
            input.TransactionHeader.PWKEY = _options.AuthenticationKey;
            input.TransactionHeader.PWPASS = _options.AuthenticationPassword;

            var request = new RestRequest("/API/pwapi", Method.Post);
            request.AddHeader("Content-Type", "text/xml");
            request.AddHeader("Content-Type", "text/xml");
            request.AddXmlBody(input);

            var response = await _restClient.PostAsync<GetAuthTokenResponse>(request);
            return response;
        }
        

        private static string GetEndpointUrl(PaywireEndpoint endpoint)
        {
            return endpoint switch
            {
                PaywireEndpoint.Staging => 
                    "https://a7f8d8c59328.ngrok.io",
                    //"https://dbstage1.paywire.com",
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

    [XmlType("TRANSACTIONHEADER")]
    public class TransactionHeader
    {
        public string PWCLIENTID { get; set; }
        public string PWKEY { get; set; }
        public string PWUSER { get; set; }
        public string PWPASS { get; set; }
        public string PWTRANSACTIONTYPE { get; set; }
    }

    public class BasePaywireRequest
    {
        public TransactionHeader TransactionHeader { get; set; }
    }

    public class BasePaywireResponse
    {
        public string RESULT { get; set; }
    }

    [XmlType("PAYMENTREQUEST")]
    public class GetAuthTokenRequest : BasePaywireRequest
    {
        public GetAuthTokenRequest()
        {
            TransactionHeader = new TransactionHeader
            {
                PWTRANSACTIONTYPE = "GETAUTHTOKEN"
            };
        }
    }
    
    [XmlRoot("PAYMENTRESPONSE")]
    public class GetAuthTokenResponse : BasePaywireResponse
    {

        //[XmlElement(ElementName="PWCLIENTID")] 
        public int PWCLIENTID { get; set; }

        //[XmlElement(ElementName="AUTHTOKEN")] 
        public string AUTHTOKEN { get; set; }
    }
}