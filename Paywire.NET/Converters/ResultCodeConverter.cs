using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Converters
{
    public class ResultCodeConverter : IXmlSerializable
    {
        public PaywireResult PaywireResult { get; set; }

        private static readonly Dictionary<string, PaywireResult> StringToResultMapping = new()
        {
            {"APPROVAL", PaywireResult.Approval},
            {"APPROVED", PaywireResult.Approval},
            {"DECLINED", PaywireResult.Declined},
            {"CAPTURED", PaywireResult.Captured},
            {"CHARGEBACK", PaywireResult.Chargeback},
            {"ERROR", PaywireResult.Error},
            {"SUCCESS", PaywireResult.Success},
            {"UNSUCCESS", PaywireResult.UnSuccess},
            {"SETTLED", PaywireResult.Settled},
            {"VOIDED", PaywireResult.Voided},
            {"REJECTED", PaywireResult.Rejected},
            {"PENDING", PaywireResult.Pending}
        };

        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && !reader.IsEmptyElement)
            {
                reader.ReadStartElement();
                var value = reader.ReadContentAsString();
                if (StringToResultMapping.TryGetValue(value, out var result))
                {
                    PaywireResult = result;
                }
                else
                {
                    throw new InvalidOperationException($"Unknown result code: {value}");
                }
                reader.ReadEndElement();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            var resultString = StringToResultMapping.FirstOrDefault(kv => kv.Value == PaywireResult).Key;
            if (resultString != null)
            {
                writer.WriteString(resultString);
            }
            else
            {
                throw new InvalidOperationException($"Unknown result code: {PaywireResult}");
            }
        }
    }

}
