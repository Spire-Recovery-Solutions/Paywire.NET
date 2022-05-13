using System.Xml.Serialization;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.GetConsumerFee;

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




    public string PWCLIENTID { get; set; }
    public string PWINVOICENUMBER { get; set; }
    public string RESULT { get; set; }
    public string RESTEXT { get; set; }
    public string PWADJDESC { get; set; }
    public string PWADJAMOUNT { get; set; }
    public string PWSALETAX { get; set; }
    public string PWSALEAMOUNT { get; set; }
    public string MASKEDACCOUNTNUMBER { get; set; }
    public string AHFIRSTNAME { get; set; }
    public string AHLASTNAME { get; set; }
    public string AUTHCODE { get; set; }
    public string PWCID { get; set; }
    public string AVSCODE { get; set; }
    public string CVVCODE { get; set; }
    public string RECURRING { get; set; }


    [XmlElement("CDSUMMARY")] public ConsumerFeeSummary CDSUMMARY { get; set; }
}           