using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.Sale;


public class SaleRequest : BasePaywireRequest
{
    [XmlElement("CUSTOMER")]
    public Customer CUSTOMER { get; set; }
}