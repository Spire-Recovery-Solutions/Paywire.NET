using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.Verification
{
    [XmlRoot("PAYMENTRESPONSE")]
    public class VerificationResponse : BasePaywireResponse
    {
        /// <summary>
        /// Batch number
        /// </summary>
        public int BATCHID { get; set; }

        /// <summary>
        /// Authorization code associated with the transaction
        /// </summary>
        public string AUTHCODE { get; set; }

        /// <summary>
        /// Transaction AVS code result. Refer to the AVS Codes table.
        /// </summary>
        /// <see cref="https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html?csharp#avs-codes"/>
        public string AVSCODE { get; set; }

        /// <summary>
        /// CVV response code.
        /// </summary>
        public string CVVCODE { get; set; }

        /// <summary>
        /// Payment method.
        /// </summary>
        public string PAYMETH { get; set; }

        /// <summary>
        /// The Paywire Unique ID returned in the Initialize response.
        /// </summary>
        public string PWUNIQUEID { get; set; }

        /// <summary>
        /// Payment amount.
        /// </summary>
        public double AMOUNT { get; set; }

        /// <summary>
        /// Masked credit card number.
        /// </summary>
        public string MACCOUNT { get; set; }

        /// <summary>
        /// Credit card type.
        /// </summary>
        public string CCTYPE { get; set; } 

        /// <summary>
        /// Client custom ID.
        /// </summary>
        public string PWCUSTOMID2 { get; set; }

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
