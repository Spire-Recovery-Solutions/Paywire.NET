using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.BatchInquiry;
using Paywire.NET.Models.GetAuthToken;
using Paywire.NET.Models.GetConsumerFee;
using Paywire.NET.Models.Sale;
using Paywire.NET.Models.SearchTransactions;
using Paywire.NET.Models.Verification;

namespace Paywire.NET.Factories
{
    public static class PaywireRequestFactory
    {
        /// <summary>
        /// Credit a transaction. The transaction amount must be equal to or less than the amount to credit, and the PWUNIQUEID must match the unique identifier associated with the transaction to credit. Only transactions in a closed batch with a status of SETTLED can be credited.
        /// </summary>
        public static void Credit()
        {

        }
        /// <summary>
        /// Pre-authorize a card.
        /// </summary>
        public static void PreAuth()
        {
        }

        /// <summary>
        /// Void a transaction. The transaction amount must match the amount of the original transaction, and the PWUNIQUEID must match the unique identifier associated with the transaction to void. The transaction must be in the current open batch to void it.
        /// </summary>
        public static void Void()
        {
        }

        /// <summary>
        /// Input the sale amount to get adjustment, tax, and total transaction amounts. Relevant for merchants configured with Cash Discount or Convenience Fees.
        /// </summary>
        public static GetConsumerFeeRequest GetConsumerFee()
        {
            return new GetConsumerFeeRequest()
            {
                TransactionHeader = new TransactionHeader()
                {
                    PWSALEAMOUNT = 0.01,
                    DISABLECF = "FALSE"
                },
                Customer = new Customer()
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
                    REQUESTTOKEN = "TRUE",
                    PWMEDIA = "CC",
                    CARDNUMBER = 4012301230123010,
                    CVV2 = "123",
                    EXP_YY = "22",
                    EXP_MM = "07",
                    FIRSTNAME = "CHRIS",
                    LASTNAME = "FROST",
                    PRIMARYPHONE = "7035551212",
                    EMAIL = "CFFROST@EMAILADDRESS.COM",
                    ADDRESS1 = "123",
                    CITY = "DENVER",
                    STATE = "CO",
                    ZIP = "80014",
                }
            };
        }
        /// <summary>
        /// Validate a card and return a token.
        /// </summary>
        public static void StoreToken()
        {


        }

        /// <summary>
        /// Verification transaction will verify the customer's card or bank account before submitting the payment.
        /// </summary>
        /// <returns></returns>
        public static VerificationRequest Verification()
        {
            return new VerificationRequest
            {
                Customer = new Customer()
                {
                    REQUESTTOKEN = "TRUE",
                    PWMEDIA = "CC",
                    CARDNUMBER = 4761739001010267,
                    CVV2 = "999",
                    EXP_YY = "22",
                    EXP_MM = "07",
                    FIRSTNAME = "John",
                    LASTNAME = "Doe",
                    PRIMARYPHONE = "7168675309",
                    EMAIL = "john@doe.com",
                    ADDRESS1 = "123 John St",
                    ZIP = "14094",
                }
            };
        }

        /// <summary>
        /// Query the database for transaction results.
        /// </summary>
        public static SearchTransactionsRequest SearchTransactions()
        {
            return new SearchTransactionsRequest()
            {
                TransactionHeader = new TransactionHeader()
                {
                    XOPTION = "TRUE"
                },
                SearchCondition = new SearchCondition()
                {
                    DateFrom = DateTime.Now.AddDays(-1),   //COND_DATEFROM			DateTime	Search date range from.	Date Format yyyy-mm-dd HH:MM.
                    DateTo = DateTime.Now.AddDays(1),      //COND_DATETO			DateTime	Search date range to.	Date Format yyyy-mm-dd HH:MM.
                    COND_PWCID = "",                       //COND_PWCID			    string	    Paywire Customer Identifier. When submitted, the created token will be associated with this customer.
                    COND_USERNAME = "",                    //COND_USERNAME			String	    Search by the USERNAME initiating the transaction.	
                    COND_UNIQUEID = "",                    //COND_UNIQUEID			int	        Search by transaction Unique ID returned by the gateway.	
                    COND_BATCHID = "",                     //COND_BATCHID			string	    Search by Batch ID returned by the gateway.	
                    COND_TRANSAMT = "",                    //COND_TRANSAMT			int/decimal	Search by transaction amount.	
                    COND_TRANSTYPE = "ALL",                //COND_TRANSTYPE         string	    Search by transaction type.	Fixed options: ALL, SALE, CREDIT, VOID
                    COND_RESULT = "",                      //COND_RESULT			string	    Search by transaction result returned by the gateway.	See Transaction Result values.
                    COND_CARDTYPE = "",                    //COND_CARDTYPE			string	    Search by the card type used for the transaction.	Fixed options: ALL, VISA, MC, DISC, AMEX, ACH, REMOTE
                    COND_LASTFOUR = "",                    //COND_LASTFOUR			int	        Search by the last four digits of the account or card used in the transaction searched.	4/4
                    COND_CUSTOMERID = "",                  //COND_CUSTOMERID		string	    Search by the Paywire customer identifier returned when creating a token.	
                    COND_RECURRINGID = "",                 //COND_RECURRINGID		int	        Search by the periodic identifier returned when creating a periodic plan.	
                    COND_PWINVOICENUMBER = "",             //COND_PWINVOICENUMBER	string	    Search by the merchant-submitted or Paywire-generated unique invoice number associated with the transaction.	0/20, Alphanumeric
                    COND_PWCUSTOMID1 = "",                 //COND_PWCUSTOMID1		string	    Search by the custom third-party identifier associated with the transaction.	
                }   
            };

        }

        /// <summary>
        /// Get the current open batch summary.
        /// </summary>
        public static BatchInquiryRequest BatchInquiry()
        {
            return new BatchInquiryRequest();
        }


        /// <summary>
        /// Charge a card or bank account (if applicable).
        /// </summary>
        /// <returns></returns>
        public static SaleRequest CardSale()
        {
            return new SaleRequest
            {
                TransactionHeader = new TransactionHeader()
                {
                    PWSALEAMOUNT = 0.01,
                    DISABLECF = "FALSE",
                    PWINVOICENUMBER = "TEST001"
                },
                Customer = new Customer
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
                    REQUESTTOKEN = "FALSE",
                    DESCRIPTION = "Description",
                    PWMEDIA = "CC",
                    CARDNUMBER = 4111111111111111,
                    CVV2 = "123",
                    EXP_YY = "33",
                    EXP_MM = "11",
                    FIRSTNAME = "CHRIS",
                    LASTNAME = "FROSTY",
                    PRIMARYPHONE = "7035551212",
                    EMAIL = "CFFROST@EMAILADDRESS.COM",
                    ADDRESS1 = "123",
                    ADDRESS2 = "",
                    CITY = "LOCKPORT",
                    STATE = "NY",
                    COUNTRY = "US",
                    ZIP = "55555",
                }
            };
        }
    }
}
