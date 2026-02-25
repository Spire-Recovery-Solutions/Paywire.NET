using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.SearchTransactions;

public class SearchTransactionsResponse : BasePaywireResponse
{
    [XmlElement("SEARCHRESULT")]
    public SearchResult SEARCH_RESULTS { get; set; } = new();

}

[XmlType("SearchTransactionResult")]
public class SearchResult
{
    [XmlElement("PWPAYDETAIL")]
    public PaywirePaymentDetail[] PAYMENT_DETAILS { get; set; } = [];

}

public class PaywirePaymentDetail
{
    public string PWUID { get; set; } = string.Empty; //     281195</PWUID
    public string BATCHID { get; set; } = string.Empty; //       526</BATCHID
    public string TRANSTYPE { get; set; } = string.Empty; //         SALE</TRANSTYPE
    public string CARDTYPE { get; set; } = string.Empty; //        VISA</CARDTYPE
    public string CARDNUM { get; set; } = string.Empty; //       X1111</CARDNUM
    public string SALEAMOUNT { get; set; } = string.Empty; //          30.00</SALEAMOUNT
    public string TRANSAMOUNT { get; set; } = string.Empty; //           30.00</TRANSAMOUNT
    public string ADJAMOUNT { get; set; } = string.Empty; //         0.00</ADJAMOUNT
    public string TIPAMOUNT { get; set; } = string.Empty; //         0.00</TIPAMOUNT
    public string TAXAMOUNT { get; set; } = string.Empty; //         0.00</TAXAMOUNT
    public string CREDITAMOUNT { get; set; } = string.Empty; //            0.00</CREDITAMOUNT
    public string AUTHCODE { get; set; } = string.Empty; //        281195</AUTHCODE
    //

    [XmlElement("RESULT")] public string RAW_RESULT { get; set; } = string.Empty;

    /// <summary>
    /// Status for the request.	APPROVAL, DECLINED, ERROR, SUCCESS, CAPTURED, CHARGEBACK
    /// </summary>
    [XmlIgnore]
    public PaywireResult RESULT
    {
        get
        {
            var canParse = Enum.TryParse(typeof(PaywireResult), RAW_RESULT, true, out var parsed);
            if (canParse)
            {
                return (PaywireResult)(parsed ?? PaywireResult.Unknown);
            }

            return RAW_RESULT.ToUpper() == "APPROVED" ? PaywireResult.Approval : PaywireResult.Unknown;
        }
        set => throw new NotImplementedException();
    }

    public string NAME { get; set; } = string.Empty; //    Test Customer</NAME
    public string RECURRINGID { get; set; } = string.Empty; //           0</RECURRINGID
    public string TRANSTIME { get; set; } = string.Empty; //         11/09/2020 05:54 PM</TRANSTIME
    public string PWINVOICENUMBER { get; set; } = string.Empty; //               test12345</PWINVOICENUMBER
    public string PWCUSTOMID1 { get; set; } = string.Empty; //            sPWCUSTOMID1
    public string PWCUSTOMID2 { get; set; } = string.Empty; //           c211021fdc26324095c1111111111a0a</c
    public string SETTLEMENTDATE { get; set; } = string.Empty; //              2021-01-12</SETTLEMENTDATE
    public string RESPONSETEXT { get; set; } = string.Empty; //         CVV2 Mismatch</RESPONSETEXT>
    public string DESCRIPTION { get; set; } = string.Empty; //            Transaction custom description message.
    /// <summary>
    /// Original transaction ID for VOID/CREDIT transactions.
    /// </summary>
    public string ORGTRANSID { get; set; } = string.Empty;
    /// <summary>
    /// Paywire customer identifier.
    /// </summary>
    public string CUSTOMERID { get; set; } = string.Empty;
    /// <summary>
    /// ACH split-fee identifier.
    /// </summary>
    public string PWCUSTOMID3 { get; set; } = string.Empty;
    /// <summary>
    /// Periodic payment indicator: C = Customer-initiated, I = Initial, R = Recurring, T = Installment.
    /// </summary>
    public string POSINDICATOR { get; set; } = string.Empty;

}
