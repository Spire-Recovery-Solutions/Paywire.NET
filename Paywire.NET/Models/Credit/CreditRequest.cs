using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.Credit
{

    public class CreditRequest : BasePaywireRequest
    {
        [XmlElement("CUSTOMER")]
        public Customer CUSTOMER { get; set; }
    }
}
