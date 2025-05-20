using Paywire.NET.Models.Base;
using System.Xml.Serialization;

namespace Paywire.NET.Models.RemoveToken
{

    public class RemoveTokenRequest : BasePaywireRequest
    {
        [XmlElement("CUSTOMER")]
        public Customer CUSTOMER { get; set; }
             
    }
}
