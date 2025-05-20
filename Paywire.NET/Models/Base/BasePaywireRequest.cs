using System.Xml.Serialization;

namespace Paywire.NET.Models.Base;

[XmlRoot("PAYMENTREQUEST")]
public class BasePaywireRequest
{

    [XmlElement("TRANSACTIONHEADER")]
    public required TransactionHeader TRANSACTION_HEADER { get; set; }
}