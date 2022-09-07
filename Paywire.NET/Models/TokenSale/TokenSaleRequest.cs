using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.TokenSale
{
    [XmlRoot("PAYMENTREQUEST")]
    public class TokenSaleRequest : BasePaywireRequest
    {
        [XmlElement("CUSTOMER")]
        public Customer Customer { get; set; }
    }
}
