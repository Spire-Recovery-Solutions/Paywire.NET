using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.StoreToken
{
 
    public class StoreTokenResponse : BasePaywireResponse
    {
        /// <summary>
        /// Unique identifier for the batch containing the transaction.
        /// </summary>
        public string BATCHID { get; set; }
        /// <summary>
        /// Method of payment with which the transaction was processed: E for web ACH, C for Card.
        /// </summary>
        public string PAYMETH { get; set; }
        /// <summary>
        /// The unique ID assigned by Paywire associated with this transaction.
        /// </summary>
        public string PWUNIQUEID { get; set; }
        /// <summary>
        /// The account holder's name that was supplied.
        /// </summary>
        public string AHNAME { get; set; }
        /// <summary>
        /// Amount of the transaction total including any adjustments and taxes. Maximum 7 digits, excluding decimals.
        /// </summary>
        public string AMOUNT { get; set; }
        /// <summary>
        /// Masked Card or Account number.
        /// </summary>
        public string MACCOUNT { get; set; }
        /// <summary>
        /// The card type used. This field is blank if PAYMETH is E.
        /// </summary>
        public string CCTYPE { get; set; }
        /// <summary>
        /// 'Consumer Fee'-enabled merchants only: Amount of the service adjustment. Maximum 7 digits, excluding decimals.
        /// </summary>
        public string PWADJAMOUNT { get; set; }
        /// <summary>
        /// 'Consumer Fee'-enabled merchants only: Amount of the sales tax calculated based on the 'Sales Tax Flat Rate %' set in the merchant configuration. Maximum 7 digits, excluding decimals.
        /// </summary>
        public string PWSALETAX { get; set; }
        /// <summary>
        /// Original Sale Amount, before any markups or discounts. Max 7 digits, excluding decimals.
        /// </summary>
        public string PWSALEAMOUNT { get; set; }
        /// <summary>
        /// Transaction AVS code result. Refer to AVS Codes table.
        /// </summary>
        /// <see cref="https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html?csharp#avs-codes"/>
        public string AVSCODE { get; set; }
        /// <summary>
        /// Paywire Customer Identifier. Will be returned if ADDCUSTOMER is set to TRUE in the request.
        /// </summary>
        public string PWCID { get; set; }

    }
}
