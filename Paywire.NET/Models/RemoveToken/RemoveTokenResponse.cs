using Paywire.NET.Models.Base;
using System.Xml.Serialization;

namespace Paywire.NET.Models.RemoveToken
{
 
    public class RemoveTokenResponse : BasePaywireResponse
    {
        public string PAYMETH { get; set; }
        public string AMOUNT { get; set; }
    }
}
