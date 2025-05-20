using Paywire.NET.Models.Base;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Paywire.NET.Models.BatchInquiry;
using Paywire.NET.Models.BinValidation;
using Paywire.NET.Models.Capture;
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

namespace Paywire.NET
{
    public class PaywireClient
    {
        private readonly PaywireClientOptions _paywireClientOptions;
        private readonly RestClient _restClient;
        private readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(30);

        public PaywireClient(PaywireClientOptions options)
        {
            _paywireClientOptions = options;
            var endpointUrl = GetEndpointUrl(options.ENDPOINT);
            var restClientOptions = new RestClientOptions(endpointUrl)
            {
                // Don't throw on errors so we can properly handle API error responses
                ThrowOnAnyError = false,
                ThrowOnDeserializationError = false,
                Timeout = _defaultTimeout,
            };
            _restClient = new RestClient(restClientOptions);
        }

        public PaywireClient(PaywireClientOptions options, bool enableLogging = false, bool useHttpClientFactory = false, int? timeoutMilliseconds = null)
        {
            _paywireClientOptions = options;
            var endpointUrl = GetEndpointUrl(options.ENDPOINT);
            var timeout = timeoutMilliseconds.HasValue 
                ? TimeSpan.FromMilliseconds(timeoutMilliseconds.Value) 
                : _defaultTimeout;
                
            var restClientOptions = new RestClientOptions(endpointUrl)
            {
                // Don't throw on errors so we can properly handle API error responses
                ThrowOnAnyError = false,
                ThrowOnDeserializationError = false, 
                Timeout = timeout,
            };
            
            _restClient = new RestClient(restClientOptions);
            
            // Additional configuration could be applied here if needed
            // if (enableLogging) { ... }
            // if (useHttpClientFactory) { ... }
        }
        
        public async Task<T> SendRequest<T>(BasePaywireRequest request) where T : BasePaywireResponse, new()
        {
            try
            {
                // Setup request headers
                request.TRANSACTION_HEADER ??= new TransactionHeader();
                request.TRANSACTION_HEADER.PWCLIENTID = _paywireClientOptions.AUTHENTICATION_CLIENT_ID;
                request.TRANSACTION_HEADER.PWKEY = _paywireClientOptions.AUTHENTICATION_KEY;
                request.TRANSACTION_HEADER.PWUSER = _paywireClientOptions.AUTHENTICATION_USERNAME;
                request.TRANSACTION_HEADER.PWPASS = _paywireClientOptions.AUTHENTICATION_PASSWORD;
                request.TRANSACTION_HEADER.PWVERSION = 3;

                // Set transaction type based on request type
                request.TRANSACTION_HEADER.PWTRANSACTIONTYPE = request switch
                {
                    GetAuthTokenRequest => PaywireTransactionType.GetAuthToken,
                    VerificationRequest => PaywireTransactionType.Verification,
                    GetConsumerFeeRequest => PaywireTransactionType.GetConsumerFee,
                    SaleRequest => PaywireTransactionType.Sale,
                    BatchInquiryRequest => PaywireTransactionType.BatchInquiry,
                    SearchTransactionsRequest => PaywireTransactionType.SearchTransactions,
                    CreditRequest => PaywireTransactionType.Credit,
                    PreAuthRequest => PaywireTransactionType.PreAuth,
                    VoidRequest => PaywireTransactionType.Void,
                    StoreTokenRequest => PaywireTransactionType.StoreToken,
                    TokenSaleRequest => PaywireTransactionType.Sale,
                    SendReceiptRequest => PaywireTransactionType.SendReceipt,
                    SearchChargebackRequest => PaywireTransactionType.SearchChargeback,
                    BinValidationRequest => PaywireTransactionType.BinValidation,
                    CloseBatchRequest => PaywireTransactionType.CloseBatch,
                    RemoveTokenRequest => PaywireTransactionType.RemoveToken,
                    CaptureRequest => PaywireTransactionType.Capture,
                    _ => request.TRANSACTION_HEADER.PWTRANSACTIONTYPE
                };

                // Setup and execute request
                var restRequest = new RestRequest("/API/pwapi", Method.Post);
                restRequest.AddXmlBody(request);
                var response = await _restClient.ExecuteAsync(restRequest);
                
                // Initialize default response
                var returnResponse = new T();
                
                // Get timestamp from response headers if available
                DateTimeOffset transDateTime = DateTimeOffset.UtcNow;
                var dateHeader = response.Headers?.FirstOrDefault(h => 
                    h.Name?.Equals("Date", StringComparison.OrdinalIgnoreCase) == true);
                    
                if (dateHeader?.Value != null && 
                    DateTimeOffset.TryParse(dateHeader.Value.ToString(), out var parsedDate))
                {
                    transDateTime = parsedDate;
                }

                // Setup XML deserialization
                var emptyNamespace = new XmlSerializerNamespaces();
                emptyNamespace.Add("", "");
                
                var xmlSerializer = new XmlSerializer(
                    typeof(T), 
                    new XmlRootAttribute("PAYMENTRESPONSE") { Namespace = "" }
                );

                // Handle response content
                if (!string.IsNullOrEmpty(response.Content))
                {
                    try
                    {
                        using TextReader textReader = new StringReader(response.Content);
                        var deserializedResponse = (T)xmlSerializer.Deserialize(textReader);
                        
                        if (deserializedResponse != null)
                        {
                            returnResponse = deserializedResponse;
                        }
                    }
                    catch (Exception ex)
                    {
                        // If deserialization fails, set error information
                        returnResponse.RAW_RESULT = "ERROR";
                        returnResponse.RESTEXT = $"Deserialization error: {ex.Message}";
                    }
                }
                else if (response.ErrorException != null)
                {
                    // Handle transport/network errors
                    returnResponse.RAW_RESULT = "ERROR";
                    returnResponse.RESTEXT = $"Request error: {response.ErrorException.Message}";
                }
                else if (!response.IsSuccessful)
                {
                    // Handle other HTTP errors
                    returnResponse.RAW_RESULT = "ERROR";
                    returnResponse.RESTEXT = $"HTTP error: {response.StatusCode} - {response.StatusDescription}";
                }

                // Set timestamp
                returnResponse.TIMESTAMP = transDateTime;

                return returnResponse;
            }
            catch (Exception ex)
            {
                // Catch-all for any unexpected errors
                var errorResponse = new T
                {
                    RAW_RESULT = "ERROR",
                    RESTEXT = $"Client error: {ex.Message}",
                    TIMESTAMP = DateTimeOffset.UtcNow
                };
                
                return errorResponse;
            }
        }

        /// <summary>
        /// Translates the selected endpoint enumeration into the proper url.
        /// </summary>
        /// <param name="endpoint">PaywireEndpoint enum value</param>
        /// <returns>Endpoint URL</returns>
        /// <exception cref="ArgumentOutOfRangeException">Invalid Endpoint Selection Provided</exception>
        private static string GetEndpointUrl(PaywireEndpoint endpoint)
        {
            return endpoint switch
            {
                PaywireEndpoint.Staging => "https://dbstage1.paywire.com",
                PaywireEndpoint.Production => "https://dbtranz.paywire.com",
                _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, "Endpoint URL type enum out of range")
            };
        }
    }
}