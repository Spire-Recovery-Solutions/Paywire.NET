using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.PreAuth
{
 
    public class PreAuthResponse : BasePaywireResponse
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
        /// The user's email address that was supplied at the start of the transaction.
        /// </summary>
        public string EMAIL { get; set; }
        /// <summary>
        /// The card type used. This field is blank if PAYMETH is E.
        /// </summary>
        public string CCTYPE { get; set; }
        /// <summary>
        /// Authorization code associated with the transaction, if applicable.
        /// </summary>
        public string AUTHCODE { get; set; }
        /// <summary>
        /// Transaction AVS code result. Refer to AVS Codes table.
        /// </summary>
        /// <see cref="https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html?csharp#avs-codes"/>
        public string AVSCODE { get; set; }
        /// <summary>
        /// Transaction CVV result: 1 for a match, 0 for a failure.
        /// </summary>
        public string CVVCODE { get; set; }
    }
}
