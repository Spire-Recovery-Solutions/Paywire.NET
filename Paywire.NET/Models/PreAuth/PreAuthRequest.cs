using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.PreAuth
{
    [XmlRoot("PAYMENTREQUEST")]
    public class PreAuthRequest : BasePaywireRequest
    {
        [XmlElement("CUSTOMER")]
        public Customer Customer { get; set; }
    }
}
