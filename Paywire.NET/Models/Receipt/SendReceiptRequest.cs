using Paywire.NET.Models.Base;
using System.Xml.Serialization;

namespace Paywire.NET.Models.Receipt
{
    [XmlRoot("PAYMENTREQUEST")]
    public class SendReceiptRequest : BasePaywireRequest
    {
        [XmlElement("CUSTOMER")]
        public Customer Customer { get; set; }
    }
}
