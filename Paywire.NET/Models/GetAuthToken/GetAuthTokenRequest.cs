using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.GetAuthToken;

[XmlRoot("PAYMENTREQUEST")]
public class GetAuthTokenRequest : BasePaywireRequest
{
    public GetAuthTokenRequest()
    {
        TransactionHeader = new TransactionHeader
        {
            PWTRANSACTIONTYPE = PaywireTransactionType.GetAuthToken
        };
    }
}