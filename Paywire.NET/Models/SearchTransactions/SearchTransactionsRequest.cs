using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.SearchTransactions;

public class SearchTransactionsRequest : BasePaywireRequest
{
    [XmlElement("SEARCHCONDITION")]
    public SearchCondition SearchCondition { get; set; }
}

public class SearchCondition
{
    [XmlIgnore]
    public DateTimeOffset DateFrom { get; set; }
    [XmlIgnore]
    public DateTimeOffset DateTo { get; set; }

    public string COND_DATEFROM => DateFrom.ToString("yyyy-mm-dd HH:MM");
    public string COND_DATETO => DateTo.ToString("yyyy-mm-dd HH:MM");
    public string COND_TRANSTYPE { get; set; }
    public string COND_PWCID { get; set; }
    public string COND_PWCUSTOMID2 { get; set; }
    public string COND_USERNAME { get; set; }
    public string COND_UNIQUEID { get; set; }
    public string COND_BATCHID { get; set; }
    public string COND_TRANSAMT { get; set; }
    public string COND_CARDTYPE { get; set; }
    public string COND_LASTFOUR { get; set; }
    public string COND_CUSTOMERID { get; set; }
    public string COND_RECURRINGID { get; set; }
    public string COND_PWINVOICENUMBER { get; set; }
    public string COND_RESULT { get; set; }
    public string COND_PWCUSTOMID1 { get; set; }
    public string COND_PWCUSTOMID3 { get; set; }
    public string COND_CBTYPE { get; set; }
    public string COND_INSTITUTION { get; set; }
}