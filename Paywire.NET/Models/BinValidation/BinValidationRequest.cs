using Paywire.NET.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Paywire.NET.Models.BinValidation
{
    public class BinValidationRequest : BasePaywireRequest
    {
        [XmlElement("CUSTOMER")]
        public Customer Customer { get; set; }
    }
}
