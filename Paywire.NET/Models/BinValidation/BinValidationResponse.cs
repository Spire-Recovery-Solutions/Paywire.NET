using Paywire.NET.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Paywire.NET.Models.BinValidation
{
    [XmlRoot("PAYMENTRESPONSE")]
    public class BinValidationResponse : BasePaywireResponse
    {
        [XmlElement("BINVALDETAIL")]
        public Bin BinDetail { get; set; }
    }

    public class Bin
    {
        public string BIN { get; set; }
        public string BRAND { get; set; }
        public string CARDTYPE { get; set; }
        public string BANK { get; set; }
        public string COUNTRY { get; set; }
        public string ISFSA { get; set; }
    }
}
