using NUnit.Framework;
using Paywire.NET.Factories;
using Paywire.NET.Models.Base;
using Paywire.NET.Models.SearchTransactions;

namespace Paywire.NET.Tests.UnitTests;

[TestFixture]
public class PaywireRequestFactoryTests
{
    [Test]
    public void GetAuthToken_NoArgs_ReturnsRequestWithEmptyHeader()
    {
        var request = PaywireRequestFactory.GetAuthToken();
        Assert.That(request.TRANSACTION_HEADER, Is.Not.Null);
    }

    [Test]
    public void GetAuthToken_WithHeader_ReturnsRequestWithGivenHeader()
    {
        var header = new TransactionHeader { PWCLIENTID = "C1" };
        var request = PaywireRequestFactory.GetAuthToken(header);
        Assert.That(request.TRANSACTION_HEADER.PWCLIENTID, Is.EqualTo("C1"));
    }

    [Test]
    public void Credit_WithAmountInvoiceUniqueId_SetsHeaderFields()
    {
        var request = PaywireRequestFactory.Credit(50.0, "INV1", "UID1");
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(50.0));
        Assert.That(request.TRANSACTION_HEADER.PWINVOICENUMBER, Is.EqualTo("INV1"));
        Assert.That(request.TRANSACTION_HEADER.PWUNIQUEID, Is.EqualTo("UID1"));
    }

    [Test]
    public void Credit_WithHeader_ReturnsRequestWithGivenHeader()
    {
        var header = new TransactionHeader { PWSALEAMOUNT = 25.0 };
        var request = PaywireRequestFactory.Credit(header);
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(25.0));
    }

    [Test]
    public void Credit_WithHeaderAndCustomer_SetsBothFields()
    {
        var header = new TransactionHeader { PWSALEAMOUNT = 10.0 };
        var customer = new Customer { FIRSTNAME = "Bob" };
        var request = PaywireRequestFactory.Credit(header, customer);
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(10.0));
        Assert.That(request.CUSTOMER.FIRSTNAME, Is.EqualTo("Bob"));
    }

    [Test]
    public void PreAuth_WithHeaderAndCustomer_SetsBothFields()
    {
        var header = new TransactionHeader { PWSALEAMOUNT = 100.0 };
        var customer = new Customer { STATE = "CA" };
        var request = PaywireRequestFactory.PreAuth(header, customer);
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(100.0));
        Assert.That(request.CUSTOMER.STATE, Is.EqualTo("CA"));
    }

    [Test]
    public void PreAuth_WithAmountAndCustomer_SetsAmount()
    {
        var customer = new Customer { STATE = "NY" };
        var request = PaywireRequestFactory.PreAuth(75.0, customer);
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(75.0));
        Assert.That(request.CUSTOMER.STATE, Is.EqualTo("NY"));
    }

    [Test]
    public void PreAuth_WithCardDetails_SetsCustomerFields()
    {
        var request = PaywireRequestFactory.PreAuth(50.0, "TX", 4111111111111111, "12", "25", "123");
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(50.0));
        Assert.That(request.CUSTOMER.STATE, Is.EqualTo("TX"));
        Assert.That(request.CUSTOMER.CARDNUMBER, Is.EqualTo(4111111111111111));
        Assert.That(request.CUSTOMER.EXP_MM, Is.EqualTo("12"));
        Assert.That(request.CUSTOMER.EXP_YY, Is.EqualTo("25"));
        Assert.That(request.CUSTOMER.CVV2, Is.EqualTo("123"));
    }

    [Test]
    public void Void_WithHeader_ReturnsRequestWithGivenHeader()
    {
        var header = new TransactionHeader { PWUNIQUEID = "UID1" };
        var request = PaywireRequestFactory.Void(header);
        Assert.That(request.TRANSACTION_HEADER.PWUNIQUEID, Is.EqualTo("UID1"));
    }

    [Test]
    public void Void_WithAmountInvoiceUniqueId_SetsHeaderFields()
    {
        var request = PaywireRequestFactory.Void(30.0, "INV2", "UID2");
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(30.0));
        Assert.That(request.TRANSACTION_HEADER.PWINVOICENUMBER, Is.EqualTo("INV2"));
        Assert.That(request.TRANSACTION_HEADER.PWUNIQUEID, Is.EqualTo("UID2"));
    }

    [Test]
    public void GetConsumerFee_WithHeaderAndCustomer_SetsBothFields()
    {
        var header = new TransactionHeader { PWSALEAMOUNT = 200.0 };
        var customer = new Customer { STATE = "FL" };
        var request = PaywireRequestFactory.GetConsumerFee(header, customer);
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(200.0));
        Assert.That(request.CUSTOMER.STATE, Is.EqualTo("FL"));
    }

    [Test]
    public void GetConsumerFee_WithAmountMediaState_SetsFields()
    {
        var request = PaywireRequestFactory.GetConsumerFee(100.0, "CC", "CA");
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(100.0));
        Assert.That(request.TRANSACTION_HEADER.DISABLECF, Is.EqualTo("FALSE"));
        Assert.That(request.CUSTOMER.PWMEDIA, Is.EqualTo("CC"));
        Assert.That(request.CUSTOMER.STATE, Is.EqualTo("CA"));
    }

    [Test]
    public void StoreCreditCardToken_SetsMediaToCC()
    {
        var request = PaywireRequestFactory.StoreCreditCardToken(10.0, 4111111111111111, "01", "26", "999");
        Assert.That(request.CUSTOMER.PWMEDIA, Is.EqualTo("CC"));
        Assert.That(request.CUSTOMER.CARDNUMBER, Is.EqualTo(4111111111111111));
        Assert.That(request.CUSTOMER.EXP_MM, Is.EqualTo("01"));
        Assert.That(request.CUSTOMER.EXP_YY, Is.EqualTo("26"));
        Assert.That(request.CUSTOMER.CVV2, Is.EqualTo("999"));
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(10.0));
    }

    [Test]
    public void StoreCheckToken_SetsMediaToECHECK()
    {
        var request = PaywireRequestFactory.StoreCheckToken(20.0, "111000025", "123456789", "CHECKING");
        Assert.That(request.CUSTOMER.PWMEDIA, Is.EqualTo("ECHECK"));
        Assert.That(request.CUSTOMER.ROUTINGNUMBER, Is.EqualTo("111000025"));
        Assert.That(request.CUSTOMER.ACCOUNTNUMBER, Is.EqualTo("123456789"));
        Assert.That(request.CUSTOMER.BANKACCTTYPE, Is.EqualTo("CHECKING"));
    }

    [Test]
    public void TokenSale_WithHeaderAndCustomer_SetsRequestTokenFalse()
    {
        var header = new TransactionHeader { PWSALEAMOUNT = 50.0 };
        var customer = new Customer { PWTOKEN = "TK1" };
        var request = PaywireRequestFactory.TokenSale(header, customer);
        Assert.That(request.CUSTOMER.REQUESTTOKEN, Is.EqualTo("FALSE"));
        Assert.That(request.CUSTOMER.PWTOKEN, Is.EqualTo("TK1"));
    }

    [Test]
    public void TokenSale_WithAmountTokenState_SetsFields()
    {
        var request = PaywireRequestFactory.TokenSale(99.0, "TK2", "NY");
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(99.0));
        Assert.That(request.CUSTOMER.PWTOKEN, Is.EqualTo("TK2"));
        Assert.That(request.CUSTOMER.STATE, Is.EqualTo("NY"));
    }

    [Test]
    public void Verification_WithCustomer_ReturnsRequest()
    {
        var customer = new Customer { CARDNUMBER = 4111111111111111 };
        var request = PaywireRequestFactory.Verification(customer);
        Assert.That(request.TRANSACTION_HEADER, Is.Not.Null);
        Assert.That(request.CUSTOMER.CARDNUMBER, Is.EqualTo(4111111111111111));
    }

    [Test]
    public void Verification_WithCardDetails_SetsFields()
    {
        var request = PaywireRequestFactory.Verification(0.0, 4111111111111111, "12", "25", "123");
        Assert.That(request.CUSTOMER.PWMEDIA, Is.EqualTo("CC"));
        Assert.That(request.CUSTOMER.CARDNUMBER, Is.EqualTo(4111111111111111));
    }

    [Test]
    public void SearchTransactions_WithSearchCondition_SetsXOptionTrue()
    {
        var search = new SearchCondition { COND_UNIQUEID = "UID1" };
        var request = PaywireRequestFactory.SearchTransactions(search);
        Assert.That(request.TRANSACTION_HEADER.XOPTION, Is.EqualTo("TRUE"));
        Assert.That(request.SEARCH_CONDITION.COND_UNIQUEID, Is.EqualTo("UID1"));
    }

    [Test]
    public void SearchTransactions_WithHeaderAndSearch_UsesProvidedHeader()
    {
        var header = new TransactionHeader { XOPTION = "FALSE" };
        var search = new SearchCondition();
        var request = PaywireRequestFactory.SearchTransactions(header, search);
        Assert.That(request.TRANSACTION_HEADER.XOPTION, Is.EqualTo("FALSE"));
    }

    [Test]
    public void OneTimeCardPayment_SetsMediaToCC()
    {
        var request = PaywireRequestFactory.OneTimeCardPayment(50.0, 4111111111111111, "12", "25", "123");
        Assert.That(request.CUSTOMER.PWMEDIA, Is.EqualTo("CC"));
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(50.0));
    }

    [Test]
    public void OneTimeCheckPayment_SetsMediaToECHECK()
    {
        var request = PaywireRequestFactory.OneTimeCheckPayment(75.0, "111000025", "123456789", "CHECKING");
        Assert.That(request.CUSTOMER.PWMEDIA, Is.EqualTo("ECHECK"));
        Assert.That(request.CUSTOMER.ROUTINGNUMBER, Is.EqualTo("111000025"));
    }

    [Test]
    public void Sale_WithHeaderAndCustomer_SetsBothFields()
    {
        var header = new TransactionHeader { PWSALEAMOUNT = 150.0 };
        var customer = new Customer { FIRSTNAME = "Alice" };
        var request = PaywireRequestFactory.Sale(header, customer);
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(150.0));
        Assert.That(request.CUSTOMER.FIRSTNAME, Is.EqualTo("Alice"));
    }

    [Test]
    public void Capture_SetsHeaderFields()
    {
        var request = PaywireRequestFactory.Capture(100.0, "INV5", "UID5");
        Assert.That(request.TRANSACTION_HEADER.PWSALEAMOUNT, Is.EqualTo(100.0));
        Assert.That(request.TRANSACTION_HEADER.PWINVOICENUMBER, Is.EqualTo("INV5"));
        Assert.That(request.TRANSACTION_HEADER.PWUNIQUEID, Is.EqualTo("UID5"));
    }

    [Test]
    public void BatchInquiry_WithHeader_ReturnsRequest()
    {
        var header = new TransactionHeader { PWCLIENTID = "C1" };
        var request = PaywireRequestFactory.BatchInquiry(header);
        Assert.That(request.TRANSACTION_HEADER.PWCLIENTID, Is.EqualTo("C1"));
    }

    [Test]
    public void CloseBatch_WithHeader_ReturnsRequest()
    {
        var header = new TransactionHeader { PWCLIENTID = "C1" };
        var request = PaywireRequestFactory.CloseBatch(header);
        Assert.That(request.TRANSACTION_HEADER.PWCLIENTID, Is.EqualTo("C1"));
    }

    [Test]
    public void RemoveToken_WithHeaderAndCustomer_SetsBothFields()
    {
        var header = new TransactionHeader();
        var customer = new Customer { PWTOKEN = "TK1" };
        var request = PaywireRequestFactory.RemoveToken(header, customer);
        Assert.That(request.CUSTOMER.PWTOKEN, Is.EqualTo("TK1"));
    }

    [Test]
    public void SendReceipt_WithHeaderAndCustomer_SetsBothFields()
    {
        var header = new TransactionHeader { PWUNIQUEID = "UID1" };
        var customer = new Customer { EMAIL = "test@example.com" };
        var request = PaywireRequestFactory.SendReceipt(header, customer);
        Assert.That(request.TRANSACTION_HEADER.PWUNIQUEID, Is.EqualTo("UID1"));
        Assert.That(request.CUSTOMER.EMAIL, Is.EqualTo("test@example.com"));
    }

    [Test]
    public void SearchChargeback_WithSearchCondition_SetsXOptionTrue()
    {
        var search = new SearchCondition { COND_CBTYPE = "RETRIEVAL" };
        var request = PaywireRequestFactory.SearchChargeback(search);
        Assert.That(request.TRANSACTION_HEADER.XOPTION, Is.EqualTo("TRUE"));
        Assert.That(request.SEARCH_CONDITION.COND_CBTYPE, Is.EqualTo("RETRIEVAL"));
    }

    [Test]
    public void SearchChargeback_WithHeaderAndSearch_UsesProvidedHeader()
    {
        var header = new TransactionHeader { XOPTION = "FALSE" };
        var search = new SearchCondition();
        var request = PaywireRequestFactory.SearchChargeback(header, search);
        Assert.That(request.TRANSACTION_HEADER.XOPTION, Is.EqualTo("FALSE"));
    }

    [Test]
    public void BinValidation_WithHeaderAndCustomer_SetsBothFields()
    {
        var header = new TransactionHeader();
        var customer = new Customer { BINNUMBER = "411111" };
        var request = PaywireRequestFactory.BinValidation(header, customer);
        Assert.That(request.CUSTOMER.BINNUMBER, Is.EqualTo("411111"));
    }

    [Test]
    public void StoreToken_WithHeaderAndCustomer_SetsBothFields()
    {
        var header = new TransactionHeader { PWSALEAMOUNT = 0.0 };
        var customer = new Customer { CARDNUMBER = 4111111111111111 };
        var request = PaywireRequestFactory.StoreToken(header, customer);
        Assert.That(request.CUSTOMER.CARDNUMBER, Is.EqualTo(4111111111111111));
    }
}
