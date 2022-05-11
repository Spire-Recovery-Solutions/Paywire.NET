using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Models.Verification
{
    [XmlRoot("PAYMENTREQUEST")]
    public class VerificationRequest : BasePaywireRequest
    {
        [XmlElement("CUSTOMER")]
        public Customer Customer { get; set; }
        /*
        
         * PWSALEAMOUNT	int/decimal	Amount of the transaction. [In TransactionHeader]
         *
         * REQUESTTOKEN 	Returns a PWTOKEN in the response when set to TRUE.
         * PWMEDIA	string	Defines the payment method.	Fixed options: CC and ECHECK.
         * CARDNUMBER	int	Card number with which to process the payment. Required only when CC is submitted in PWMEDIA.
         * EXP_MM	string	Card expiry month. Required only when CC is submitted in PWMEDIA.	2/2, >0, <=12
         * EXP_YY	string	Card expiry year. Required only when CC is submitted in PWMEDIA.	2/2
         * CVV2	int	Card Verification Value. Required only when CC is submitted in PWMEDIA.	3/4
         * CUSTOMERNAME		string	Full name of the customer, possibly different than the Account Holder.
         * FIRSTNAME		string	Account Holder's first name.
         * LASTNAME		string	Account Holder's last name.
         * ADDRESS1		string	Account Holder's primary address.
         * ZIP		string	Account Holder's address postal/zip code. See important note on Zip Codes.
         * EMAIL		string	Account Holder's email address.
         * PRIMARYPHONE		string	Account Holder's primary phone number.
         */
    }
}
