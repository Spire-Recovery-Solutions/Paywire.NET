using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.Credit
{
    [XmlRoot("PAYMENTREQUEST")]
    public class CreditRequest : BasePaywireRequest
    {
    }
}
