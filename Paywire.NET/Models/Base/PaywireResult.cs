using System.Xml.Serialization;

namespace Paywire.NET.Models.Base;

public enum PaywireResult
{
    [XmlEnum(null)]
    Unknown,
    [XmlEnum("ERROR")]
    Error,
    [XmlEnum("SUCCESS")]
    Success,
    [XmlEnum("APPROVAL")]
    Approval,
    [XmlEnum("DECLINED")]
    Declined,
    [XmlEnum("CAPTURED")]
    Captured,
    [XmlEnum("CHARGEBACK")]
    Chargeback,
    [XmlEnum("SETTLED")]
    Settled,
    [XmlEnum("VOIDED")]
    Voided,
    [XmlEnum("REJECTED")]
    Rejected,
    [XmlEnum("PENDING")]
    Pending
}