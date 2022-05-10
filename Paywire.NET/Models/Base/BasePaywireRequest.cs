using System.Xml.Serialization;

namespace Paywire.NET.Models.Base;

public class BasePaywireRequest
{

    [XmlElement("TRANSACTIONHEADER")]
    public TransactionHeader TransactionHeader { get; set; }
}