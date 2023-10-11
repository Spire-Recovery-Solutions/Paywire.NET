using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.BatchInquiry;
using Paywire.NET.Models.BinValidation;
using Paywire.NET.Models.CloseBatch;
using Paywire.NET.Models.Credit;
using Paywire.NET.Models.GetAuthToken;
using Paywire.NET.Models.GetConsumerFee;
using Paywire.NET.Models.PreAuth;
using Paywire.NET.Models.Receipt;
using Paywire.NET.Models.RemoveToken;
using Paywire.NET.Models.Sale;
using Paywire.NET.Models.SearchChargebacks;
using Paywire.NET.Models.SearchTransactions;
using Paywire.NET.Models.StoreToken;
using Paywire.NET.Models.TokenSale;
using Paywire.NET.Models.Verification;
using Paywire.NET.Models.Void;

namespace Paywire.NET.Factories
{
    public static class PaywireRequestFactory
    {
        /// <summary>
        /// Returns your Paywire AuthToken. As long as the client was properly configured, the header here can be left out. 
        /// </summary>
        public static GetAuthTokenRequest GetAuthToken(TransactionHeader header)
        {
            return new GetAuthTokenRequest { TransactionHeader = header };
        }

        /// <summary>
        /// Returns your Paywire AuthToken. As long as the client was properly configured, the header here can be left out. 
        /// </summary>
        public static GetAuthTokenRequest GetAuthToken()
        {
            return new GetAuthTokenRequest();
        }

        /// <summary>
        /// Credit a transaction. The transaction amount must be equal to or less than the amount to credit, and the PWUNIQUEID must match the unique identifier associated with the transaction to credit. Only transactions in a closed batch with a status of SETTLED can be credited.
        /// </summary>
        public static CreditRequest Credit(TransactionHeader header)
        {
            return new CreditRequest() { TransactionHeader = header };
        }

        public static CreditRequest Credit(double saleAmount, string invoiceNumber, string uniqueId)
        {
            return new CreditRequest
            {
                TransactionHeader = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount, PWINVOICENUMBER = invoiceNumber, PWUNIQUEID = uniqueId
                }
            };
        }

        /// <summary>
        /// Pre-authorize a card.
        /// </summary>
        public static PreAuthRequest PreAuth(TransactionHeader header, Customer customer)
        {
            return new PreAuthRequest { TransactionHeader = header, Customer = customer };
        }

        public static PreAuthRequest PreAuth(double saleAmount, Customer customer)
        {
            return new PreAuthRequest { TransactionHeader = new TransactionHeader { PWSALEAMOUNT = saleAmount }, Customer = customer };
        }

        public static PreAuthRequest PreAuth(double saleAmount, string payorState, long cardNumber, string expMM, string expYY, int cvv2) // string payorFirstName, string payorLastName, string payorAddress1, string payorAddress2, string payorCity, string payorZip,
        {
            return new PreAuthRequest
            {
                TransactionHeader = new TransactionHeader { PWSALEAMOUNT = saleAmount }, 
                Customer = new Customer
                {
                    STATE = payorState,
                    CARDNUMBER = cardNumber,
                    CVV2 = cvv2,
                    EXP_MM = expMM,
                    EXP_YY = expYY
                }
            };
        }


        /// <summary>
        /// Void a transaction. The transaction amount must match the amount of the original transaction, and the PWUNIQUEID must match the unique identifier associated with the transaction to void. The transaction must be in the current open batch to void it.
        /// </summary>
        public static VoidRequest Void(TransactionHeader header)
        {
            return new VoidRequest { TransactionHeader = header };
        }

        public static VoidRequest Void(double saleAmount, string invoiceNumber, string uniqueId)
        {
            return new VoidRequest { TransactionHeader = new TransactionHeader { PWSALEAMOUNT = saleAmount, PWINVOICENUMBER = invoiceNumber, PWUNIQUEID = uniqueId } };
        }

        /// <summary>
        /// Input the sale amount to get adjustment, tax, and total transaction amounts. Relevant for merchants configured with Cash Discount or Convenience Fees.
        /// </summary>
        public static GetConsumerFeeRequest GetConsumerFee(TransactionHeader header, Customer customer)
        {
            return new GetConsumerFeeRequest()
            {
                TransactionHeader = header,
                Customer = customer
            };
        }

        public static GetConsumerFeeRequest GetConsumerFee(double saleAmount, string media, string state, string disableCF = "FALSE", string token = "")
        {
            return new GetConsumerFeeRequest
            {
                TransactionHeader = new TransactionHeader{ PWSALEAMOUNT = saleAmount, DISABLECF = disableCF},
                Customer = new Customer {PWMEDIA = media, STATE = state, PWTOKEN = token}
            };
        }

        /// <summary>
        /// Validate a card and return a token.
        /// </summary>
        public static StoreTokenRequest StoreToken(TransactionHeader header, Customer customer)
        {
            return new StoreTokenRequest{TransactionHeader = header, Customer = customer};
        }

        public static StoreTokenRequest StoreCreditCardToken(double saleAmount, long cardNumber, string expMM, string expYY, int cvv2, string companyName = "", string firstName = "", string lastName = "", string email = "", string address = "",  string address2 = "", string city = "", string state = "", string country = "", string zip = "", string primaryPhone = "", string workPhone = "", string pwCid = "", string addCustomer = "FALSE")
        {
            return new StoreTokenRequest
            {
                TransactionHeader = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount,
                },
                Customer = new Customer()
                {
                    PWMEDIA = "CC",
                    CARDNUMBER = cardNumber,
                    EXP_MM = expMM,
                    EXP_YY = expYY,
                    CVV2 = cvv2,
                    COMPANYNAME = companyName,
                    FIRSTNAME = firstName,
                    LASTNAME = lastName,
                    EMAIL = email,
                    ADDRESS1 = address,
                    ADDRESS2 = address2,
                    CITY = city,
                    STATE = state,
                    COUNTRY = country,
                    ZIP = zip,
                    PRIMARYPHONE = primaryPhone,
                    WORKPHONE = workPhone,
                    PWCID = pwCid,
                    ADDCUSTOMER = addCustomer
                }
            };
        }

        public static StoreTokenRequest StoreCheckToken(double saleAmount, string routingNumber, string accountNumber, string bankAccountType, string companyName = "", string firstName = "", string lastName = "", string email = "", string address = "", string address2 = "", string city = "", string state = "", string country = "", string zip = "", string primaryPhone = "", string workPhone = "", string pwCid = "", string addCustomer = "FALSE")
        {
            return new StoreTokenRequest
            {
                TransactionHeader = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount,
                },
                Customer = new Customer()
                {
                    PWMEDIA = "ECHECK",
                    ROUTINGNUMBER = routingNumber,
                    ACCOUNTNUMBER = accountNumber,
                    BANKACCTTYPE = bankAccountType,
                    COMPANYNAME = companyName,
                    FIRSTNAME = firstName,
                    LASTNAME = lastName,
                    EMAIL = email,
                    ADDRESS1 = address,
                    ADDRESS2 = address2,
                    CITY = city,
                    STATE = state,
                    COUNTRY = country,
                    ZIP = zip,
                    PRIMARYPHONE = primaryPhone,
                    WORKPHONE = workPhone,
                    PWCID = pwCid,
                    ADDCUSTOMER = addCustomer
                }
            };
        }

        /// <summary>
        /// Process a payment using a token. Simply replace the Card or E-Check information with the PWTOKEN returned by the gateway when storing tokens.
        /// </summary>
        public static TokenSaleRequest TokenSale(TransactionHeader header, Customer customer)
        {
            return new TokenSaleRequest { TransactionHeader = header, Customer = customer };
        }

        public static TokenSaleRequest TokenSale(double saleAmount, string token, string state, string DISABLECF = "FALSE")
        {
            return new TokenSaleRequest
            {
                TransactionHeader = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount,
                    DISABLECF = DISABLECF
                },
                Customer = new Customer
                {
                    PWTOKEN = token,
                    STATE = state
                }
            };
        }

        /// <summary>
        /// Verification transaction will verify the customer's card or bank account before submitting the payment.
        /// </summary>
        /// <returns></returns>
        public static VerificationRequest Verification(Customer customer)
        {
            return new VerificationRequest
            {
                Customer = customer
            };
        }

        public static VerificationRequest Verification(double saleAmount, long cardNumber, string expMM, string expYY, int cvv2, string firstName = "", string lastName = "", string address = "", string address2 = "", string zip = "", string email = "", string primaryPhone = "")
        {
            return new VerificationRequest
            {
                TransactionHeader = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount
                },
                Customer = new Customer
                {
                    PWMEDIA = "CC",
                    CARDNUMBER = cardNumber,
                    EXP_MM = expMM,
                    EXP_YY = expYY,
                    CVV2 = cvv2,
                    FIRSTNAME = firstName,
                    LASTNAME = lastName,
                    ADDRESS1 = address,
                    ADDRESS2 = address2, // This field was not in the docs but we can assume it's there...
                    ZIP = zip,
                    EMAIL = email,
                    PRIMARYPHONE = primaryPhone
                }
            };
        }
        /// <summary>
        /// Query the database for transaction results.
        /// </summary>
        public static SearchTransactionsRequest SearchTransactions(TransactionHeader header, SearchCondition search)
        {
            return new SearchTransactionsRequest()
            {
                TransactionHeader = header,
                SearchCondition = search
            };
        }

        public static SearchTransactionsRequest SearchTransactions(SearchCondition search)
        {
            return new SearchTransactionsRequest()
            {
                TransactionHeader = new TransactionHeader{ XOPTION = "TRUE" },
                SearchCondition = search
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
        public static SaleRequest CardSale(TransactionHeader header, Customer customer)
        {
            var request = new SaleRequest
            {
                TransactionHeader = header,
                Customer = customer
            };

            return request;
        }

        /// <summary>
        /// Charge a card.
        /// </summary>
        /// <returns></returns>
        public static SaleRequest OneTimeCardPayment(double saleAmount, long cardNumber, string expMM, string expYY, int cvv2, string companyName = "", string firstName = "", string lastName = "", string email = "", string address = "",  string address2 = "", string city = "", string state = "", string country = "", string zip = "", string primaryPhone = "", string workPhone = "")
        {
            // TODO: Check what field addCustomer goes into
            return new SaleRequest
            {
                TransactionHeader = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount,
                },
                Customer = new Customer()
                {
                    PWMEDIA = "CC",
                    CARDNUMBER = cardNumber,
                    EXP_MM = expMM,
                    EXP_YY = expYY,
                    CVV2 = cvv2,
                    COMPANYNAME = companyName,
                    FIRSTNAME = firstName,
                    LASTNAME = lastName,
                    EMAIL = email,
                    ADDRESS1 = address,
                    ADDRESS2 = address2,
                    CITY = city,
                    STATE = state,
                    COUNTRY = country,
                    ZIP = zip,
                    PRIMARYPHONE = primaryPhone,
                    WORKPHONE = workPhone
                }
            };
        }

        /// <summary>
        /// Charge a bank account.
        /// </summary>
        /// <returns></returns>
        public static SaleRequest OneTimeCheckPayment(double saleAmount, string routingNumber, string accountNumber, string bankAccountType, string companyName = "", string firstName = "", string lastName = "", string email = "", string address = "", string address2 = "", string city = "", string state = "", string country = "", string zip = "", string primaryPhone = "", string workPhone = "")
        {
            return new SaleRequest
            {
                TransactionHeader = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount,
                },
                Customer = new Customer()
                {
                    PWMEDIA = "ECHECK",
                    ROUTINGNUMBER = routingNumber,
                    ACCOUNTNUMBER = accountNumber,
                    BANKACCTTYPE = bankAccountType,
                    COMPANYNAME = companyName,
                    FIRSTNAME = firstName,
                    LASTNAME = lastName,
                    EMAIL = email,
                    ADDRESS1 = address,
                    ADDRESS2 = address2,
                    CITY = city,
                    STATE = state,
                    COUNTRY = country,
                    ZIP = zip,
                    PRIMARYPHONE = primaryPhone,
                    WORKPHONE = workPhone,
                }
            };
        }

        /// <summary>
        /// Send a receipt for an existing transaction.
        /// </summary>
        /// <param name="header"
        /// <param name="customer"
        /// <returns></returns>
        public static SendReceiptRequest SendReceipt(TransactionHeader header, Customer customer)
        {
            return new SendReceiptRequest
            {
                TransactionHeader = header,
                Customer = customer
            };
        }

        /// <summary>
        /// Query the database for Chargeback transactions.
        /// </summary>
        public static SearchChargebackRequest SearchChargeback(TransactionHeader header, SearchCondition search)
        {
            return new SearchChargebackRequest()
            {
                TransactionHeader = header,
                SearchCondition = search
            };
        }

        public static SearchChargebackRequest SearchChargeback(SearchCondition search)
        {
            return new SearchChargebackRequest()
            {
                TransactionHeader = new TransactionHeader { XOPTION = "TRUE" },
                SearchCondition = search
            };
        }

        /// <summary>
        /// Bin Validation.
        /// </summary>
        /// <param name="header"
        /// <param name="customer"
        /// <returns></returns>
        public static BinValidationRequest BinValidation(TransactionHeader header, Customer customer)
        {
            return new BinValidationRequest
            {
                TransactionHeader = header,
                Customer = customer
            };
        }
        /// <summary>
        /// Close an open batch. 
        /// </summary>
        /// <param name="header"
        /// <returns></returns>
        public static CloseBatchRequest CloseBatch(TransactionHeader header)
        {
            return new CloseBatchRequest
            {
                TransactionHeader = header
            };
        }

        /// <summary>
        /// Remove a token
        /// </summary>
        /// <param name="header"
        /// <param name="customer"
        /// <returns></returns>
        public static RemoveTokenRequest RemoveToken(TransactionHeader header, Customer customer)
        {
            return new RemoveTokenRequest
            {
                TransactionHeader = header,
                Customer = customer
            };
        }

    }
}
