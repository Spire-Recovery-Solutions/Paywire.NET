using System;
using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.PreAuth;
using Paywire.NET.Models.Sale;

namespace Paywire.NET.Tests.UnitTests;

[TestFixture]
public class XmlSerializationTests
{
    private static string Serialize<T>(T obj)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var writer = new StringWriter();
        serializer.Serialize(writer, obj);
        return writer.ToString();
    }

    private static T Deserialize<T>(string xml)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var reader = new StringReader(xml);
        return (T)serializer.Deserialize(reader)!;
    }

    [Test]
    public void SaleRequest_SerializesToXml_WithExpectedElements()
    {
        var request = new SaleRequest
        {
            TRANSACTION_HEADER = new TransactionHeader
            {
                PWCLIENTID = "TestClient",
                PWVERSION = 3
            },
            CUSTOMER = new Customer { FIRSTNAME = "John" }
        };

        var xml = Serialize(request);

        // Derived types serialize with their own class name as root;
        // PaywireClient uses XmlRootAttribute override to serialize as PAYMENTREQUEST at send time
        Assert.That(xml, Does.Contain("<SaleRequest"));
        Assert.That(xml, Does.Contain("<TRANSACTIONHEADER>"));
        Assert.That(xml, Does.Contain("<PWCLIENTID>TestClient</PWCLIENTID>"));
        Assert.That(xml, Does.Contain("<CUSTOMER>"));
        Assert.That(xml, Does.Contain("<FIRSTNAME>John</FIRSTNAME>"));
    }

    [Test]
    public void BasePaywireResponse_DeserializesFromXml()
    {
        const string xml = """
            <?xml version="1.0"?>
            <PAYMENTRESPONSE>
                <RESULT>APPROVAL</RESULT>
                <RESTEXT>Transaction approved</RESTEXT>
                <PWCLIENTID>C123</PWCLIENTID>
                <PWINVOICENUMBER>INV001</PWINVOICENUMBER>
            </PAYMENTRESPONSE>
            """;

        var response = Deserialize<BasePaywireResponse>(xml);

        Assert.That(response.RAW_RESULT, Is.EqualTo("APPROVAL"));
        Assert.That(response.RESULT, Is.EqualTo(PaywireResult.Approval));
        Assert.That(response.RESTEXT, Is.EqualTo("Transaction approved"));
        Assert.That(response.PWCLIENTID, Is.EqualTo("C123"));
        Assert.That(response.PWINVOICENUMBER, Is.EqualTo("INV001"));
    }

    [Test]
    public void Customer_AdjTaxRate_NullableOmittedWhenNull()
    {
        var customer = new Customer { FIRSTNAME = "Test" };

        var xml = Serialize(customer);

        // ADJTAXRATE is double? — when null (default), it should not serialize a value
        Assert.That(xml, Does.Not.Contain("<ADJTAXRATE>0</ADJTAXRATE>"));
    }

    [Test]
    public void Customer_AdjTaxRate_SerializedWhenSet()
    {
        var customer = new Customer { FIRSTNAME = "Test", ADJTAXRATE = 7.5 };

        var xml = Serialize(customer);

        Assert.That(xml, Does.Contain("<ADJTAXRATE>7.5</ADJTAXRATE>"));
    }

    [Test]
    public void TransactionHeader_Roundtrip()
    {
        var header = new TransactionHeader
        {
            PWCLIENTID = "CLIENT1",
            PWKEY = "KEY1",
            PWUSER = "USER1",
            PWPASS = "PASS1",
            PWVERSION = 3,
            PWTRANSACTIONTYPE = "SALE",
            PWSALEAMOUNT = 25.50,
            PWINVOICENUMBER = "INV123",
            XOPTION = "TRUE"
        };

        var xml = Serialize(header);
        var deserialized = Deserialize<TransactionHeader>(xml);

        Assert.That(deserialized.PWCLIENTID, Is.EqualTo("CLIENT1"));
        Assert.That(deserialized.PWKEY, Is.EqualTo("KEY1"));
        Assert.That(deserialized.PWUSER, Is.EqualTo("USER1"));
        Assert.That(deserialized.PWPASS, Is.EqualTo("PASS1"));
        Assert.That(deserialized.PWVERSION, Is.EqualTo(3));
        Assert.That(deserialized.PWTRANSACTIONTYPE, Is.EqualTo("SALE"));
        Assert.That(deserialized.PWSALEAMOUNT, Is.EqualTo(25.50));
        Assert.That(deserialized.PWINVOICENUMBER, Is.EqualTo("INV123"));
        Assert.That(deserialized.XOPTION, Is.EqualTo("TRUE"));
    }

    [Test]
    public void BasePaywireRequest_HasCorrectXmlRootAttribute()
    {
        var attr = (XmlRootAttribute?)Attribute.GetCustomAttribute(
            typeof(BasePaywireRequest), typeof(XmlRootAttribute));

        Assert.That(attr, Is.Not.Null);
        Assert.That(attr!.ElementName, Is.EqualTo("PAYMENTREQUEST"));
    }

    [Test]
    public void BasePaywireResponse_HasCorrectXmlRootAttribute()
    {
        var attr = (XmlRootAttribute?)Attribute.GetCustomAttribute(
            typeof(BasePaywireResponse), typeof(XmlRootAttribute));

        Assert.That(attr, Is.Not.Null);
        Assert.That(attr!.ElementName, Is.EqualTo("PAYMENTRESPONSE"));
    }

    [Test]
    public void SaleRequest_Roundtrip()
    {
        var request = new SaleRequest
        {
            TRANSACTION_HEADER = new TransactionHeader
            {
                PWCLIENTID = "C1",
                PWSALEAMOUNT = 100.00,
                PWVERSION = 3
            },
            CUSTOMER = new Customer
            {
                PWMEDIA = "CC",
                CARDNUMBER = 4111111111111111,
                EXP_MM = "12",
                EXP_YY = "25",
                CVV2 = "123",
                FIRSTNAME = "Jane",
                LASTNAME = "Doe",
                STATE = "TX"
            }
        };

        var xml = Serialize(request);
        var deserialized = Deserialize<SaleRequest>(xml);

        Assert.That(deserialized.TRANSACTION_HEADER.PWCLIENTID, Is.EqualTo("C1"));
        Assert.That(deserialized.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(100.00));
        Assert.That(deserialized.CUSTOMER.CARDNUMBER, Is.EqualTo(4111111111111111));
        Assert.That(deserialized.CUSTOMER.FIRSTNAME, Is.EqualTo("Jane"));
        Assert.That(deserialized.CUSTOMER.STATE, Is.EqualTo("TX"));
    }

    [Test]
    public void SaleRequest_WithDigitalWallet_SerializesCorrectly()
    {
        var request = new SaleRequest
        {
            TRANSACTION_HEADER = new TransactionHeader { PWCLIENTID = "C1" },
            CUSTOMER = new Customer { PWMEDIA = "CC" },
            DIGITAL_WALLET = new DigitalWallet { DWTYPE = "A", DWPAYLOAD = "TEST_PAYLOAD" }
        };

        var xml = Serialize(request);

        Assert.That(xml, Does.Contain("<DIGITALWALLET>"));
        Assert.That(xml, Does.Contain("<DWTYPE>A</DWTYPE>"));
        Assert.That(xml, Does.Contain("<DWPAYLOAD>TEST_PAYLOAD</DWPAYLOAD>"));
        // DIGITALWALLET should be a sibling of TRANSACTIONHEADER and CUSTOMER
        Assert.That(xml, Does.Contain("<TRANSACTIONHEADER>"));
        Assert.That(xml, Does.Contain("<CUSTOMER>"));
    }

    [Test]
    public void SaleRequest_WithoutDigitalWallet_OmitsElement()
    {
        var request = new SaleRequest
        {
            TRANSACTION_HEADER = new TransactionHeader { PWCLIENTID = "C1" },
            CUSTOMER = new Customer { PWMEDIA = "CC" }
        };

        var xml = Serialize(request);

        Assert.That(xml, Does.Not.Contain("<DIGITALWALLET>"));
    }

    [Test]
    public void DigitalWallet_DecryptedPath_SerializesAllFields()
    {
        var wallet = new DigitalWallet
        {
            DWTYPE = "G",
            ISTDES = "TRUE",
            CAVV = "CAVV_DATA",
            ECI = "05",
            UCAF = "UCAF_IND"
        };

        var xml = Serialize(wallet);

        Assert.That(xml, Does.Contain("<DWTYPE>G</DWTYPE>"));
        Assert.That(xml, Does.Contain("<ISTDES>TRUE</ISTDES>"));
        Assert.That(xml, Does.Contain("<CAVV>CAVV_DATA</CAVV>"));
        Assert.That(xml, Does.Contain("<ECI>05</ECI>"));
        Assert.That(xml, Does.Contain("<UCAF>UCAF_IND</UCAF>"));
    }
}
