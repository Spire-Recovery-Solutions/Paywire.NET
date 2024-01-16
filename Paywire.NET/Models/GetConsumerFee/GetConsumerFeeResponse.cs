using System.Security.AccessControl;
using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.GetConsumerFee
{

    public class GetConsumerFeeResponse : BasePaywireResponse
    {
        /// <summary>
        /// Describes the payment method.	C: Card, A: ACH
        /// </summary>
        public string PAYMETH { get; set; }

        /// <summary>
        /// The description for the adjustment as set in the merchant configuration.	
        /// </summary>
        public string PWADJDESC { get; set; }

        /// <summary>
        /// The tax amount calculated by the gateway, based on the Sales Tax rate set in the merchant configuration.	
        /// </summary>
        public double PWSALETAX { get; set; }

        /// <summary>
        /// 	The adjustment amount calculated by the gateway, based on the Adjustment rate or fixed amount set in the merchant configuration. This can be either the Cash Discount markdown or the Convenience Fee.	
        /// </summary>
        public double PWADJAMOUNT { get; set; }

        /// <summary>
        /// The total amount of the transaction, including tax and any adjustments.	
        /// </summary>
        public double AMOUNT { get; set; }


        [XmlElement("CDSUMMARY")] public ConsumerFeeSummary CDSUMMARY { get; set; }

    }

    public class ConsumerFeeSummary
    {
        /// <summary>
        /// Name of the merchant as set in the merchant configuration.	
        /// </summary>
        public string MERCHANTNAME { get; set; }

        /// <summary>
        /// The processor's merchant identifier.	
        /// </summary>
        public string MID { get; set; }

        /// <summary>
        /// The type of merchant as set in the merchant configuration. A: General + Single SAP, B: Medical, C: General + Split SAP, D: Remote Check + SAP Invoices, E: Cash Discount, F: Convenience Fees
        /// </summary>
        public string MERCHANTTYPE { get; set; }

        /// <summary>
        /// The Sales Tax rate as set in the merchant configuration or submitted in the request.	
        /// </summary>
        public double ADJTAXRATE { get; set; }

        /// <summary>
        /// The Card Sale amount before tax and any adjustments. Relevant for Cash Discount.
        /// </summary>
        public double CARDSALESAMOUNT { get; set; }

        /// <summary>
        /// The Adjustment amount for a Card transaction. Relevant for Cash Discount.	
        /// </summary>
        public double CARDADJAMOUNT { get; set; }

        /// <summary>
        /// The calculated Sales Tax amount for a Card transaction. Relevant for Cash Discount.	
        /// </summary>
        public double CARDTAXAMOUNT { get; set; }

        /// <summary>
        /// The total amount for a Card transaction after tax and any adjustments. Relevant for Cash Discount.
        /// </summary>
        public double CARDTRANSACTIONAMOUNT { get; set; }

        /// <summary>
        /// The adjusted amount for a Card transaction before adding tax. Relevant for Cash Discount.	
        /// </summary>
        public double CARDAMOUNTBEFORETAX { get; set; }

        /// <summary>
        /// The Cash Sale amount before tax and any adjustments. Relevant for Cash Discount.	
        /// </summary>
        public double CASHSALESAMOUNT { get; set; }

        /// <summary>
        /// The calculated Sales Tax amount for a Cash transaction. Relevant for Cash Discount.	
        /// </summary>
        public double CASHTAXAMOUNT { get; set; }

        /// <summary>
        /// The total amount for a Cash transaction after tax and any adjustments. Relevant for Cash Discount.	
        /// </summary>
        public double CASHTRANSACTIONAMOUNT { get; set; }

        /// <summary>
        /// The adjusted amount for a Cash transaction before adding tax. Relevant for Cash Discount.	
        /// </summary>
        public double CASHAMOUNTBEFORETAX { get; set; }

        /// <summary>
        /// The descriptive text set in the merchant configuration. To be displayed on the VPOS payment page.	
        /// </summary>
        [XmlIgnore]
        [Obsolete("The line breaks contained in the response break XML parsing.")]
        public string CDDESCRIPTIONVPOS { get; set; }

        /// <summary>
        /// The descriptive text set in the merchant config. to be displayed on the OSBP payment page.	
        /// </summary>
        [XmlIgnore]
        [Obsolete("The line breaks contained in the response break XML parsing.")]
        public string CDDESCRIPTIONOSBP { get; set; }

        /// <summary>
        /// ACH Account Holder full name. Returned only when ECHECK in PWMEDIA and a valid PWTOKEN are submitted in the request.	
        /// </summary>
        public string AHNAME { get; set; }

        /// <summary>
        /// Masked Card or Account number. Returned only when a valid PWTOKEN is submitted in the request.	
        /// </summary>
        public string MACCOUNT { get; set; }

        /// <summary>
        /// U.S. Bank Account routing number. Returned only when ECHECK in PWMEDIA and a valid PWTOKEN are submitted in the request.	
        /// </summary>
        public string ROUTINGNUMBER { get; set; }

        /// <summary>
        /// Type of Bank Account. Returned only when ECHECK in PWMEDIA and a valid PWTOKEN are submitted in the request.
        /// </summary>
        public string BANKACCTTYPE { get; set; }

        /// <summary>
        /// Expiry month. Returned only when CC in PWMEDIA and a valid PWTOKEN are submitted in the request.	
        /// </summary>
        public string EXP_MM { get; set; }

        /// <summary>
        /// Card Expiry year. Returned only when CC in PWMEDIA and a valid PWTOKEN are submitted in the request.
        /// </summary>
        public string EXP_YY { get; set; }

        /// <summary>
        /// 	Account Holder first name. Returned only when a valid PWTOKEN is submitted in the request.	
        /// </summary>
        public string FIRSTNAME { get; set; }

        /// <summary>
        /// Account Holder last name. Returned only when a valid PWTOKEN is submitted in the request.
        /// </summary>
        public string LASTNAME { get; set; }

    }

}
