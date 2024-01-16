using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.StoreToken
{
  
    public class StoreTokenRequest : BasePaywireRequest
    {
        [XmlElement("CUSTOMER")] 
        public Customer Customer { get; set; }
    }
}
