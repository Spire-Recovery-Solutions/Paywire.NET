using Paywire.NET.Models.Base;
using System.Xml.Serialization;

namespace Paywire.NET.Models.Receipt
{
  
    public class SendReceiptRequest : BasePaywireRequest
    {
        [XmlElement("CUSTOMER")]
        public Customer CUSTOMER { get; set; }
    }
}
