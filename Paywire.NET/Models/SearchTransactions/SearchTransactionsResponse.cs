using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.SearchTransactions;

[XmlRoot("PAYMENTRESPONSE")]
public class SearchTransactionsResponse : BasePaywireResponse
{
    [XmlElement("SEARCHRESULT")]
    public SearchResult SearchResults { get; set; }

}

public class SearchResult
{
    [XmlElement("PWPAYDETAIL")]
    public PaywirePaymentDetail[] PaymentDetails { get; set; }

}

public class PaywirePaymentDetail
{
    public string PWUID            { get; set; } //     281195</PWUID                 
    public string BATCHID          { get; set; } //       526</BATCHID                 
    public string TRANSTYPE        { get; set; } //         SALE</TRANSTYPE                 
    public string CARDTYPE         { get; set; } //        VISA</CARDTYPE                 
    public string CARDNUM          { get; set; } //       X1111</CARDNUM                 
    public string SALEAMOUNT       { get; set; } //          30.00</SALEAMOUNT                 
    public string TRANSAMOUNT      { get; set; } //           30.00</TRANSAMOUNT                 
    public string ADJAMOUNT        { get; set; } //         0.00</ADJAMOUNT                 
    public string TIPAMOUNT        { get; set; } //         0.00</TIPAMOUNT                 
    public string TAXAMOUNT        { get; set; } //         0.00</TAXAMOUNT                 
    public string CREDITAMOUNT     { get; set; } //            0.00</CREDITAMOUNT                 
    public string AUTHCODE         { get; set; } //        281195</AUTHCODE                 
    public PaywireResult RESULT           { get; set; } //      CAPTURED</RESULT                 
    public string NAME             { get; set; } //    Test Customer</NAME                 
    public string RECURRINGID      { get; set; } //           0</RECURRINGID                 
    public string TRANSTIME        { get; set; } //         11/09/2020 05:54 PM</TRANSTIME                 
    public string PWINVOICENUMBER  { get; set; } //               test12345</PWINVOICENUMBER
    public string PWCUSTOMID1      { get; set; } //            sPWCUSTOMID1
    public string PWCUSTOMID2      { get; set; } //           c211021fdc26324095c1111111111a0a</c                 
    public string SETTLEMENTDATE   { get; set; } //              2021-01-12</SETTLEMENTDATE
    public string RESPONSETEXT { get; set; } //         CVV2 Mismatch</RESPONSETEXT>

}