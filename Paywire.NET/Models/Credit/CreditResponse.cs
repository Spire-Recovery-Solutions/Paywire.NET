using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.Credit
{
    public class CreditResponse : BasePaywireResponse
    {
        /// <summary>
        /// Unique identifier for the batch containing the transaction.
        /// </summary>
        public string BATCHID { get; set; } = string.Empty;
        /// <summary>
        /// Method of payment with which the transaction was processed: E for web ACH, C for Card.
        /// </summary>
        public string PAYMETH { get; set; } = string.Empty;
        /// <summary>
        /// The unique ID assigned by Paywire associated with this transaction.
        /// </summary>
        public string PWUNIQUEID { get; set; } = string.Empty;
        /// <summary>
        /// The account holder's name that was supplied.
        /// </summary>
        public string AHNAME { get; set; } = string.Empty;
        /// <summary>
        /// Amount of the transaction total including any adjustments and taxes. Maximum 7 digits, excluding decimals.
        /// </summary>
        public string AMOUNT { get; set; } = string.Empty;
        /// <summary>
        /// Masked Card or Account number.
        /// </summary>
        public string MACCOUNT { get; set; } = string.Empty;
        /// <summary>
        /// The user's email address that was supplied at the start of the transaction.
        /// </summary>
        public string EMAIL { get; set; } = string.Empty;
        /// <summary>
        /// The card type used. This field is blank if PAYMETH is E.
        /// </summary>
        public string CCTYPE { get; set; } = string.Empty;

    }
}
