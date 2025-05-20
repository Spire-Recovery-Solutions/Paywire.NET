using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.SearchTransactions;

public class SearchTransactionsRequest : BasePaywireRequest
{
    [XmlElement("SEARCHCONDITION")]
    public SearchCondition SEARCH_CONDITION { get; set; }
}

public class SearchCondition
{
    [XmlIgnore]
    public DateTimeOffset? DATE_FROM { get; set; }
    [XmlIgnore]
    public DateTimeOffset? DATE_TO { get; set; }

    public string? COND_DATEFROM => DATE_FROM?.ToString("yyyy-MM-dd HH:mm");
    public string? COND_DATETO => DATE_TO?.ToString("yyyy-MM-dd HH:mm");
    public string? COND_TRANSTYPE { get; set; }
    public string? COND_PWCID { get; set; }
    public string? COND_PWCUSTOMID2 { get; set; }
    public string? COND_USERNAME { get; set; }
    public string? COND_UNIQUEID { get; set; }
    public string? COND_BATCHID { get; set; }
    public string? COND_TRANSAMT { get; set; }
    public string? COND_CARDTYPE { get; set; }
    public string? COND_LASTFOUR { get; set; }
    public string? COND_CUSTOMERID { get; set; }
    public string? COND_RECURRINGID { get; set; }
    public string? COND_PWINVOICENUMBER { get; set; }
    public string? COND_RESULT { get; set; }
    public string? COND_PWCUSTOMID1 { get; set; }
    public string? COND_PWCUSTOMID3 { get; set; }
    public string? COND_CBTYPE { get; set; }
    public string COND_INSTITUTION { get; set; }
}