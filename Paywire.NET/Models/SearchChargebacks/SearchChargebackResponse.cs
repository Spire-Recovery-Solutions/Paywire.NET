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

    public class SearchChargebackResponse : BasePaywireResponse
    {
        [XmlElement("SEARCHRESULT")]
        public SearchResult SEARCH_RESULTS { get; set; } = new();
    }

    [XmlType("SearchChargebackResult")]
    public class SearchResult
    {
        [XmlElement("PWCBDETAIL")]
        public PaywirePaymentChargebackDetail[] PAYMENT_CHARGEBACK_DETAILS { get; set; } = [];
    }

    public class PaywirePaymentChargebackDetail
    {
        public string MID { get; set; } = string.Empty;
        public string EXTERNALMID { get; set; } = string.Empty;
        public string PWUID { get; set; } = string.Empty;
        public string CARDTYPE { get; set; } = string.Empty;
        public string NAME { get; set; } = string.Empty;
        public string RETURNDATE { get; set; } = string.Empty;
        public string RETURNAMOUNT { get; set; } = string.Empty;
        public string RESULT { get; set; } = string.Empty;
        public string RETURNCODE { get; set; } = string.Empty;
        public string RESPONSETEXT { get; set; } = string.Empty;
        public string CBCYCLE { get;set; } = string.Empty;
        public string CBCURRENCY { get; set; } = string.Empty;
        public string CBTRACENUM { get; set; } = string.Empty;
        public string TRAMOUNT { get; set; } = string.Empty;
        public string TRCURRENCY { get; set; } = string.Empty;
        public string TRDATETIME { get; set; } = string.Empty;
        public string PWINVOICENUBMER { get; set; } = string.Empty;
        public string CARDDATA { get; set; } = string.Empty;
        public string DESCRIPTOR { get; set; } = string.Empty;
    }
}
