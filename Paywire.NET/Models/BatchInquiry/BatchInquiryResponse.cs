using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.BatchInquiry;

[XmlRoot("PAYMENTRESPONSE")]
public class BatchInquiryResponse : BasePaywireResponse
{

    public string BATCHID { get; set; }
    public string SALESTOTAL { get; set; }
    public string SALESRECS { get; set; }
    public string CREDITAMT { get; set; }
    public string CREDITRECS { get; set; }
    public string NETCRAMT { get; set; }
    public string NETCRRECS { get; set; }
    public string VOIDAMT { get; set; }
    public string VOIDRECS { get; set; }

}