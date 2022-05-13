using System.Text;
using System.Xml.Serialization;

namespace Paywire.NET.Models.Base;

public class TransactionHeader
{
    /// <summary>
    /// ID associated with merchant
    /// </summary>
    public string PWCLIENTID { get; set; }
    public string PWKEY { get; set; }
    public string PWUSER { get; set; }
    public string PWPASS { get; set; }
    public string AUTHTOKEN { get; set; }
    /// <summary>
    /// The Paywire Gateway version number.
    /// </summary>
    public string PWVERSION { get; set; }
    public string PWTRANSACTIONTYPE { get; set; }
    public double PWSALEAMOUNT { get; set; }
    /// <summary>
    /// The Merchant's unique invoice number submitted in the transaction request
    /// </summary>
    public string PWINVOICENUMBER { get; set; }
    public string CARDPRESENT { get; set; }
    public string XOPTION { get; set; }
    public string RECURRINGID { get; set; }
    public string PWUNIQUEID { get; set; }
    public string POSINDICATOR { get; set; }
    public string PWADJAMOUNT { get; set; }
    public string CURRENCY { get; set; }
    public string REQUESTTOKEN { get; set; }
    public string PWAPPROVALURL { get; set; }
    public string PWDECLINEURL { get; set; }
    public string AUTHONLY { get; set; }
    public string ISCUP { get; set; }
    public string LANGUAGE { get; set; }
    public string FORCESAVETOKEN { get; set; }
    public string NONSECUREPLUS { get; set; }
    public string URL { get; set; }
    /// <summary>
    /// Overrides applying a Convenience Fee or Cash Discount when set to TRUE, if configured. Note that Sales Tax will also be disabled.
    /// </summary>
    public string DISABLECF { get; set; }

}