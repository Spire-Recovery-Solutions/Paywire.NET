using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.Void
{
    [XmlRoot("PAYMENTREQUEST")]
    public class VoidRequest : BasePaywireRequest
    {
    }
}
