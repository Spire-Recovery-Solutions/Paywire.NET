using Paywire.NET.Converters;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Paywire.NET.Models.Base;

public class BasePaywireResponse
{
    private PaywireResult _result;
    /// <summary>
    /// Status for the request.	APPROVAL, DECLINED, ERROR, SUCCESS, CAPTURED, CHARGEBACK
    /// </summary>
    [XmlElement("RESULT")]
    public string ResultAsString
    {
        get => ResultCodeConverter.ConvertPaywireResultToString(_result);
        set => _result = ResultCodeConverter.ConvertStringToPaywireResult(value);
    }

    [XmlIgnore]
    public PaywireResult Result
    {
        get => _result;
        set => _result = value;
    }

    /// <summary>
    /// Transaction DateTime from the response headers
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// Paywire-generated unique merchant identifier.	
    /// </summary>
    public string PWCLIENTID { get; set; }
    /// <summary>
    /// Identifier for this request.	
    /// </summary>
    public string PWINVOICENUMBER { get; set; }

    /// <summary>
    /// Contains the error message.
    /// </summary>
    public string RESTEXT { get; set; }

    public string PWCUSTOMERID { get; set; }

    public string PWCID { get; set; }

    public string PWTOKEN { get; set; }
}