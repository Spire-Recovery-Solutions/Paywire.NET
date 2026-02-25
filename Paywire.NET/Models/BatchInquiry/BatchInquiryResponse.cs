using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.BatchInquiry;

public class BatchInquiryResponse : BasePaywireResponse
{

    public string BATCHID { get; set; } = string.Empty;
    public string SALESTOTAL { get; set; } = string.Empty;
    public string SALESRECS { get; set; } = string.Empty;
    public string CREDITAMT { get; set; } = string.Empty;
    public string CREDITRECS { get; set; } = string.Empty;
    public string NETCRAMT { get; set; } = string.Empty;
    public string NETCRRECS { get; set; } = string.Empty;
    public string VOIDAMT { get; set; } = string.Empty;
    public string VOIDRECS { get; set; } = string.Empty;

}
