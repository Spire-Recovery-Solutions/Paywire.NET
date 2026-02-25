using Paywire.NET.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Paywire.NET.Models.BinValidation
{

    public class BinValidationResponse : BasePaywireResponse
    {
        [XmlElement("BINVALDETAIL")]
        public Bin BIN_DETAIL { get; set; } = new();
    }

    public class Bin
    {
        public string BIN { get; set; } = string.Empty;
        public string BRAND { get; set; } = string.Empty;
        public string CARDTYPE { get; set; } = string.Empty;
        public string BANK { get; set; } = string.Empty;
        public string COUNTRY { get; set; } = string.Empty;
        public string ISFSA { get; set; } = string.Empty;
        /// <summary>
        /// Card sub-type (0-50 chars).
        /// </summary>
        public string SUBTYPE { get; set; } = string.Empty;
        /// <summary>
        /// TRUE/FALSE - prepaid card indicator.
        /// </summary>
        public string ISPREPAID { get; set; } = string.Empty;
    }
}
