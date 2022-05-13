using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.GetAuthToken;
using Paywire.NET.Models.Sale;
using Paywire.NET.Models.Verification;

namespace Paywire.NET.Factories
{
    public static class PaywireRequestFactory
    {

        public static void Credit()
        {

        }

        public static void PreAuth()
        {
        }

        public static void Void()
        {
        }


        public static void GetConsumerFee()
        {
        }
        public static void StoreToken()
        {


        }


        public static VerificationRequest Verification()
        {
            return new VerificationRequest
            {
                Customer = new Customer()
                {
                    REQUESTTOKEN = "FALSE",
                    PWMEDIA = "CC",
                    CARDNUMBER = 4761739001010267,
                    CVV2 = 999,
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

        
        public static void SearchTransactions()
        {


        }

        public static void BatchInquiry()
        {

        }



        public static SaleRequest Sale()
        {
            return new SaleRequest()
            {
                TransactionHeader = new TransactionHeader()
                {
                    PWSALEAMOUNT = 0.00,
                    DISABLECF = "TRUE"
                },
                Customer = new Customer()
                {
                    //4111 1111 1111 1111, cvv 123, exp 12/25
                    REQUESTTOKEN = "FALSE",
                    PWMEDIA = "CC",
                    CARDNUMBER = 4012301230123010,
                    CVV2 = 123,
                    EXP_YY = "22",
                    EXP_MM = "07",
                    FIRSTNAME = "CHRIS",
                    LASTNAME = "FROST",
                    PRIMARYPHONE = "7035551212",
                    EMAIL = "CFFROST@EMAILADDRESS.COM",
                    ADDRESS1 = "123",
                    CITY = "LOCKPORT",
                    STATE = "NY",
                    ZIP = "55555",
                }
            };
        }
    }
}
