using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.Verification;

namespace Paywire.NET.Models.GetConsumerFee
{
    /// <summary>
    /// For merchants configured with Cash Discount or Convenience Fees, submit GETCONSUMERFEE in the <PWTRANSACTIONTYPE /> parameter to retrieve the adjustment amount.
    /// </summary>
    [XmlRoot("PAYMENTREQUEST")]
    public class GetConsumerFeeRequest : BasePaywireRequest
    {
        public GetConsumerFeeRequest()
        {
            TransactionHeader = new TransactionHeader
            {
                PWTRANSACTIONTYPE = PaywireTransactionType.GetConsumerFee,
                PWSALEAMOUNT = 100.00
            };
        }

        public string PWINVOICENUMBER { get; set; }

        //Customer Object
        [XmlElement("CUSTOMER")]
        public ConsumerFeeCustomer ConsumerFeeCustomer { get; set; } // Put the shit below into this... 


        /*
         * Parameter	        Required	Type	    Description	                                                            Validation
         * PWSALEAMOUNT	        x           int/decimal	Sale amount.
         * PWINVOICENUMBER		            string	    The merchant's unique invoice number associated with this transaction.
         * PWMEDIA	            x           string	    Defines the payment method.	Fixed options: CC and ECHECK.
         * DISABLECF		                bool	    Overrides applying a Cash Discount or Convenience Fee when set to TRUE, if configured. Note that Sales Tax will also be disabled.	Default: FALSE
         * ADJTAXRATE		                decimal	    Overrides the configured Sales Tax rate.
         * PWTOKEN		                    string	    When submitted, returns customer or token details in the response.
         * STATE	            x           string	    Account Holder's state of residence. Required if configured with Convenience Fees.	
         */
    }

    public class ConsumerFeeCustomer
    {
        public string PWMEDIA { get; set; } = "CC";
        public bool DISABLECF { get; set; }
        public double ADJTAXRATE { get; set; }
        public string PWTOKEN { get; set; }
        public string STATE { get; set; }   
    }
}
