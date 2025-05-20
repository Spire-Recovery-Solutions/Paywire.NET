namespace Paywire.NET;

public class PaywireClientOptions
{
    public PaywireEndpoint ENDPOINT { get; set; }
    public string AUTHENTICATION_CLIENT_ID { get; set; }
    public string AUTHENTICATION_KEY { get; set; }
    public string AUTHENTICATION_USERNAME { get; set; }
    public string AUTHENTICATION_PASSWORD { get; set; }
}

public enum PaywireEndpoint
{
    Staging, //Staging     https://dbstage1.paywire.com
    Production //Production    https://dbtranz.paywire.com
}