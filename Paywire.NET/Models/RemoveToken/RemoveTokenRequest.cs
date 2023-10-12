using Paywire.NET.Models.Base;
using System.Xml.Serialization;

namespace Paywire.NET.Models.RemoveToken
{
    [XmlRoot("PAYMENTREQUEST")]
    public class RemoveTokenRequest : BasePaywireRequest
    {
        [XmlElement("CUSTOMER")]
        public Customer Customer { get; set; }
             
    }
}
