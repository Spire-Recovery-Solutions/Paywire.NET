namespace Paywire.NET.Models.Base;

public class Customer
{
    /// <summary>
    /// Customer's company name.
    /// </summary>
    public string COMPANYNAME { get; set; }

    public string FIRSTNAME { get; set; }
    public string LASTNAME { get; set; }
    public string EMAIL { get; set; }
    public string ADDRESS1 { get; set; }
    public string CITY { get; set; }
    public string STATE { get; set; }
    public string ZIP { get; set; }
    public string COUNTRY { get; set; }
    public string PRIMARYPHONE { get; set; }
    public string WORKPHONE { get; set; }
    public string PWMEDIA { get; set; }
    public long CARDNUMBER { get; set; }
    public string EXP_MM { get; set; }
    public string EXP_YY { get; set; }
    public int CVV2 { get; set; }
    public string BANKACCTTYPE { get; set; }
    public string ROUTINGNUMBER { get; set; }
    public string ACCOUNTNUMBER { get; set; }
    public string REQUESTTOKEN { get; set; }
    public string SECCODE { get; set; }
    public string ADDRESS2 { get; set; }
    public double ADJTAXRATE { get; set; }
    public string DESCRIPTION { get; set; }
    public string EXTCID { get; set; }
    public string PWCID { get; set; }
    public string PWTOKEN { get; set; }
    public string PWCTRANSTYPE { get; set; }
    public string PWUNIQUEID { get; set; }
    public string SECURECODE { get; set; }
    public string POSINDICATOR { get; set; }
    public string TOTALINSTALLMENTS { get; set; }
    public string BR_DOCUMENT { get; set; }
    public string FORCESAVETOKEN { get; set; }


}