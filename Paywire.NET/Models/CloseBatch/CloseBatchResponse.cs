using Paywire.NET.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Paywire.NET.Models.CloseBatch
{
    
    public class CloseBatchResponse : BasePaywireResponse
    {
        public string BATCHID { get; set; }
        public string SALESTOTAL { get; set; }
        public string SALESRECS { get; set; }
        public string CREDITAMT { get; set; }
        public string CREDITRECS { get; set; }
        public string NETCRAMT { get; set; }
        public string NETCRRECS { get; set; }
        public string VOIDAMT { get; set; }
        public string VOIDRECS { get; set;}
    }
}
