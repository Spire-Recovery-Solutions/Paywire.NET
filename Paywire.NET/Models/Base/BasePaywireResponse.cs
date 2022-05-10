namespace Paywire.NET.Models.Base;

public class BasePaywireResponse
{
    /// <summary>
    /// Status for the request.	SUCCESS, ERROR
    /// </summary>
    public string RESULT { get; set; }
    /// <summary>
    /// Paywire-generated unique merchant identifier.	
    /// </summary>
    public string PWCLIENTID { get; set; }
    /// <summary>
    /// Identifier for this request.	
    /// </summary>
    public string PWINVOICENUMBER { get; set; }

    /// <summary>
    /// Contains the error message.
    /// </summary>
    public string RESTEXT { get; set; }
}