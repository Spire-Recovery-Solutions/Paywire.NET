﻿using Paywire.NET.Models.Base;
using Paywire.NET.Models.BatchInquiry;
using Paywire.NET.Models.BinValidation;
using Paywire.NET.Models.Capture;
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
        /// <param name="header"/>
        public static GetAuthTokenRequest GetAuthToken(TransactionHeader header)
        {
            return new GetAuthTokenRequest { TRANSACTION_HEADER = header };
        }

        /// <summary>
        /// Returns your Paywire AuthToken. As long as the client was properly configured, the header here can be left out. 
        /// </summary>
        public static GetAuthTokenRequest GetAuthToken()
        {
            return new GetAuthTokenRequest
            {
                TRANSACTION_HEADER = new TransactionHeader()
            };
        }

        /// <summary>
        /// Credit a transaction. The transaction amount must be equal to or less than the amount to credit, and the PWUNIQUEID must match the unique identifier associated with the transaction to credit. Only transactions in a closed batch with a status of SETTLED can be credited.
        /// </summary>
        /// <param name="header"/>
        public static CreditRequest Credit(TransactionHeader header)
        {
            return new CreditRequest() { TRANSACTION_HEADER = header };
        }

        /// <summary>
        /// Credit a transaction. The transaction amount must be equal to or less than the amount to credit, and the PWUNIQUEID must match the unique identifier associated with the transaction to credit. Only transactions in a closed batch with a status of SETTLED can be credited.
        /// </summary>
        ///<paramref name="saleAmount"/>
        ///<paramref name="invoiceNumber"/>
        ///<paramref name="uniqueId"/>
        public static CreditRequest Credit(double saleAmount, string invoiceNumber, string uniqueId)
        {
            return new CreditRequest
            {
                TRANSACTION_HEADER = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount,
                    PWINVOICENUMBER = invoiceNumber,
                    PWUNIQUEID = uniqueId
                }
            };
        }

        public static CreditRequest Credit(TransactionHeader transactionHeader, Customer customer)
        {
            return new CreditRequest
            {
                TRANSACTION_HEADER = transactionHeader,
                CUSTOMER = customer
            };
        }

        /// <summary>
        /// Pre-authorize a card.
        /// </summary>
        /// <param name="header"/>
        /// <param name="customer"/>
        public static PreAuthRequest PreAuth(TransactionHeader header, Customer customer)
        {
            return new PreAuthRequest { TRANSACTION_HEADER = header, CUSTOMER = customer };
        }

        /// <summary>
        /// Pre-authorize a card.
        /// </summary>
        /// <param name="saleAmount"/>
        /// <param name="customer"/>
        public static PreAuthRequest PreAuth(double saleAmount, Customer customer)
        {
            return new PreAuthRequest { TRANSACTION_HEADER = new TransactionHeader { PWSALEAMOUNT = saleAmount }, CUSTOMER = customer };
        }

        /// <summary>
        /// Pre-authorize a card.
        /// </summary>
        /// <param name="saleAmount"/>
        /// <param name="payorState"/>
        /// <param name="cardNumber"/>
        /// <param name="expMm"/>
        /// <param name="expYy"/>
        /// <param name="cvv2"/>
        public static PreAuthRequest PreAuth(double saleAmount, string payorState, long cardNumber, string expMm, string expYy, string cvv2) // string payorFirstName, string payorLastName, string payorAddress1, string payorAddress2, string payorCity, string payorZip,
        {
            return new PreAuthRequest
            {
                TRANSACTION_HEADER = new TransactionHeader { PWSALEAMOUNT = saleAmount },
                CUSTOMER = new Customer
                {
                    STATE = payorState,
                    CARDNUMBER = cardNumber,
                    CVV2 = cvv2,
                    EXP_MM = expMm,
                    EXP_YY = expYy
                }
            };
        }


        /// <summary>
        /// Void a transaction. The transaction amount must match the amount of the original transaction, and the PWUNIQUEID must match the unique identifier associated with the transaction to void. The transaction must be in the current open batch to void it.
        /// </summary>
        /// <param name="header"/>
        public static VoidRequest Void(TransactionHeader header)
        {
            return new VoidRequest { TRANSACTION_HEADER = header };
        }
        /// <summary>
        /// Void a transaction. The transaction amount must match the amount of the original transaction, and the PWUNIQUEID must match the unique identifier associated with the transaction to void. The transaction must be in the current open batch to void it.
        /// </summary>
        /// <param name="saleAmount"/>
        /// <param name="invoiceNumber"/>
        /// <param name="uniqueId"/>
        public static VoidRequest Void(double saleAmount, string invoiceNumber, string uniqueId)
        {
            return new VoidRequest { TRANSACTION_HEADER = new TransactionHeader { PWSALEAMOUNT = saleAmount, PWINVOICENUMBER = invoiceNumber, PWUNIQUEID = uniqueId } };
        }

        /// <summary>
        /// Input the sale amount to get adjustment, tax, and total transaction amounts. Relevant for merchants configured with Cash Discount or Convenience Fees.
        /// </summary>
        /// <param name="header"/>
        /// <param name="customer"/>
        public static GetConsumerFeeRequest GetConsumerFee(TransactionHeader header, Customer customer)
        {
            return new GetConsumerFeeRequest()
            {
                TRANSACTION_HEADER = header,
                CUSTOMER = customer
            };
        }

        /// <summary>
        /// Input the sale amount to get adjustment, tax, and total transaction amounts. Relevant for merchants configured with Cash Discount or Convenience Fees.
        /// </summary>
        /// <param name="saleAmount"/>
        /// <param name="media"/>
        /// <param name="state"/>
        /// <param name="disableCf"/>
        /// <param name="token"/>
        public static GetConsumerFeeRequest GetConsumerFee(double saleAmount, string media, string state, string disableCf = "FALSE", string token = "")
        {
            return new GetConsumerFeeRequest
            {
                TRANSACTION_HEADER = new TransactionHeader { PWSALEAMOUNT = saleAmount, DISABLECF = disableCf },
                CUSTOMER = new Customer { PWMEDIA = media, STATE = state, PWTOKEN = token }
            };
        }

        /// <summary>
        /// Validate a card and return a token.
        /// </summary>
        /// <param name="header"/>
        /// <param name="customer"/>
        public static StoreTokenRequest StoreToken(TransactionHeader header, Customer customer)
        {
            return new StoreTokenRequest { TRANSACTION_HEADER = header, CUSTOMER = customer };
        }

        /// <summary>
        /// Validate a card and return a token.
        /// </summary>
        /// <param name="saleAmount"/>
        /// <param name="cardNumber"/>
        /// <param name="expMm"/>
        /// <param name="expYy"/>
        /// <param name="cvv2"/>
        /// <param name="companyName"/>
        /// <param name="firstName"/>
        /// <param name="lastName"/>
        /// <param name="email"/>
        /// <param name="address"/>
        /// <param name="address2"/>
        /// <param name="city"/>
        /// <param name="state"/>
        /// <param name="country"/>
        /// <param name="zip"/>
        /// <param name="primaryPhone"/>
        /// <param name="workPhone"/>
        /// <param name="pwCid"/>
        /// <param name="addCustomer"/>
        public static StoreTokenRequest StoreCreditCardToken(double saleAmount, long cardNumber, string expMm, string expYy, string cvv2, string companyName = "", string firstName = "", string lastName = "", string email = "", string address = "", string address2 = "", string city = "", string state = "", string country = "", string zip = "", string primaryPhone = "", string workPhone = "", string pwCid = "", string addCustomer = "FALSE")
        {
            return new StoreTokenRequest
            {
                TRANSACTION_HEADER = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount,
                },
                CUSTOMER = new Customer()
                {
                    PWMEDIA = "CC",
                    CARDNUMBER = cardNumber,
                    EXP_MM = expMm,
                    EXP_YY = expYy,
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

        /// <summary>
        /// Validate a card and return a token.
        /// </summary>
        /// <param name="saleAmount"></param>
        /// <param name="routingNumber"></param>
        /// <param name="accountNumber"></param>
        /// <param name="bankAccountType"></param>
        /// <param name="companyName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="address2"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <param name="zip"></param>
        /// <param name="primaryPhone"></param>
        /// <param name="workPhone"></param>
        /// <param name="pwCid"></param>
        /// <param name="addCustomer"></param>
        /// <returns></returns>
        public static StoreTokenRequest StoreCheckToken(double saleAmount, string routingNumber, string accountNumber, string bankAccountType, string companyName = "", string firstName = "", string lastName = "", string email = "", string address = "", string address2 = "", string city = "", string state = "", string country = "", string zip = "", string primaryPhone = "", string workPhone = "", string pwCid = "", string addCustomer = "FALSE")
        {
            return new StoreTokenRequest
            {
                TRANSACTION_HEADER = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount,
                },
                CUSTOMER = new Customer()
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
            customer.REQUESTTOKEN = "FALSE";
            return new TokenSaleRequest { TRANSACTION_HEADER = header, CUSTOMER = customer };
        }

        /// <summary>
        /// Process a payment using a token. Simply replace the Card or E-Check information with the PWTOKEN returned by the gateway when storing tokens.
        /// </summary>
        /// <param name="saleAmount"></param>
        /// <param name="token"></param>
        /// <param name="state"></param>
        /// <param name="disablecf"></param>
        /// <returns></returns>
        public static TokenSaleRequest TokenSale(double saleAmount, string token, string state, string disablecf = "FALSE")
        {
            return new TokenSaleRequest
            {
                TRANSACTION_HEADER = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount,
                    DISABLECF = disablecf
                },
                CUSTOMER = new Customer
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
                TRANSACTION_HEADER = new TransactionHeader(),
                CUSTOMER = customer
            };
        }

        /// <summary>
        /// Verification transaction will verify the customer's card or bank account before submitting the payment.
        /// </summary>
        /// <param name="saleAmount"></param>
        /// <param name="cardNumber"></param>
        /// <param name="expMm"></param>
        /// <param name="expYy"></param>
        /// <param name="cvv2"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="address2"></param>
        /// <param name="zip"></param>
        /// <param name="email"></param>
        /// <param name="primaryPhone"></param>
        /// <returns></returns>
        public static VerificationRequest Verification(double saleAmount, long cardNumber, string expMm, string expYy, string cvv2, string firstName = "", string lastName = "", string address = "", string address2 = "", string zip = "", string email = "", string primaryPhone = "")
        {
            return new VerificationRequest
            {
                TRANSACTION_HEADER = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount
                },
                CUSTOMER = new Customer
                {
                    PWMEDIA = "CC",
                    CARDNUMBER = cardNumber,
                    EXP_MM = expMm,
                    EXP_YY = expYy,
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
                TRANSACTION_HEADER = header,
                SEARCH_CONDITION = search
            };
        }

        /// <summary>
        /// Query the database for transaction results.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static SearchTransactionsRequest SearchTransactions(SearchCondition search)
        {
            return new SearchTransactionsRequest()
            {
                TRANSACTION_HEADER = new TransactionHeader { XOPTION = "TRUE" },
                SEARCH_CONDITION = search
            };
        }

        /// <summary>
        /// Get the current open batch summary.
        /// </summary>
        public static BatchInquiryRequest BatchInquiry(TransactionHeader header)
        {
            return new BatchInquiryRequest() { TRANSACTION_HEADER = header};
        }

        /// <summary>
        /// Charge a card or bank account (if applicable).
        /// </summary>
        /// <returns></returns>
        public static SaleRequest Sale(TransactionHeader header, Customer customer)
        {
            var request = new SaleRequest
            {
                TRANSACTION_HEADER = header,
                CUSTOMER = customer
            };

            return request;
        }

        /// <summary>
        /// Charge a card.
        /// </summary>
        /// <returns></returns>
        public static SaleRequest OneTimeCardPayment(double saleAmount, long cardNumber, string expMm, string expYy, string cvv2, string companyName = "", string firstName = "", string lastName = "", string email = "", string address = "", string address2 = "", string city = "", string state = "", string country = "", string zip = "", string primaryPhone = "", string workPhone = "")
        {
            // TODO: Check what field addCustomer goes into
            return new SaleRequest
            {
                TRANSACTION_HEADER = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount,
                },
                CUSTOMER = new Customer()
                {
                    PWMEDIA = "CC",
                    CARDNUMBER = cardNumber,
                    EXP_MM = expMm,
                    EXP_YY = expYy,
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
                TRANSACTION_HEADER = new TransactionHeader
                {
                    PWSALEAMOUNT = saleAmount,
                },
                CUSTOMER = new Customer()
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
                TRANSACTION_HEADER = header,
                CUSTOMER = customer
            };
        }

        /// <summary>
        /// Query the database for Chargeback transactions.
        /// </summary>
        public static SearchChargebackRequest SearchChargeback(TransactionHeader header, SearchCondition search)
        {
            return new SearchChargebackRequest()
            {
                TRANSACTION_HEADER = header,
                SEARCH_CONDITION = search
            };
        }

        /// <summary>
        /// Query the database for Chargeback transactions.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static SearchChargebackRequest SearchChargeback(SearchCondition search)
        {
            return new SearchChargebackRequest()
            {
                TRANSACTION_HEADER = new TransactionHeader { XOPTION = "TRUE" },
                SEARCH_CONDITION = search
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
                TRANSACTION_HEADER = header,
                CUSTOMER = customer
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
                TRANSACTION_HEADER = header
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
                TRANSACTION_HEADER = header,
                CUSTOMER = customer
            };
        }

        public static CaptureRequest Capture(double saleAmount, string invoiceNumber, string uniqueId)
        {
            return new CaptureRequest { TRANSACTION_HEADER = new TransactionHeader { PWSALEAMOUNT = saleAmount, PWINVOICENUMBER = invoiceNumber, PWUNIQUEID = uniqueId } };
        }

    }
}