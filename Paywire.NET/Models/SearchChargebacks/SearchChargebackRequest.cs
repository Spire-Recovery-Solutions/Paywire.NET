using Paywire.NET.Models.Base;
using Paywire.NET.Models.SearchTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Paywire.NET.Models.SearchChargebacks
{
    [XmlRoot("PAYMENTREQUEST")]
    public class SearchChargebackRequest : BasePaywireRequest
    {
        [XmlElement("SEARCHCONDITION")]
        public SearchCondition SearchCondition { get; set; }
    }
}
