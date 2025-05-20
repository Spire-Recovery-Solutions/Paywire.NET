using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.TokenSale
{
 
    public class TokenSaleRequest : BasePaywireRequest
    {
        [XmlElement("CUSTOMER")]
        public Customer CUSTOMER { get; set; }
    }
}
