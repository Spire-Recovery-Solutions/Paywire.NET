using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.PreAuth
{

    public class PreAuthRequest : BasePaywireRequest
    {
        [XmlElement("CUSTOMER")]
        public Customer CUSTOMER { get; set; }
    }
}
