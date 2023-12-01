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

        public XmlSchema? GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && !reader.IsEmptyElement)
            {
                reader.ReadStartElement();
                string value = reader.ReadContentAsString();
                PaywireResult = ConvertStringToPaywireResult(value);
                reader.ReadEndElement();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(ConvertPaywireResultToString(PaywireResult));
        }

        public static PaywireResult ConvertStringToPaywireResult(string stringValue)
        {
            return stringValue switch
            {
                "APPROVAL" => PaywireResult.Approval,
                "APPROVED" => PaywireResult.Approval,
                "DECLINED" => PaywireResult.Declined,
                "CAPTURED" => PaywireResult.Captured,
                "CHARGEBACK" => PaywireResult.Chargeback,
                "ERROR" => PaywireResult.Error,
                "SUCCESS" => PaywireResult.Success,
                "UNSUCCESS" => PaywireResult.UnSuccess,
                "SETTLED" => PaywireResult.Settled,
                "VOIDED" => PaywireResult.Voided,
                "REJECTED" => PaywireResult.Rejected,
                "PENDING" => PaywireResult.Pending,
                "" => PaywireResult.Unknown,
                _ => throw new InvalidOperationException($"Unknown result code: {stringValue}")
            };
        }

        public static string ConvertPaywireResultToString(PaywireResult result)
        {
            switch (result)
            {
                case PaywireResult.Approval:
                    return "APPROVAL";
                case PaywireResult.Declined:
                    return "DECLINED";
                case PaywireResult.Captured:
                    return "CAPTURED";
                case PaywireResult.Chargeback:
                    return "CHARGEBACK";
                case PaywireResult.Error:
                    return "ERROR";
                case PaywireResult.Success:
                    return "SUCCESS";
                case PaywireResult.UnSuccess:
                    return "UNSUCCESS";
                case PaywireResult.Settled:
                    return "SETTLED";
                case PaywireResult.Voided:
                    return "VOIDED";
                case PaywireResult.Rejected:
                    return "REJECTED";
                case PaywireResult.Pending:
                    return "PENDING";
                case PaywireResult.Unknown:
                    return "";
                default:
                    throw new InvalidOperationException($"Unknown result code: {result}");
            }
        }

    }

}
