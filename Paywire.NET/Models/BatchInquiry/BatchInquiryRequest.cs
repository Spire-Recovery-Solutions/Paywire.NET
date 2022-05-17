using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.BatchInquiry;

[XmlRoot("PAYMENTREQUEST")]
public class BatchInquiryRequest : BasePaywireRequest
{

}