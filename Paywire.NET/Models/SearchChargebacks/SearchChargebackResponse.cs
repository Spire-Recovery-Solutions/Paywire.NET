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
    [XmlRoot("PAYMENTRESPONSE")]
    public class SearchChargebackResponse : BasePaywireResponse
    {
        [XmlElement("SEARCHRESULT")]
        public SearchResult SearchResults { get; set; }
    }

    public class SearchResult
    {
        [XmlElement("PWCBDETAIL")]
        public PaywirePaymentChargebackDetail[] PaymentChargebackDetails { get; set; }
    }

    public class PaywirePaymentChargebackDetail
    {
        public string MID { get; set; }
        public string EXTERNALMID { get; set; }
        public string PWUID { get; set; }                    
        public string CARDTYPE { get; set; }                 
        public string NAME { get; set; }                  
        public string RETURNDATE { get; set; }                 
        public string RETURNAMOUNT { get; set; }         
        public string RESULT { get; set; }                
        public string RETURNCODE { get; set; }                  
        public string RESPONSETEXT { get; set; }
        public string CBCYCLE { get;set; }
        public string CBCURRENCY { get; set; }
        public string CBTRACENUM { get; set; }
        public string TRAMOUNT { get; set; }
        public string TRCURRENCY { get; set; }
        public string TRDATETIME { get; set; }
        public string PWINVOICENUBMER { get; set; }
        public string CARDDATA { get; set; }
        public string DESCRIPTOR { get; set; }
    }
}
