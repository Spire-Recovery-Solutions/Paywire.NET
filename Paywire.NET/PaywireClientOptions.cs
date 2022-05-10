namespace Paywire.NET;

public class PaywireClientOptions
{
    public PaywireEndpoint Endpoint { get; set; }
    public string AuthenticationClientId { get; set; }
    public string AuthenticationKey { get; set; }
    public string AuthenticationUsername { get; set; }
    public string AuthenticationPassword { get; set; }
}

public enum PaywireEndpoint
{
    Staging, //Staging     https://dbstage1.paywire.com
    Production //Production    https://dbtranz.paywire.com
}