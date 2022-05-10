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
        public int BATCHID { get; set; }

        public string AUTHCODE { get; set; }

        public string AVSCODE { get; set; }

        public string CVVCODE { get; set; }

        public string PAYMETH { get; set; }

        public string PWUNIQUEID { get; set; }

        public double AMOUNT { get; set; }

        public string MACCOUNT { get; set; }

        public string CCTYPE { get; set; } 
        public string PWCUSTIMID2 { get; set; }

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
