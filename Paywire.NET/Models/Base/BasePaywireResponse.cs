using System.Xml.Serialization;

namespace Paywire.NET.Models.Base;

[XmlRoot("PAYMENTRESPONSE")]
public class BasePaywireResponse
{

    [XmlElement("RESULT")]
    public string RAW_RESULT { get; set; } = string.Empty;

    /// <summary>
    /// Status for the request.	APPROVAL, DECLINED, ERROR, SUCCESS, CAPTURED, CHARGEBACK
    /// </summary>
    [XmlIgnore]
    public PaywireResult RESULT
    {
        get
        {
            var canParse = Enum.TryParse(typeof(PaywireResult), RAW_RESULT, true, out var parsed);
            if (canParse)
            {
                return (PaywireResult)(parsed ?? PaywireResult.Unknown);
            }

            return RAW_RESULT.ToUpper() == "APPROVED" ? PaywireResult.Approval : PaywireResult.Unknown;
        }
        set { }
    }

    /// <summary>
    /// Transaction DateTime from the response headers
    /// </summary>
    public DateTimeOffset TIMESTAMP { get; set; }

    /// <summary>
    /// Paywire-generated unique merchant identifier.
    /// </summary>
    public string PWCLIENTID { get; set; } = string.Empty;
    /// <summary>
    /// Identifier for this request.
    /// </summary>
    public string PWINVOICENUMBER { get; set; } = string.Empty;

    /// <summary>
    /// Contains the error message.
    /// </summary>
    public string RESTEXT { get; set; } = string.Empty;

    public string PWCUSTOMERID { get; set; } = string.Empty;

    public string PWCID { get; set; } = string.Empty;

    public string PWTOKEN { get; set; } = string.Empty;

    /// <summary>
    /// Custom third-party id to be associated with this transaction.
    /// </summary>
    public string PWCUSTOMID1 {get; set; } = string.Empty;

    public string PWCUSTOMID2 { get; set; } = string.Empty;
}
