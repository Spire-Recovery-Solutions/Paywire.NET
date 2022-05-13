namespace Paywire.NET.Models.Base;

public class Customer
{
    /// <summary>
    /// Customer's company name.
    /// </summary>
    public string COMPANYNAME { get; set; }
    /// <summary>
    /// Account Holder's first name (required for RCC).	
    /// </summary>
    public string FIRSTNAME { get; set; }
    /// <summary>
    /// Account Holder's last name (required for RCC).	
    /// </summary>
    public string LASTNAME { get; set; }
    /// <summary>
    /// Account Holder's primary address (required for RCC).	
    /// </summary>
    public string ADDRESS1 { get; set; }

    /// <summary>
    /// Account Holder's secondary address (required for RCC).	
    /// </summary>
    public string ADDRESS2 {  get; set; }
    /// <summary>
    /// Account Holder's city of residence (required for RCC).	
    /// </summary>
    public string CITY { get; set; }

    /// <summary>
    /// Account Holder's state of residence. Required if configured with Convenience Fees (required for RCC).	
    /// </summary>
    public string STATE { get; set; }
    /// <summary>
    /// Account Holder's country of residence (required for RCC).	
    /// </summary>
    public string COUNTRY { get; set; }
    /// <summary>
    /// Account Holder's address postal/zip code (required for RCC) See important note on Zip Codes.	
    /// </summary>
    public string ZIP { get; set; }
    /// <summary>
    /// Account Holder's email address.	
    /// </summary>
    public string EMAIL { get; set; }
    /// <summary>
    /// Account Holder's primary phone number.	
    /// </summary>
    public string PRIMARYPHONE { get; set; }


    public string WORKPHONE { get; set; }
    /// <summary>
    /// Defines the payment method.	Fixed options: CC and ECHECK.
    /// </summary>
    public string PWMEDIA { get; set; }
    /// <summary>
    /// Card number to process payment with. Required only when CC is submitted in PWMEDIA.	
    /// </summary>
    public long CARDNUMBER { get; set; }
    /// <summary>
    /// Card expiry month. Required only when CC is submitted in PWMEDIA.	
    /// </summary>
    public string EXP_MM { get; set; }
    /// <summary>
    /// Card expiry year. Required only when CC is submitted in PWMEDIA.	
    /// </summary>
    public string EXP_YY { get; set; }
    /// <summary>
    /// Card Verification Value. Required only when CC is submitted in PWMEDIA.	
    /// </summary>
    public int CVV2 { get; set; }
    /// <summary>
    /// Type of Bank Account to process payment with. Required only when ECHECK is submitted in PWMEDIA.	CHECKING, SAVINGS
    /// </summary>
    public string BANKACCTTYPE { get; set; }
    /// <summary>
    /// Routing number of Bank Account to process payment with. Required only when ECHECK is submitted in PWMEDIA.	
    /// </summary>
    public string ROUTINGNUMBER { get; set; }
    /// <summary>
    /// Account number of Bank Account to process payment with. Required only when ECHECK is submitted in PWMEDIA.	
    /// </summary>
    public string ACCOUNTNUMBER { get; set; }
    /// <summary>
    /// Creates a token and returns a PWTOKEN in the response when set to TRUE. By default, when not submitted, a PWTOKEN is returned when CC is submitted in PWMEDIA but not for ECHECK.	
    /// </summary>
    public string REQUESTTOKEN { get; set; }
    /// <summary>
    /// SEC Code for ECHECK payments.	
    /// </summary>
    public string SECCODE { get; set; }
    /// <summary>
    /// Overrides the configured Sales Tax rate.	
    /// </summary>
    public double ADJTAXRATE { get; set; }
    /// <summary>
    /// Transaction custom description message.	
    /// </summary>
    public string DESCRIPTION { get; set; }
    /// <summary>
    /// External Customer ID.	
    /// </summary>
    public string EXTCID { get; set; }
    /// <summary>
    /// Paywire Customer Identifier. When submitted, the created token will be associated with this customer.	
    /// </summary>
    public string PWCID { get; set; }
    /// <summary>
    /// Unique token representing a customer's card or account details stored on the Paywire Gateway.	
    /// </summary>
    public string PWTOKEN { get; set; }
    /// <summary>
    /// The unique ID assigned by Paywire associated with this transaction.
    /// </summary>
    public string PWUNIQUEID { get; set; }
    /// <summary>
    /// The SMS code returned in the Initialize response.	
    /// </summary>
    public string SECURECODE { get; set; }
    
    public string POSINDICATOR { get; set; }

    /// <summary>
    /// The number of installments when the payment is initiated by the merchant.	
    /// </summary>
    public string TOTALINSTALLMENTS { get; set; }
    /// <summary>
    /// Document number. For Brazilians, the expected document will be C.P.F. or CNPJ - Data format can be 023.472.201-01 or 02347220101.	
    /// </summary>
    public string BR_DOCUMENT { get; set; }
    /// <summary>
    /// Available for PWCTRANSTYPE = 2 or 4, default is TRUE, Set to FALSE to allow the customer to decide to save the token or not.	
    /// </summary>
    public string FORCESAVETOKEN { get; set; }


}