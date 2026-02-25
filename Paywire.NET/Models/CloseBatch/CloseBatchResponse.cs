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
        public string BATCHID { get; set; } = string.Empty;
        public string SALESTOTAL { get; set; } = string.Empty;
        public string SALESRECS { get; set; } = string.Empty;
        public string CREDITAMT { get; set; } = string.Empty;
        public string CREDITRECS { get; set; } = string.Empty;
        public string NETCRAMT { get; set; } = string.Empty;
        public string NETCRRECS { get; set; } = string.Empty;
        public string VOIDAMT { get; set; } = string.Empty;
        public string VOIDRECS { get; set; } = string.Empty;
    }
}
