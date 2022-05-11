using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.Sale;

[XmlRoot("PAYMENTRESPONSE")]

public class SaleResponse : BasePaywireResponse
{
   public string BATCHID { get; set; }
   public string PAYMETH { get; set; }
   public string PWUNIQUEID { get; set; }
   public string AHNAME { get; set; }
   public string AMOUNT { get; set; }
   public string MACCOUNT { get; set; }
   public string EMAIL { get; set; }
   public string CCTYPE { get; set; }
}  