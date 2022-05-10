using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.GetAuthToken;

/// <summary>
/// Exchange your credentials for an AUTHTOKEN to use when calling the OSBP.
/// </summary>
[XmlRoot("PAYMENTRESPONSE")]
public class GetAuthTokenResponse : BasePaywireResponse
{
    /// <summary>
    /// The Authentication Token to be used when calling the OSBP.	
    /// </summary>
    public string AUTHTOKEN { get; set; }
}