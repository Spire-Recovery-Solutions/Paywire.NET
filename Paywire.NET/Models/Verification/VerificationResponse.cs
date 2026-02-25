using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.Verification
{

    public class VerificationResponse : BasePaywireResponse
    {
        /// <summary>
        /// Batch number
        /// </summary>
        public string BATCHID { get; set; } = string.Empty;

        /// <summary>
        /// Authorization code associated with the transaction
        /// </summary>
        public string AUTHCODE { get; set; } = string.Empty;

        /// <summary>
        /// Transaction AVS code result. Refer to the AVS Codes table.
        /// </summary>
        /// <see cref="https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html?csharp#avs-codes"/>
        public string AVSCODE { get; set; } = string.Empty;

        /// <summary>
        /// CVV response code.
        /// </summary>
        public string CVVCODE { get; set; } = string.Empty;

        /// <summary>
        /// Payment method.
        /// </summary>
        public string PAYMETH { get; set; } = string.Empty;

        /// <summary>
        /// The Paywire Unique ID returned in the Initialize response.
        /// </summary>
        public string PWUNIQUEID { get; set; } = string.Empty;

        /// <summary>
        /// Payment amount.
        /// </summary>
        public string AMOUNT { get; set; } = string.Empty;

        /// <summary>
        /// Masked credit card number.
        /// </summary>
        public string MACCOUNT { get; set; } = string.Empty;

        /// <summary>
        /// Credit card type.
        /// </summary>
        public string CCTYPE { get; set; } = string.Empty;

        /*
         * RESULT	string	China UnionPay transaction result.
BATCHID	int	Batch number.
PWCLIENTID	string	Paywire client ID.
AUTHCODE	string	Authorization code associated with the transaction.
AVSCODE	string	Transaction AVS code result. Refer to the AVS Codes table
CVVCODE	string	CVV response code.
PAYMETH	string	Payment method.
PWUNIQUEID	int	The Paywire Unique ID returned in the Initialize response.	3
AMOUNT	decimal	Payment amount.
MACCOUNT	string	Masked credit card number.
CCTYPE	string	Credit card type.
PWCUSTOMID2	string	Client custom ID.
         */
    }
}
