using System.Security.AccessControl;
using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.GetConsumerFee
{
    [XmlRoot("PAYMENTRESPONSE")]
    public class GetConsumerFeeResponse : BasePaywireResponse
    {
        public string PAYMETH { get; set; }
        public string PWADJDESC { get; set; }
        public double PWSALETAX { get; set; }
        public double PWADJAMOUNT { get; set; }
        public double AMOUNT { get; set; }


        [XmlElement("CDSUMMARY")]
        public ConsumeFeeSummary CDSUMMARY { get; set; }
        /*
         * Response Parameters
Parameter	Type	Description	Options

PAYMETH	string	Describes the payment method.	C: Card, A: ACH
PWADJDESC	string	The description for the adjustment as set in the merchant configuration.	
PWSALETAX	decimal	The tax amount calculated by the gateway, based on the Sales Tax rate set in the merchant configuration.	
PWADJAMOUNT	decimal	The adjustment amount calculated by the gateway, based on the Adjustment rate or fixed amount set in the merchant configuration. This can be either the Cash Discount markdown or the Convenience Fee.	
PWSALEAMOUNT	decimal	The Sale amount submitted in the request.	
AMOUNT	decimal	The total amount of the transaction, including tax and any adjustments.	
PWINVOICENUMBER	string	The merchant's unique invoice number associated with this transaction.
        
         */
    }


    public class ConsumeFeeSummary
    {
        public string MERCHANTNAME { get; set; }

        public string MID { get; set; }

        public string MERCHANTTYPE { get; set; }

        public double ADJTAXRATE { get; set; }

        public double CARDSALESAMOUNT { get; set; }

        public double CARDADJAMOUNT { get; set; }

        public double CARDTAXAMOUNT { get; set; }

        public double CARDTRANSACTIONAMOUNT { get; set; }

        public double CARDAMOUNTBEFORETAX { get; set; }

        public double CASHSALESAMOUNT { get; set; }

        public double CASHTAXAMOUNT { get; set; }

        public double CASHTRANSACTIONAMOUNT { get; set; }

        public double CASHAMOUNTBEFORETAX { get; set; }

        [XmlIgnore]
        public string CDDESCRIPTIONVPOS { get; set; }

        [XmlIgnore]
        public string CDDESCRIPTIONOSBP { get; set; }

        public string AHNAME { get; set; }

        public string MACCOUNT { get; set; }

        public string ROUTINGNUMBER { get; set; }

        public string BANKACCTTYPE { get; set; }

        public string EXP_MM { get; set; }

        public string EXP_YY { get; set; }

        public string FIRSTNAME { get; set; }

        public string LASTNAME { get; set; }

        /*
         *   // CDSUMMARY ELEMENT
MERCHANTNAME	string	Name of the merchant as set in the merchant configuration.	
MID	string	The processor's merchant identifier.	
MERCHANTTYPE	string	The type of merchant as set in the merchant configuration.
        A: General + Single SAP,
        B: Medical,
        C: General + Split SAP,
        D: Remote Check + SAP Invoices,
        E: Cash Discount,
        F: Convenience Fees
ADJTAXRATE	decimal	The Sales Tax rate as set in the merchant configuration or submitted in the request.	
CARDSALESAMOUNT	decimal	The Card Sale amount before tax and any adjustments. Relevant for Cash Discount.	
CARDADJAMOUNT	decimal	The Adjustment amount for a Card transaction. Relevant for Cash Discount.	
CARDTAXAMOUNT	decimal	The calculated Sales Tax amount for a Card transaction. Relevant for Cash Discount.	
CARDTRANSACTIONAMOUNT	decimal	The total amount for a Card transaction after tax and any adjustments. Relevant for Cash Discount.	
CARDAMOUNTBEFORETAX	decimal	The adjusted amount for a Card transaction before adding tax. Relevant for Cash Discount.	
CASHSALESAMOUNT	decimal	The Cash Sale amount before tax and any adjustments. Relevant for Cash Discount.	
CASHTAXAMOUNT	decimal	The calculated Sales Tax amount for a Cash transaction. Relevant for Cash Discount.	
CASHTRANSACTIONAMOUNT	decimal	The total amount for a Cash transaction after tax and any adjustments. Relevant for Cash Discount.	
CASHAMOUNTBEFORETAX	decimal	The adjusted amount for a Cash transaction before adding tax. Relevant for Cash Discount.	
CDDESCRIPTIONVPOS	string	The descriptive text set in the merchant configuration. To be displayed on the VPOS payment page.	
CDDESCRIPTIONOSBP	string	The descriptive text set in the merchant config. to be displayed on the OSBP payment page.	
AHNAME	string	ACH Account Holder full name. Returned only when ECHECK in PWMEDIA and a valid PWTOKEN are submitted in the request.	
MACCOUNT	string	Masked Card or Account number. Returned only when a valid PWTOKEN is submitted in the request.	
ROUTINGNUMBER	string	U.S. Bank Account routing number. Returned only when ECHECK in PWMEDIA and a valid PWTOKEN are submitted in the request.	
BANKACCTTYPE	string	Type of Bank Account. Returned only when ECHECK in PWMEDIA and a valid PWTOKEN are submitted in the request.	CHECKING, SAVINGS
EXP_MM	string	Card Expiry month. Returned only when CC in PWMEDIA and a valid PWTOKEN are submitted in the request.	
EXP_YY	string	Card Expiry year. Returned only when CC in PWMEDIA and a valid PWTOKEN are submitted in the request.	
FIRSTNAME	string	Account Holder first name. Returned only when a valid PWTOKEN is submitted in the request.	
LASTNAME	string	Account Holder last name. Returned only when a valid PWTOKEN is submitted in the request.
         */
    }

}
