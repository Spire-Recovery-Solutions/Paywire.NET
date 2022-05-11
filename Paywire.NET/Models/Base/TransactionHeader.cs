using System.Text;
using System.Xml.Serialization;

namespace Paywire.NET.Models.Base;

public class TransactionHeader
{
    [XmlElement("PWVERSION")]
    public int PaywireVersion { get; set; } = 3;

    [XmlElement("PWCLIENTID")]
    public string ClientId { get; set; }
    public string PWKEY { get; set; }
    public string PWUSER { get; set; }
    public string PWPASS { get; set; }
    public string PWTRANSACTIONTYPE { get; set; }
    public string PWINVOICENUMBER { get; set; }
    public double PWSALEAMOUNT { get; set; }
}