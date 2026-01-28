Title: Introduction – Paywire Developer Documentation

URL Source: https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html

Published Time: Thu, 15 Jan 2026 21:10:01 GMT

Markdown Content:
The Paywire Gateway implements different payment gateways, and offers a simple integration with an increasing number of features.

Four integration options are available:

1.   Application Programming Interface (API)
2.   Off Site Buy Page (OSBP)
3.   Checkout Page (via OSBP)
4.   OCX Control

The _API_ option allows the developer to use the Paywire Gateway features within their own application. This option is the most flexible, but requires a more complex implementation. More information on the API is detailed in the [API Reference](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-reference).

The _OSBP_ is the quickest integration option, favored by most merchants and developers. For more information regarding OSBP, please refer to the [OSBP Reference](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#osbp-reference) section.

The _OCX_ provides an easy-to-install component that can connect readers with the Paywire API or simplify integration with a built-in form that captures payment information. More information on the OCX is detailed in the [OCX Reference](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#ocx-reference).

URLs
----

The API and OSBP only accept HTTP POST.

Staging `https://dbstage1.paywire.com`

Production `https://dbtranz.paywire.com`

Authentication
--------------

> API/OCX Example:

```
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      ...
   </TRANSACTIONHEADER>
  ...
</PAYMENTREQUEST>
```

> Make sure to replace `{clientId}`, `{key}`, `{username}` and `{password}` with the relevant credentials provided to you by the administrator.

To authenticate with the Paywire API or OCX, simply include your 4 credentials in the XML payload.

If you do not have these credentials you may request them by emailing [support@payscout.com](mailto:support@payscout.com).

> OSBP Example:

```
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <AUTHTOKEN>4C2F8EE94CA2491AAB67EA6541CB17BA</AUTHTOKEN>
      ...
   </TRANSACTIONHEADER>
  ...
</PAYMENTREQUEST>
```

To authenticate with the OSBP, first request a [Get Auth Token](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-get-auth-token) to retrieve an `AUTHTOKEN`, then submit it as a request parameter instead of `PWUSER` and `PWPASS`.

Common Structure
----------------

XML Requests
------------

> Common Request XML Elements:

```
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID></PWCLIENTID>
      <PWKEY></PWKEY>
      <PWUSER></PWUSER>
      <PWPASS></PWPASS>
      <PWTRANSACTIONTYPE></PWTRANSACTIONTYPE>
   </TRANSACTIONHEADER>
   <CUSTOMER/>
</PAYMENTREQUEST>
```

The API, OSBP and OCX ([using form](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#ocx-using-form)) all have a similar XML request structure.

`PAYMENTREQUEST` is the parent element to a `TRANSACTIONHEADER` block, and a `CUSTOMER` block when processing a payment.

`TRANSACTIONHEADER` needs to always specify the [Authentication](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#authentication), `PWTRANSACTIONTYPE` and `PWVERSION` parameters as children.

XML Responses
-------------

> OSBP Approved Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
   <PWINVOICENUMBER>0987654321234567889</PWINVOICENUMBER>
   <RESULT>APPROVAL</RESULT>
   <PWCLIENTID>0000000001</PWCLIENTID>
   <AUTHCODE>TAS709</AUTHCODE>
   <AVSCODE>N</AVSCODE>
   <CVVCODE>M</CVVCODE>
   <PAYMETH>C</PAYMETH>
   <PWUNIQUEID>596</PWUNIQUEID>
</PAYMENTRESPONSE>
```

> API Approved Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <BATCHID>1</BATCHID>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <PAYMETH>A</PAYMETH>
    <PWUNIQUEID>112302</PWUNIQUEID>
    <AHNAME>John Doe</AHNAME>
    <AMOUNT>10.00</AMOUNT>
    <MACCOUNT>XXXXXX4082</MACCOUNT>
    <EMAIL>jd@example.com</EMAIL>
    <CCTYPE>ACH</CCTYPE>
    <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

> OSBP Declined Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
   <PWINVOICENUMBER>0987654321234567889</PWINVOICENUMBER>
   <RESULT>DECLINED</RESULT>
   <RESTEXT>CVV2 MISMATCH</RESTEXT>
   <PWCLIENTID>0000000001</PWCLIENTID>
   <CVVCODE>N</CVVCODE>
   <PAYMETH>C</PAYMETH>
   <PWUNIQUEID>597</PWUNIQUEID>
</PAYMENTRESPONSE>
```

> API Declined Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>DECLINED</RESULT>
    <RESTEXT>  ERROR  0295   </RESTEXT>
    <BATCHID>1</BATCHID>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <AVSCODE>0</AVSCODE>
    <PAYMETH>C</PAYMETH>
    <PWUNIQUEID>112301</PWUNIQUEID>
    <AHNAME>John Doe</AHNAME>
    <AMOUNT>10.00</AMOUNT>
    <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
    <EMAIL>jd@example.com</EMAIL>
    <CCTYPE>VISA</CCTYPE>
    <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

The following XML parameters are returned by the OSBP, API, and the OCX when using the UI form:

| Parameter | Type | Description |
| --- | --- | --- |
| PWCLIENTID | int | ID associated with merchant. |
| PWINVOICENUMBER | string | The Merchant's unique invoice number submitted in the transaction request. |
| RESULT | string | The result of the transaction: `APPROVAL`, `SUCCESS`, `DECLINED`, `ERROR`. |
| RESTEXT | string | Contains the error message. |
| AMOUNT | int/decimal | Amount of the transaction total including any adjustments and taxes. Maximum 7 digits, excluding decimals. |
| PWADJDESC | string | 'Consumer Fee'-enabled merchants only: The description for the service adjustment as set in the merchant configuration. |
| PWADJAMOUNT | int/decimal | 'Consumer Fee'-enabled merchants only: Amount of the service adjustment. Maximum 7 digits, excluding decimals. |
| PWSALETAX | int/decimal | 'Consumer Fee'-enabled merchants only: Amount of the sales tax calculated based on the 'Sales Tax Flat Rate %' set in the merchant configuration. Maximum 7 digits, excluding decimals. |
| PWSALEAMOUNT | int/decimal | Original Sale Amount, before any markups or discounts. Max 7 digits, excluding decimals. |
| MASKEDACCOUNTNUMBER | string | The masked account number under which the transaction was processed. |
| PAYMETH | string | Method of payment with which the transaction was processed: `A` for web ACH, `C` for Card. |
| CCTYPE | string | The card type used. This field is blank if `PAYMETH` is `A`. |
| AHNAME | string | The account holder's name that was supplied. |
| AHFIRSTNAME | string | The account holder's first name that was supplied. |
| AHLASTNAME | string | The account holder's last name that was supplied. |
| PWUNIQUEID | string | The unique ID assigned by Paywire associated with this transaction. |
| EMAIL | string | The user's email address that was supplied at the start of the transaction. |
| AUTHCODE | string | Authorization code associated with the transaction, if applicable. |
| PWCID | string | Paywire Customer Identifier associated with a transaction. If the original request was to create a customer, then this will be the new customer ID. |
| AVSCODE | string | Transaction AVS code result. Refer to [AVS Codes table](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#avs-codes). |
| CVVCODE | int | Transaction CVV result: `1` for a match, `0` for a failure. |
| RECURRING | int | The periodic amount if the value under `PWCTRANSTYPE` is selected. |

JSON Request/Response
---------------------

> Common Request XML Elements:

```
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID></PWCLIENTID>
      <PWKEY></PWKEY>
      <PWUSER></PWUSER>
      <PWPASS></PWPASS>
      <PWTRANSACTIONTYPE></PWTRANSACTIONTYPE>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <FIRSTNAME>John</FIRSTNAME>
      <FIRSTNAME>Doe</FIRSTNAME>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Common Request JSON Example (translated from the above XML):

```
{
     "TRANSACTIONHEADER": {
      "PWVERSION": "3",
      "PWCLIENTID":"",
      "PWKEY": "",
      "PWUSER": "",
      "PWPASS": "",
      "PWTRANSACTIONTYPE": ""
     },
     "CUSTOMER": { 
      "FIRSTNAME": "John",
      "LASTNAME": "Doe",
     }
    }
```

> Common Response JSON Example:

```
{
     "PAYMENTRESPONSE": {
      "RESULT": "APPROVAL" ,
      "BATCHID": "1",
      "PWCLIENTID": "0000000001",
      "PAYMETH": "A",
      "PWUNIQUEID": "112302",
      "AHNAME": "John Doe",
      "AMOUNT": "10.00",
      "MACCOUNT": "XXXXXX4082",
      "EMAIL": "jd@example.com",
      "CCTYPE": "ACH",
      "PWINVOICENUMBER": "0987654321234567890"
     }
    }
```

Payscout supports both XML and JSON. Even though all examples are provided in XML there is an easy translation from XML to JSON as per the following examples on the right side.

 This design provides consistency across both formats, ensuring that all field names, hierarchy, and required values remain identical regardless of whether the message is exchanged in XML or JSON.

Test Cards
----------

The following are available for you to test with the Paywire gateway, expiration date can be set to any date.

| Card Scheme | Number | CVV |
| --- | --- | --- |
| VISA | 4111111111111111 | 123 |
| VISA | 4761739001010267 | 123 |
| Mastercard | 5413330089010608 | 123 |
| Discover | 6510000000000034 | 123 |
| Amex | 379605170000771 | 1234 |
| JCB | 3566000021111117 | 123 |
| Diners | 36185900022226 | 123 |
| Visa Debit | 4002960001111116 | 123 |
| Visa HSA | 4264280001234559 | 123 |

| Account Type | Routing Number | Account Number | Used for |
| --- | --- | --- | --- |
| Current/Savings | 222224444 | 222224444 or any same digit number | For Regular Testing |
| Current/Savings | 11000028 | 11111111111111111 | For ME merchants |
| Current/Savings | 121140399 | 3300674738 | For ME merchants |
| Current/Savings | 121140399 | 3300674723 | For ME merchants |

ACH Bank Details for generating Verify Decline Codes
----------------------------------------------------

The following are Routing/Account combinations to generate ACH Verify Decline Codes: [ACH Verify Decline Codes.](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#ACH-Verify-Decline_Codes)

| Account Type | Routing Number | Account Number | ResultCode |
| --- | --- | --- | --- |
| Current/Savings | 061000052 | 999999991 | AVC1 |
| Current/Savings | 061000052 | 999999992 | AVC2 |
| Current/Savings | 061000052 | 999999993 | AVC3 |
| Current/Savings | 061000052 | 999999994 | AVC4 |
| Current/Savings | 061000052 | 999999995 | AVC5 |
| Current/Savings | 061000052 | 999999996 | AVC6 |
| Current/Savings | 061000052 | 999999997 | AVC7 |
| Current/Savings | 061000052 | 999999998 | AVC8 |
| Current/Savings | 061000052 | 999999999 | AVC9 |
| Current/Savings | 061000052 | 999999910 | AVC10 |
| Current/Savings | 061000052 | 999999990 | AVC0 |

API Reference
-------------

The Application Programming Interface ("API") is the alternate subroutine interface to the Off Site Buy Page (OSBP). The API is primarily used by clients who wish to add payment acceptance methods to their existing application.

Currently, the Paywire API accepts requests in XML using HTTP POST only.

API Overview
------------

> Source Code Example:

```
protected string pwPost(string url, string xmlPayload)
{
  HttpWebRequest req;
  HttpWebResponse res;
  try
  {
    req = (HttpWebRequest)WebRequest.Create(url);
    req.Method = "POST";
    req.ContentType = "text/xml; charset=utf-8";

    req.ContentLength = xmlPayload.Length;
    var sw = new StreamWriter(req.GetRequestStream());
    sw.Write(xmlPayload);
    sw.Close();

    res = (HttpWebResponse)req.GetResponse();
    Stream responseStream = res.GetResponseStream();
    var streamReader = new StreamReader(responseStream);

    //Read the response into an xml document 
    var xml = new XmlDocument();
    xml.LoadXml(streamReader.ReadToEnd());

    var result = xml.InnerXml;

    return result;
  }
  catch (Exception ex)
  {
    throw;
  }
}
```

> `xmlPayload` is the only variable between different transaction types.

To use the Paywire API you will need to:

1.   Implement logic in your application to determine the transaction type required.
2.   Collect the necessary information from the customer (where applicable), which may include PCI data. 
3.   Build an XML string including authentication parameters and (at a minimum) mandatory fields for the transaction type being processed.
4.   Send an HTTP POST containing the XML string to the Paywire API endpoint.
5.   Receive an XML response to parse and use.

* * *

[![Image 1: API Process Flowchart](https://project.paywire.com/dbtranz/docs/OSBP/files/images/pwapiflowv1.png)](https://project.paywire.com/dbtranz/docs/OSBP/files/images/pwapiflowv1.png)

API Endpoints
-------------

The same API endpoint is available for all requests, across all environments:

```
POST /API/pwapi 
Content-Type: text/xml
```

API Transaction Types
---------------------

The following transactions can be processed via the Paywire API.

Simply submit the relevant value in `PWTRANSACTIONTYPE`, along with the required XML parameters.

| Value | Description |
| --- | --- |
| SALE | Charge a card or bank account (if applicable). |
| VOID | Void a transaction. The transaction amount must match the amount of the original transaction, and the `PWUNIQUEID` must match the unique identifier associated with the transaction to void. The transaction must be in the current open batch to void it. |
| CREDIT | Credit a transaction. The transaction amount must be equal to or less than the amount to credit, and the `PWUNIQUEID` must match the unique identifier associated with the transaction to credit. Only transactions in a closed batch with a status of `SETTLED` can be credited. |
| PREAUTH | Pre-authorize a card. |
| GETAUTHTOKEN | Exchange your credentials for an `AUTHTOKEN` to use when calling the [OSBP](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#authentication). |
| GETCONSUMERFEE | Input the sale amount to get adjustment, tax, and total transaction amounts. Relevant for merchants configured with Cash Discount or [Convenience Fees](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#convenience-fees). |
| CREATECUSTOMER | Creates a Customer in the Paywire Vault. |
| GETCUSTOKENS | Lists tokens stored against a given customer. |
| STORETOKEN | Validate a card and return a token. |
| REMOVETOKEN | Delete an existing token from Paywire. |
| VERIFICATION | Verification transaction will verify the customer's card or bank account before submitting the payment. |
| BATCHINQUIRY | Get the current open batch summary. |
| CLOSE | Close the current open batch. |
| SEARCHTRANS | Query the database for transaction results. |
| GETPERIODICPLAN | Query the database for periodic plan details using `RECURRINGID`, `PWTOKEN` or `PWCID`. |
| DELETERECURRING | Delete a periodic plan. |
| SENDRECEIPT | Sends a receipt for a given transaction. |

API One-Time-Sale
-----------------

> Card Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>10</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <COMPANYNAME>The Company</COMPANYNAME>
      <FIRSTNAME>John</FIRSTNAME>
      <LASTNAME>Doe</LASTNAME>
      <EMAIL>jd@example.com</EMAIL>
      <ADDRESS1>1 The Street</ADDRESS1>
      <CITY>New York</CITY>
      <STATE>NY</STATE>
      <ZIP>12345</ZIP>
      <COUNTRY>US</COUNTRY>
      <PRIMARYPHONE>1234567890</PRIMARYPHONE>
      <WORKPHONE>1234567890</WORKPHONE>
      <PWMEDIA>CC</PWMEDIA>
      <CARDNUMBER>4111111111111111</CARDNUMBER>
      <EXP_MM>02</EXP_MM>
      <EXP_YY>22</EXP_YY>
      <CVV2>123</CVV2>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> E-Check Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>10</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <COMPANYNAME>The Company</COMPANYNAME>
      <FIRSTNAME>John</FIRSTNAME>
      <LASTNAME>Doe</LASTNAME>
      <EMAIL>jd@example.com</EMAIL>
      <ADDRESS1>1 The Street</ADDRESS1>
      <CITY>New York</CITY>
      <STATE>NY</STATE>
      <ZIP>12345</ZIP>
      <COUNTRY>US</COUNTRY>
      <PRIMARYPHONE>1234567890</PRIMARYPHONE>
      <WORKPHONE>1234567890</WORKPHONE>
      <PWMEDIA>ECHECK</PWMEDIA>
      <BANKACCTTYPE>CHECKING</BANKACCTTYPE>
      <ROUTINGNUMBER>222224444</ROUTINGNUMBER>
      <ACCOUNTNUMBER>4242204082</ACCOUNTNUMBER>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Approved Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <BATCHID>1</BATCHID>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <PAYMETH>A</PAYMETH>
    <PWUNIQUEID>112302</PWUNIQUEID>
    <AHNAME>John Doe</AHNAME>
    <AMOUNT>10.00</AMOUNT>
    <MACCOUNT>XXXXXX4082</MACCOUNT>
    <EMAIL>jd@example.com</EMAIL>
    <CCTYPE>ACH</CCTYPE>
    <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

> Declined Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>DECLINED</RESULT>
    <RESTEXT>  ERROR  0295   </RESTEXT>
    <RESPONSECODE>123</RESPONSECODE>
    <BATCHID>1</BATCHID>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <AVSCODE>0</AVSCODE>
    <PAYMETH>C</PAYMETH>
    <PWUNIQUEID>112301</PWUNIQUEID>
    <AHNAME>John Doe</AHNAME>
    <AMOUNT>10.00</AMOUNT>
    <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
    <EMAIL>jd@example.com</EMAIL>
    <CCTYPE>VISA</CCTYPE>
    <ISDEBIT>TRUE</ISDEBIT>
    <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

> RCC - Remotely Created Checks Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
      <CARDPRESENT>FALSE</CARDPRESENT>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <REQUESTTOKEN>FALSE</REQUESTTOKEN>
      <PWMEDIA>ECHECK</PWMEDIA>
      <BANKACCTTYPE>CHECKING</BANKACCTTYPE>
      <SECCODE>ICL</SECCODE>
      <ROUTINGNUMBER>222224444</ROUTINGNUMBER>
      <ACCOUNTNUMBER>4242204082</ACCOUNTNUMBER>
      <FIRSTNAME>John</FIRSTNAME>
      <LASTNAME>Doe</LASTNAME>
      <ADDRESS1>1 The Street</ADDRESS1>
      <ADDRESS2></ADDRESS2>
      <CITY>New York</CITY>
      <STATE>NY</STATE>
      <COUNTRY>US</COUNTRY>
      <ZIP>12345</ZIP>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> RCC - Remotely Created Checks Approved Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <BATCHID>1</BATCHID>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <PAYMETH>A</PAYMETH>
    <PWUNIQUEID>112302</PWUNIQUEID>
    <AHNAME>John Doe</AHNAME>
    <PWSALETAX>0.00</PWSALETAX>
    <PWADJAMOUNT>0.00</PWADJAMOUNT>
    <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
    <AMOUNT>10.00</AMOUNT>
    <MACCOUNT>XXXXX6789</MACCOUNT>
    <CCTYPE>ACH</CCTYPE>
    <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

To process a Sale transaction, submit `SALE` in the `<PWTRANSACTIONTYPE />` parameter along with the mandatory fields.

A `SALE` transaction now supports RCC: Remotely Created Checks.

 Remotely Created Checks, or RCC, is a broad term used to describe processing which clears transactions through a bank-to-bank file transfer rather than through the ACH network.

 Both RCC and ACH are used for web and phone transactions as well as one-time and recurring debits from bank accounts.

 The customer experience for RCC and ACH is the same for the most part.

 From the merchant's perspective, the process is almost identical.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `SALE` |
| PWSALEAMOUNT | ✓ | int/decimal | Amount of the transaction. |  |
| PWINVOICENUMBER |  | string | Merchant’s unique invoice number to be associated with this transaction. If not submitted, this will be generated by the gateway and returned in the XML response. | `0/20`, Alphanumeric |
| PWMEDIA | ✓ | string | Defines the payment method. | Fixed options: `CC` and `ECHECK`. |
| CARDNUMBER | (✓) | int | Card number to process payment with. Required only when `CC` is submitted in `PWMEDIA`. |  |
| EXP_MM | (✓) | string | Card expiry month. Required only when `CC` is submitted in `PWMEDIA`. | `2/2, >0, <=12` |
| EXP_YY | (✓) | string | Card expiry year. Required only when `CC` is submitted in `PWMEDIA`. | `2/2` |
| CVV2 | (✓) | int | Card Verification Value. Can only be required when `CC` is submitted in `PWMEDIA`. Requirement is configurable on a merchant-by-merchant basis. | `3/4` |
| BANKACCTTYPE | (✓) | string | Type of Bank Account to process payment with. Required only when `ECHECK` is submitted in `PWMEDIA`. | `CHECKING`, `SAVINGS` |
| ROUTINGNUMBER | (✓) | string | Routing number of Bank Account to process payment with. Required only when `ECHECK` is submitted in `PWMEDIA`. |  |
| ACCOUNTNUMBER | (✓) | string | Account number of Bank Account to process payment with. Required only when `ECHECK` is submitted in `PWMEDIA`. |  |
| ADDCUSTOMER |  | bool | Creates a customer and an associated token and returns a `PWCID` and a `PWTOKEN` in the response when set to `TRUE`. Overrides `REQUESTTOKEN` if also submitted. |  |
| REQUESTTOKEN |  | bool | Creates a token and returns a `PWTOKEN` in the response when set to `TRUE`. By default, when not submitted, a `PWTOKEN` is returned when `CC` is submitted in `PWMEDIA` but not for `ECHECK`. |  |
| PWCID |  | string | Paywire Customer Identifier. If `REQUESTTOKEN` is also submitted as `TRUE`, the created token will be associated with this customer. |  |
| PWTOKEN |  | string | Unique token representing a customer's card or account details stored on the Paywire Gateway. Use instead of submitting `CARDNUMBER`, `EXP_MM`, `EXP_YY` and `CVV2` or `ROUTINGNUMBER` and `ACCOUNTNUMBER`. |  |
| CUSTOMERNAME |  | string | Full name of the customer, possibly different to the Account Holder. |  |
| FIRSTNAME |  | string | Account Holder's first name (required for RCC). |  |
| LASTNAME |  | string | Account Holder's last name (required for RCC). |  |
| COMPANYNAME |  | string | Customer's company name. |  |
| ADDRESS1 |  | string | Account Holder's primary address (required for RCC). |  |
| ADDRESS2 |  | string | Account Holder's secondary address (required for RCC). |  |
| CITY |  | string | Account Holder's city of residence (required for RCC). |  |
| STATE | (✓) | string | Account Holder's state of residence. Required if configured with [Convenience Fees](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#convenience-fees) (required for RCC). |  |
| COUNTRY |  | string | Account Holder's country of residence (required for RCC). |  |
| ZIP |  | string | Account Holder's address postal/zip code (required for RCC) See important note on [Zip Codes](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#Zip-codes). |  |
| EMAIL |  | string | Account Holder's email address. |  |
| PRIMARYPHONE |  | string | Account Holder's primary phone number. |  |
| WORKPHONE |  | string | Account Holder's work phone number. |  |
| DISABLECF |  | bool | Overrides applying a Convenience Fee or Cash Discount when set to `TRUE`, if configured. Note that Sales Tax will also be disabled. | Default: `FALSE` |
| POSINDICATOR |  | string | Used in conjunction with Token Sales to apply Convenience Fees or Cash Discount for periodic payments handled outside Paywire. Submit this in the `TRANSACTIONHEADER` block. | `C`: Regular Token Sale `I`: First Payment of a Periodic Plan `R`: Subsequent Periodic Payment `T`: Last Payment of a Periodic Plan `P`: Periodic Payment |
| PWADJAMOUNT |  | decimal | Adjustment amount. Used to set the Convenience Fee amount to be charged for this transaction. Allowed only when submitted with `POSINDICATOR` set to `P`. Submitting amounts larger than that configured for the merchant will be ignored. | `>0` |
| ADJTAXRATE |  | decimal | Overrides the configured Sales Tax rate. |  |
| PWCUSTOMID1 |  | string | Custom third-party ID to be associated with this transaction. |  |
| PWRECEIPTDESC |  | string | Extra information to be displayed on the receipt. | `0/200` |
| PWCASHIERID |  | string | Paywire-assigned cashier identifier. |  |
| SECCODE |  | string | SEC Code for ECHECK payments. | `3/3``ICL (for RCC)` |
| DESCRIPTION |  | string | Transaction custom description message. | `0/100` |

### Sale Response Parameters

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| RESULT | string | Transaction Result | `APPROVAL` , `DECLINED` ,`ERROR` |
| RESPONSECODE | string | Card transaction response code returned from the processor. | Data varies based on processor config. See: [Processor-Decline-Code](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#Processor-Decline-Code) |
| RESTEXT | string | Transaction response message. |  |
| BATCHID | string | Transaction batch ID. |  |
| PWCLIENTID | string | Authentication credential provided to you by the administrator. |  |
| PAYMETH | string | Method of payment that the transaction was processed with: `A` for web ACH, `C` for Card. |
| PWUNIQUEID | string | The unique ID of the transaction. |  |
| AHNAME | string | The full name of the account holder. |  |
| AHFIRSTNAME | string | The first name of the account holder. |  |
| AHLASTNAME | string | The last name of the account holder. |  |
| PWADJAMOUNT | decimal | The adjustment amount of the transaction, applicable to Convenience Fee and Cash Discount transactions. |  |
| AMOUNT | decimal | The total approved amount of the transaction, including any adjustments. |  |
| MACCOUNT | string | The masked account number. |  |
| CCTYPE | string | For card payment, this is the cardbrand, for ACH payment, it is always `ACH` | `VISA`, `MC`, `DISC`, `AMEX`, `CUP`, `JCB`, `DINERS`, `ACH` |
| PWINVOICENUMBER | string | The merchants unique invoice number associated with this transaction. |  |
| PWTOKEN | string | The payment token for the payment method. |  |
| PWCID | string | The customer ID. |  |
| RECURRINGID | string | The plan UID for periodic payment plan. |  |
| AUTHCODE | string | The auth code from the processor. |  |
| AVSCODE | string | The AVS Response code from the processor. |  |
| CVVCODE | string | The CVV Response code from the processor. |  |
| ISDEBIT | Bool | Indicate if the card is a debit or credit card. | `TRUE`/`FALSE` |  |

API Periodic Sale
-----------------

> Request Example:

```
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      ...
   </TRANSACTIONHEADER>
   <CUSTOMER>
      ...
   </CUSTOMER>
   <RECURRING>
      <STARTON>2018-01-01</STARTON>
      <PAYMENTTOTAL>1234.5</PAYMENTTOTAL>
      <FREQUENCY>W</FREQUENCY>
      <PAYMENTS>4</PAYMENTS>
   </RECURRING>
</PAYMENTREQUEST>
```

> For brevity, parameters identical to the One-Time-Sale XML request structure have been summarized by `...`
> 
> 
> Approved Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <BATCHID>1</BATCHID>
    <PWCLIENTID>000000001</PWCLIENTID>
    <AUTHCODE>012345</AUTHCODE>
    <AVSCODE>R</AVSCODE>
    <PAYMETH>C</PAYMETH>
    <PWUNIQUEID>100896</PWUNIQUEID>
    <AHNAME>John Doe</AHNAME>
    <AMOUNT>10.00</AMOUNT>
    <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
    <EMAIL>jd@example.com</EMAIL>
    <CCTYPE>VISA</CCTYPE>
    <PWINVOICENUMBER>0987654321234567891</PWINVOICENUMBER>
    <RECURRINGID>113</RECURRINGID>
</PAYMENTRESPONSE>
```

> Declined Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>ERROR</RESULT>
    <RESTEXT>Invalid PAYMENTS</RESTEXT>
</PAYMENTRESPONSE>
```

> This request demonstrates the functioning of Payment Total, the Payment total Amount is 120 and the sale amount is 50 with 2 payments, in the first payment, the sale will be 50, in the 2nd it will be 70.

```
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      ...
   </TRANSACTIONHEADER>
   <CUSTOMER>
      ...
       <PWSALEAMOUNT>50</PWSALEAMOUNT>
      ...
   </CUSTOMER>
   <RECURRING>
      <STARTON>2018-01-01</STARTON>
      <PAYMENTTOTAL>120</PAYMENTTOTAL>
      <FREQUENCY>W</FREQUENCY>
      <PAYMENTS>2</PAYMENTS>
   </RECURRING>
</PAYMENTREQUEST>
```

In order to create a Periodic setup, simply include the `<RECURRING>` block in addition to the [One-Time-Sale parameters](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-one-time-sale).

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| STARTON | ✓ | Date | Date the first payment must be charged. | Date Format `yyyy-mm-dd`. |
| PAYMENTTOTAL |  | Int/Decimal | Payment Total Amount of Periodic transaction, Users can utilize this field if the recurring total cannot be divided by the payment count. | Paymentotal > (Payment Count * Single Amount). |
| FREQUENCY | ✓ | string | The frequency at which Periodic payments are charged. | `W`: Weekly, `B`: Bi-weekly, `M`: Monthly, `H`: Semi-monthly, `Q`: Quarterly, `S`: Semi-annual, `Y`: Yearly |
| PAYMENTS | ✓ | int | Number of payments to process until the Periodic setup is expired. | 1/999 |

API Delete Periodic Sale
------------------------

To delete a Periodic Sale within the gateway, submit `DELETERECURRING` in the `<PWTRANSACTIONTYPE />` parameter along with the desired `RECURRINGID`.

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>DELETERECURRING</PWTRANSACTIONTYPE>
      <XOPTION>TRUE</XOPTION>
      <RECURRINGID>123</RECURRINGID>
   </TRANSACTIONHEADER>
</PAYMENTREQUEST>
```

### Request Parameters

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `DELETERECURRING` |
| XOPTION | ✓ | Bool | Show the search result in XML or escaped XML. | `TRUE` or `FALSE` |
| RECURRINGID | ✓ | int | Periodic Plan ID. |  |

> Approved Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <PWCLIENTID>{clientId}</PWCLIENTID>
    <PAYMETH>C</PAYMETH>
    <AMOUNT>0.00</AMOUNT>
    <PWINVOICENUMBER>21141024825250035</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

> Declined Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>ERROR</RESULT>
    <RESTEXT>INVALID RECURRINGID</RESTEXT>
</PAYMENTRESPONSE>
```

### Response Parameters

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| PWCLIENTID | string | Authentication credential provided to you by the administrator. |  |
| PAYMETH | string | Method of payment that the transaction was processed with: `A` for web ACH, `C` for Card. |
| AMOUNT | decimal | The total amount of the transaction, including tax and any adjustments. |  |
| PWINVOICENUMBER | string | The merchants unique invoice number associated with this transaction. |  |

API PreAuth
-----------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>PREAUTH</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>10</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567895</PWINVOICENUMBER>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <PWMEDIA>CC</PWMEDIA>
      <CARDNUMBER>4111111111111111</CARDNUMBER>
      <EXP_MM>12</EXP_MM>
      <EXP_YY>22</EXP_YY>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <BATCHID>1</BATCHID>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <AUTHCODE>092127</AUTHCODE>
    <AVSCODE>R</AVSCODE>
    <PAYMETH>C</PAYMETH>
    <PWUNIQUEID>130909</PWUNIQUEID>
    <AMOUNT>10.00</AMOUNT>
    <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
    <CCTYPE>VISA</CCTYPE>
    <PWTOKEN>A39B7BD3NOK24CF12816</PWTOKEN>
    <PWINVOICENUMBER>0987654321234567895</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

To process a Pre-Authorization transaction, submit `PREAUTH` in the `<PWTRANSACTIONTYPE />` parameter. Mandatory fields are identical to the [API One-Time-Sale](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-one-time-sale).

API Void
--------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>VOID</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>10</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567892</PWINVOICENUMBER>
      <PWUNIQUEID>112301</PWUNIQUEID>
   </TRANSACTIONHEADER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <BATCHID>162</BATCHID>
    <PWCLIENTID>0000001403</PWCLIENTID>
    <PAYMETH>A</PAYMETH>
    <PWUNIQUEID>130903</PWUNIQUEID>
    <AMOUNT>10.00</AMOUNT>
    <MACCOUNT>XXXXXX4082</MACCOUNT>
    <CCTYPE>ACH</CCTYPE>
    <PWINVOICENUMBER>0987654321234567892</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

To process a Void transaction, submit `VOID` in the `<PWTRANSACTIONTYPE />` parameter along with the mandatory fields.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `VOID` |
| PWSALEAMOUNT | ✓ | int/decimal | Amount of original transaction: Must match. |  |
| PWINVOICENUMBER |  | string | The merchant's unique invoice number associated with this transaction. |  |
| PWUNIQUEID | ✓ | int | Unique transaction ID returned in the transaction response, associated with the transaction being voided. |  |

API Credit
----------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>CREDIT</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>10</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567893</PWINVOICENUMBER>
      <PWUNIQUEID>130291</PWUNIQUEID>
   </TRANSACTIONHEADER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <BATCHID>1</BATCHID>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <PAYMETH>C</PAYMETH>
    <PWUNIQUEID>112304</PWUNIQUEID>
    <AHNAME>John Doe</AHNAME>
    <AMOUNT>10.00</AMOUNT>
    <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
    <EMAIL>jd@example.com</EMAIL>
    <CCTYPE>VISA</CCTYPE>
    <PWINVOICENUMBER>0987654321234567893</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

To process a Credit transaction, submit `CREDIT` in the `<PWTRANSACTIONTYPE />` parameter along with the mandatory fields.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `CREDIT` |
| PWSALEAMOUNT | ✓ | int/decimal | Amount to refund. | Less than or equal to the original transaction. |
| PWINVOICENUMBER |  | string | The merchant's unique invoice number associated with this transaction. |  |
| PWUNIQUEID | ✓ | int | Unique transaction ID returned in the transaction response, associated with the transaction being voided. |  |
| PWCUSTOMID1 |  | string | Custom third-party ID to be associated with this transaction. |  |

API Get Auth Token
------------------

### Request Parameters

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
    <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <PWUSER>{username}</PWUSER>
        <PWPASS>{password}</PWPASS>
        <PWTRANSACTIONTYPE>GETAUTHTOKEN</PWTRANSACTIONTYPE>
    </TRANSACTIONHEADER>
</PAYMENTREQUEST>
```

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `GETAUTHTOKEN` |

### Response Parameters

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>SUCCESS</RESULT>
    <PWCLIENTID>{clientId}</PWCLIENTID>
    <PWINVOICENUMBER>10070170834652361</PWINVOICENUMBER>
    <AUTHTOKEN>4C2F8EE94CA2491AAB67EA6541CB17BA</AUTHTOKEN>
</PAYMENTRESPONSE>
```

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| RESULT | string | Status for the request. | `SUCCESS`, `ERROR` |
| PWCLIENTID | string | Paywire-generated unique merchant identifier. |  |
| PWINVOICENUMBER | string | Identifier for this request. |  |
| AUTHTOKEN | string | The Authentication Token to be used when calling the [OSBP](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#authentication). |  |

API Get Consumer Fee
--------------------

### Request Parameters

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>GETCONSUMERFEE</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567896</PWINVOICENUMBER>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <ADJTAXRATE>15.00</ADJTAXRATE>
      <PWMEDIA>CC</PWMEDIA>
      <STATE>NY</STATE>
   </CUSTOMER>
</PAYMENTREQUEST>
```

For merchants configured with Cash Discount or Convenience Fees, submit `GETCONSUMERFEE` in the `<PWTRANSACTIONTYPE />` parameter to retrieve the adjustment amount.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `GETCONSUMERFEE` |
| PWSALEAMOUNT | ✓ | int/decimal | Sale amount. |  |
| PWINVOICENUMBER |  | string | The merchant's unique invoice number associated with this transaction. |  |
| PWMEDIA | ✓ | string | Defines the payment method. | Fixed options: `CC` and `ECHECK`. |
| DISABLECF |  | bool | Overrides applying a Cash Discount or Convenience Fee when set to `TRUE`, if configured. Note that Sales Tax will also be disabled. | Default: `FALSE` |
| ADJTAXRATE |  | decimal | Overrides the configured Sales Tax rate. |  |
| PWTOKEN |  | string | When submitted, returns customer or token details in the response. |  |
| STATE | (✓) | string | Account Holder's state of residence. Required if configured with [Convenience Fees](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#convenience-fees). |  |

### Response Parameters

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <PAYMETH>C</PAYMETH>
    <PWADJDESC>Convenience Fee</PWADJDESC>
    <PWSALETAX>0.00</PWSALETAX>
    <PWADJAMOUNT>5.00</PWADJAMOUNT>
    <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
    <AMOUNT>15.00</AMOUNT>
    <PWINVOICENUMBER>0987654321234567896</PWINVOICENUMBER>
    <CDSUMMARY>
        <MERCHANTNAME>Merchant ABC</MERCHANTNAME>
        <MID>987345098456</MID>
        <MERCHANTTYPE>F</MERCHANTTYPE>
        <ADJTAXRATE>0.00</ADJTAXRATE>
        <CARDSALESAMOUNT>10.00</CARDSALESAMOUNT>
        <CARDADJAMOUNT>5.00</CARDADJAMOUNT>
        <CARDTAXAMOUNT>0.00</CARDTAXAMOUNT>
        <CARDTRANSACTIONAMOUNT>15.00</CARDTRANSACTIONAMOUNT>
        <CARDAMOUNTBEFORETAX>15.00</CARDAMOUNTBEFORETAX>
        <CASHSALESAMOUNT>10.00</CASHSALESAMOUNT>
        <CASHADJAMOUNT>5.00</CASHADJAMOUNT>
        <CASHTAXAMOUNT>0.00</CASHTAXAMOUNT>
        <CASHTRANSACTIONAMOUNT>15.00</CASHTRANSACTIONAMOUNT>
        <CASHAMOUNTBEFORETAX>15.00</CASHAMOUNTBEFORETAX>
        <CDDESCRIPTIONVPOS>Attention:
            <br />Please advise the payee or customer that a 'Convenience Fee' will be charged when paying on-line or over the phone. They can otherwise opt to pay in-store or by mail.
        </CDDESCRIPTIONVPOS>
        <CDDESCRIPTIONOSBP>Attention:
            <br />Please be advised that a 'Convenience Fee' is charged when paying on-line or over the phone. You can otherwise opt to pay in-store or by mail.
        </CDDESCRIPTIONOSBP>
    </CDSUMMARY>
</PAYMENTRESPONSE>
```

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| RESULT | string | Status for the transaction. | `APPROVAL`, `SUCCESS`, `DECLINED`, `ERROR` |
| PWCLIENTID | string | Paywire-generated unique merchant identifier. |  |
| PAYMETH | string | Describes the payment method. | `C`: Card, `A`: ACH |
| PWADJDESC | string | The description for the adjustment as set in the merchant configuration. |  |
| PWSALETAX | decimal | The tax amount calculated by the gateway, based on the Sales Tax rate set in the merchant configuration. |  |
| PWADJAMOUNT | decimal | The adjustment amount calculated by the gateway, based on the Adjustment rate or fixed amount set in the merchant configuration. This can be either the Cash Discount markdown or the Convenience Fee. |  |
| PWSALEAMOUNT | decimal | The Sale amount submitted in the request. |  |
| AMOUNT | decimal | The total amount of the transaction, including tax and any adjustments. |  |
| PWINVOICENUMBER | string | The merchant's unique invoice number associated with this transaction. |  |
| MERCHANTNAME | string | Name of the merchant as set in the merchant configuration. |  |
| MID | string | The processor's merchant identifier. |  |
| MERCHANTTYPE | string | The type of merchant as set in the merchant configuration. | `A`: General + Single SAP, `B`: Medical, `C`: General + Split SAP, `D`: Remote Check + SAP Invoices, `E`: Cash Discount, `F`: Convenience Fees |
| ADJTAXRATE | decimal | The Sales Tax rate as set in the merchant configuration or submitted in the request. |  |
| CARDSALESAMOUNT | decimal | The Card Sale amount before tax and any adjustments. Relevant for Cash Discount. |  |
| CARDADJAMOUNT | decimal | The Adjustment amount for a Card transaction. Relevant for Cash Discount. |  |
| CARDTAXAMOUNT | decimal | The calculated Sales Tax amount for a Card transaction. Relevant for Cash Discount. |  |
| CARDTRANSACTIONAMOUNT | decimal | The total amount for a Card transaction after tax and any adjustments. Relevant for Cash Discount. |  |
| CARDAMOUNTBEFORETAX | decimal | The adjusted amount for a Card transaction before adding tax. Relevant for Cash Discount. |  |
| CASHSALESAMOUNT | decimal | The Cash Sale amount before tax and any adjustments. Relevant for Cash Discount. |  |
| CASHTAXAMOUNT | decimal | The calculated Sales Tax amount for a Cash transaction. Relevant for Cash Discount. |  |
| CASHTRANSACTIONAMOUNT | decimal | The total amount for a Cash transaction after tax and any adjustments. Relevant for Cash Discount. |  |
| CASHAMOUNTBEFORETAX | decimal | The adjusted amount for a Cash transaction before adding tax. Relevant for Cash Discount. |  |
| CDDESCRIPTIONVPOS | string | The descriptive text set in the merchant configuration. To be displayed on the VPOS payment page. |  |
| CDDESCRIPTIONOSBP | string | The descriptive text set in the merchant config. to be displayed on the OSBP payment page. |  |
| AHNAME | string | ACH Account Holder full name. Returned only when `ECHECK` in `PWMEDIA` and a valid `PWTOKEN` are submitted in the request. |  |
| MACCOUNT | string | Masked Card or Account number. Returned only when a valid `PWTOKEN` is submitted in the request. |  |
| ROUTINGNUMBER | string | U.S. Bank Account routing number. Returned only when `ECHECK` in `PWMEDIA` and a valid `PWTOKEN` are submitted in the request. |  |
| BANKACCTTYPE | string | Type of Bank Account. Returned only when `ECHECK` in `PWMEDIA` and a valid `PWTOKEN` are submitted in the request. | `CHECKING`, `SAVINGS` |
| EXP_MM | string | Card Expiry month. Returned only when `CC` in `PWMEDIA` and a valid `PWTOKEN` are submitted in the request. |  |
| EXP_YY | string | Card Expiry year. Returned only when `CC` in `PWMEDIA` and a valid `PWTOKEN` are submitted in the request. |  |
| FIRSTNAME | string | Account Holder first name. Returned only when a valid `PWTOKEN` is submitted in the request. |  |
| LASTNAME | string | Account Holder last name. Returned only when a valid `PWTOKEN` is submitted in the request. |  |

API Create Customer
-------------------

> Request Example

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
    <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <PWUSER>{username}</PWUSER>
        <PWPASS>{password}</PWPASS>
        <PWTRANSACTIONTYPE>CREATECUSTOMER</PWTRANSACTIONTYPE>
    </TRANSACTIONHEADER>
    <DETAILRECORDS />
    <CUSTOMER>
        <COMPANYNAME>Company ABC</COMPANYNAME>
        <FIRSTNAME>John</FIRSTNAME>
        <LASTNAME>Doe</LASTNAME>
        <EMAIL>jd@example.com</EMAIL>
        <ADDRESS1>1, The Street</ADDRESS1>
        <ADDRESS2>Unit 10</ADDRESS2>
        <CITY>Los Angeles</CITY>
        <STATE>CA</STATE>
        <ZIP>12345</ZIP>
        <COUNTRY>US</COUNTRY>
        <PRIMARYPHONE>1234567890</PRIMARYPHONE>
        <WORKPHONE>1234567890</WORKPHONE>
        <DESCRIPTION>Description</DESCRIPTION>
        <EXTCID>AA000123</EXTCID>
    </CUSTOMER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>SUCCESS</RESULT>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <PWCID>P0000000001</PWCID>
    <PWINVOICENUMBER>0987654321234127820</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

To create a customer in the Paywire Vault, submit `CREATECUSTOMER` in the `<PWTRANSACTIONTYPE />` parameter. The gateway will return a customer identifier in `<PWCID>` if successful.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `CREATECUSTOMER` |
| COMPANYNAME |  | string | Customer's company name. |  |
| ADDRESS1 |  | string | Customer's primary address. |  |
| ADDRESS2 |  | string | Customer's secondary address. |  |
| CITY |  | string | Customer's city of residence. |  |
| STATE |  | string | Customer's state of residence. |  |
| COUNTRY |  | string | Customer's country of residence. |  |
| ZIP |  | string | Customer's address postal/zip code, See important note on [Zip Codes](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#Zip-codes). |  |
| EMAIL |  | string | Customer's email address. |  |
| PRIMARYPHONE |  | string | Customer's primary phone number. |  |
| WORKPHONE |  | string | Customer's work phone number. |  |
| DESCRIPTION |  | string | Customer description. | `0/100` |
| EXTCID |  | string | External Customer ID | `0/50` |

API List Customer Tokens
------------------------

> Request Example

```
<PAYMENTREQUEST>
    <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <PWUSER>{username}</PWUSER>
        <PWPASS>{password}</PWPASS>
	<XOPTION>TRUE</XOPTION>
        <PWTRANSACTIONTYPE>GETCUSTOKENS</PWTRANSACTIONTYPE>
    </TRANSACTIONHEADER>
    <DETAILRECORDS />
    <CUSTOMER>
        <PWCID>P0000000001</PWCID>
	<EXTCID>AA000123</EXTCID>
    </CUSTOMER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>SUCCESS</RESULT>
    <PWCLIENTID>0000000120</PWCLIENTID>
    <PWCUSTOMERDETAILS>
        <PWCUSTOMERDETAIL>
            <PWCID>P00008F4039</PWCID>
            <EXTCID>test</EXTCID>
            <CUSTOMERNAME>Test Last</CUSTOMERNAME>
            <EMAIL>test@example.com</EMAIL>
            <PHONE>8888888888</PHONE>
            <ADDRESS>1 Main St</ADDRESS>
            <CITY>Hartford</CITY>
            <STATE>AE</STATE>
            <ZIP>06105</ZIP>
            <TOKENS>
                <PWTOKENDETAIL>
                    <PWTOKEN>TB32D49C0B9CA840</PWTOKEN>
                    <PWMEDIA>CC</PWMEDIA>
                    <CCTYPE>MC</CCTYPE>
                    <MACCOUNT>X608 </MACCOUNT>
                    <EXP_MM>02</EXP_MM>
                    <EXP_YY>25</EXP_YY>
                </PWTOKENDETAIL>
                <PWTOKENDETAIL>
                    <PWTOKEN>T3C6A1FC9B022912</PWTOKEN>
                    <PWMEDIA>CC</PWMEDIA>
                    <CCTYPE>VISA</CCTYPE>
                    <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
                    <EXP_MM>12</EXP_MM>
                    <EXP_YY>22</EXP_YY>
                </PWTOKENDETAIL>
                <PWTOKENDETAIL>
                    <PWTOKEN>T19EE239FE11C914</PWTOKEN>
                    <PWMEDIA>CC</PWMEDIA>
                    <CCTYPE>VISA</CCTYPE>
                    <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
                    <EXP_MM>02</EXP_MM>
                    <EXP_YY>20</EXP_YY>
                </PWTOKENDETAIL>
            </TOKENS>
        </PWCUSTOMERDETAIL>
        <PWCUSTOMERDETAIL>
            <PWCID>P00008F8043</PWCID>
            <EXTCID>test</EXTCID>
            <CUSTOMERNAME>tttt</CUSTOMERNAME>
        </PWCUSTOMERDETAIL>
    </PWCUSTOMERDETAILS>
</PAYMENTRESPONSE>
```

To list tokens stored against a customer in the Paywire Vault, submit `GETCUSTOKENS` in the `<PWTRANSACTIONTYPE />` parameter, along with parameters `<PWCID>` or `<EXTCID>`. If more than one condition is passed, the logic will consider only one condition and the precedence order is `PWCID`>`EXTCID`.

### Request Parameters:

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `GETCUSTOKENS` |
| XOPTION |  | Bool | Show the search result in XML or escaped XML. | Options: `TRUE` or `FALSE` |
| PWCID |  | string | Identifier for Customer stored in the Paywire Vault. |  |
| EXTCID |  | string | External Customer ID. |  |

### Response Parameters:

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| PWCID | string | Identifier for Customer stored in the Paywire Vault. |  |
| EXTCID | string | External Customer ID. |  |
| CUSTOMERNAME | string | Customer's Name. |  |
| COMPANY | string | Customer's Company Name. |  |
| EMAIL | string | Customer's email address. |  |
| PHONE | string | Customer's Phone. |  |
| ADDRESS | string | Customer's primary address. |  |
| ADDRESS2 | string | Customer's secondary address. |  |
| CITY | string | Customer's city of residence. |  |
| STATE | string | Customer's state or province of residence. |  |
| ZIP | string | Customer's address postal/zip code, See important note on [Zip Codes](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#Zip-codes). |  |
| NOTES | string | Customer's additional information. |  |
| PWTOKEN | string | Unique token representing a customer's card or account details stored on the Paywire Gateway. |  |
| PWMEDIA | string | Defines the payment method. | `CC` `ECHECK` |
| BANKACCTTYPE | string | Type of Bank Account. Returned only when `ECHECK` in `PWMEDIA` and a valid `PWTOKEN` are submitted in the request. | `CHECKING`, `SAVINGS` |
| CCTYPE | string | Type of Credit Card. Returned only when `CC` in `PWMEDIA`. | `VISA`, `MC`, `DISC`, `AMEX`, `CUP`, `JCB`, `DINERS` |
| MACCOUNT | string | Masked account number. |  |
| EXP_MM | string | Card expiry month. Returned only when `CC` in `PWMEDIA`. |
| EXP_YY | string | Card expiry year. Returned only when `CC` in `PWMEDIA`. |

API Store Token
---------------

> Request Example for Credit Card:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>STORETOKEN</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>10</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567897</PWINVOICENUMBER>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <COMPANYNAME>The Company</COMPANYNAME>
      <FIRSTNAME>John</FIRSTNAME>
      <LASTNAME>Doe</LASTNAME>
      <EMAIL>jd@example.com</EMAIL>
      <ADDRESS1>1 The Street</ADDRESS1>
      <CITY>New York</CITY>
      <STATE>NY</STATE>
      <ZIP>12345</ZIP>
      <COUNTRY>US</COUNTRY>
      <PRIMARYPHONE>1234567890</PRIMARYPHONE>
      <WORKPHONE>1234567890</WORKPHONE>
      <PWMEDIA>CC</PWMEDIA>
      <CARDNUMBER>4111111111111111</CARDNUMBER>
      <EXP_MM>02</EXP_MM>
      <EXP_YY>22</EXP_YY>
      <CVV2>123</CVV2>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Response Example for Credit Card:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <BATCHID>1</BATCHID>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <AVSCODE>0</AVSCODE>
    <PAYMETH>C</PAYMETH>
    <PWUNIQUEID>130310</PWUNIQUEID>
    <AMOUNT>0.00</AMOUNT>
    <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
    <CCTYPE>VISA</CCTYPE>
    <PWTOKEN>56A6603A4141234A2817</PWTOKEN>
    <PWINVOICENUMBER>0987654321234567897</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

> Request Example for ECheck:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>STORETOKEN</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>10</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567897</PWINVOICENUMBER>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <COMPANYNAME>The Company</COMPANYNAME>
      <FIRSTNAME>John</FIRSTNAME>
      <LASTNAME>Doe</LASTNAME>
      <EMAIL>jd@example.com</EMAIL>
      <ADDRESS1>1 The Street</ADDRESS1>
      <CITY>New York</CITY>
      <STATE>NY</STATE>
      <ZIP>12345</ZIP>
      <COUNTRY>US</COUNTRY>
      <PRIMARYPHONE>1234567890</PRIMARYPHONE>
      <WORKPHONE>1234567890</WORKPHONE>
      <PWMEDIA>ECHECK</PWMEDIA>
      <ROUTINGNUMBER>222224444</ROUTINGNUMBER>
      <ACCOUNTNUMBER>123456</ACCOUNTNUMBER>
      <BANKACCTTYPE>CHECKING</BANKACCTTYPE>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Response Example for ECheck:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <BATCHID>1</BATCHID>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <PAYMETH>A</PAYMETH>
    <PWUNIQUEID>130310</PWUNIQUEID>
    <AHNAME>John Doe</AHNAME>
    <PWSALETAX>0.00</PWSALETAX>
    <PWADJAMOUNT>0.00</PWADJAMOUNT>
    <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
    <AMOUNT>0.00</AMOUNT>
    <MACCOUNT>XXXXXX4082</MACCOUNT>
    <CCTYPE>ACH</CCTYPE>
    <PWCUSTOMERID>T31F547196597457</PWCUSTOMERID>
    <PWTOKEN>56A6603A4141234A2817</PWTOKEN>
    <PWCID>P0000007990</PWCID>
    <PWINVOICENUMBER>0987654321234567897</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

In order to store a customer's payment details (card or e-check), submit `STORETOKEN` in the `<PWTRANSACTIONTYPE />` parameter. The gateway will return a token in `<PWTOKEN>` if successful.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `STORETOKEN` |
| PWSALEAMOUNT | ✓ | int/decimal | Set to `0`, otherwise a `SALE` is processed. |  |
| PWINVOICENUMBER |  | string | The merchants unique invoice number associated with this transaction. |  |
| COMPANYNAME |  | string | Customer's company name. |  |
| PWMEDIA | ✓ | string | Defines the payment method. | Fixed options: `CC` and `ECHECK`. |
| CARDNUMBER | ✓ | int | Card number to be stored. Required only when `CC` is submitted in `PWMEDIA`. |  |
| EXP_MM | ✓ | string | Card expiry month. Required only when `CC` is submitted in `PWMEDIA`. | `2/2, >0, <=12` |
| EXP_YY | ✓ | string | Card expiry year. Required only when `CC` is submitted in `PWMEDIA`. | `2/2` |
| CVV2 |  | int | Card Verification Value. Can only be required when `CC` is submitted in `PWMEDIA`. Requirement is configurable on a merchant-by-merchant basis. | `3/4` |
| ROUTINGNUMBER | (✓) | string | Routing number of Bank Account being stored. Required only when `ECHECK` is submitted in `PWMEDIA`. |  |
| ACCOUNTNUMBER | (✓) | string | Account number of Bank Account being stored. Required only when `ECHECK` is submitted in `PWMEDIA`. |  |
| BANKACCTTYPE | (✓) | string | Type of Bank Account to process payment with. Required only when `ECHECK` is submitted in `PWMEDIA`. | `CHECKING`, `SAVINGS` |
| ADDCUSTOMER |  | bool | Creates a customer in the Paywire Vault associated with the token, and returns a `PWCID` in the response when set to `TRUE`. |  |
| PWCID |  | string | Paywire Customer Identifier. When submitted, the created token will be associated with this customer. |  |
| FIRSTNAME |  | string | Account Holder's first name. |  |
| LASTNAME |  | string | Account Holder's last name. |  |
| EMAIL |  | string | Account Holder's email address. |  |
| ADDRESS1 |  | string | Account Holder's primary address. |  |
| ADDRESS2 |  | string | Account Holder's secondary address. |  |
| CITY |  | string | Account Holder's city of residence. |  |
| STATE |  | string | Account Holder's state of residence. Required if configured with [Convenience Fees](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#convenience-fees). |  |
| COUNTRY |  | string | Account Holder's country of residence. |  |
| ZIP |  | string | Account Holder's address postal/zip code. See important note on [Zip Codes](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#Zip-codes). |  |
| PRIMARYPHONE |  | string | Account Holder's primary phone number. |  |
| WORKPHONE |  | string | Account Holder's work phone number. |  |

API Token Sale
--------------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>20.00</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567898</PWINVOICENUMBER>
      <POSINDICATOR>P</POSINDICATOR>
      <PWADJAMOUNT>1.00</PWADJAMOUNT>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <PWMEDIA>CC</PWMEDIA>
      <PWTOKEN>T6767AB4C79CA132</PWTOKEN>
      <STATE>NY</STATE>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <BATCHID>1</BATCHID>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <AUTHCODE>081257</AUTHCODE>
    <AVSCODE>N</AVSCODE>
    <PAYMETH>C</PAYMETH>
    <PWUNIQUEID>120115</PWUNIQUEID>
    <AHNAME>John Doe</AHNAME>
    <PWSALETAX>0.00</PWSALETAX>
    <PWADJAMOUNT>1.00</PWADJAMOUNT>
    <PWSALEAMOUNT>20.00</PWSALEAMOUNT>
    <AMOUNT>21.00</AMOUNT>
    <MACCOUNT>XXXXXXXXXXXX0608</MACCOUNT>
    <EMAIL>test@example.com</EMAIL>
    <CCTYPE>MC</CCTYPE>
    <PWCUSTOMERID>T6767AB4C79CA132</PWCUSTOMERID>
    <PWTOKEN>T6767AB4C79CA132</PWTOKEN>
    <PWCUSTOMID1>23232121321</PWCUSTOMID1>
    <PWCUSTOMID2>f188b4a50a7c3352951a5ba09cc092a6</PWCUSTOMID2>
    <PWINVOICENUMBER>0987654321234567898</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

To process a `SALE` using a token, simply replace the Card or E-Check payment details with the `<PWTOKEN>` parameter returned by the gateway when storing tokens. The same fields as the [API One-Time-Sale](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-one-time-sale) apply: It must be noted that this customer information can be sent as well to overwrite what has been stored.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWTOKEN | ✓ | string | Token returned by the Paywire Gateway. |  |
| PWMEDIA | ✓ | string | Defines the payment method. | Fixed options: `CC` and `ECHECK`. |
| DISABLECF |  | Bool | Overrides applying a Convenience Fee or Cash Discount when set to `TRUE`, if configured. Note that Sales Tax will also be disabled. Default: `FALSE`. | `TRUE` `FALSE` |
| POSINDICATOR |  | string | Used in conjunction with Token Sales to apply Convenience Fees or Cash Discount for periodic payments handled outside Paywire. Submit this in the `TRANSACTIONHEADER` block. | `C`: Regular Token Sale `I`: First Payment of a Periodic Plan `R`: Subsequent Periodic Payment `T`: Last Payment of a Periodic Plan `P`: Periodic Payment |
| PWADJAMOUNT |  | decimal | Adjustment amount. Used to set the Convenience Fee amount to be charged for this transaction. Allowed only when submitted with `POSINDICATOR` set to `P`. Submitting amounts larger than that configured for the merchant will be ignored. | `>0` |
| STATE | (✓) | string | Account Holder's state of residence. Required if configured with [Convenience Fees](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#convenience-fees). |  |

API Remove Token
----------------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>REMOVETOKEN</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>10</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567899</PWINVOICENUMBER>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <PWCTRANSTYPE>3</PWCTRANSTYPE>
      <PWMEDIA>CC</PWMEDIA>
      <PWTOKEN>56A6603A41444122817</PWTOKEN>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <PAYMETH>C</PAYMETH>
    <AMOUNT>0.00</AMOUNT>
    <PWINVOICENUMBER>0987654321234567899</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

To remove a token, submit `REMOVETOKEN` in the `<PWTRANSACTIONTYPE />` parameter along with the token to delete in `<PWTOKEN />`.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `REMOVETOKEN` |
| PWTOKEN | ✓ | string | Token returned by the Paywire Gateway. |  |

API Verification
----------------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>VERIFICATION</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>0.00</PWSALEAMOUNT>
    </TRANSACTIONHEADER>
    <DETAILRECORDS />
   <CUSTOMER>
      <REQUESTTOKEN>FALSE</REQUESTTOKEN>
      <PWMEDIA>CC</PWMEDIA>
      <FIRSTNAME>John</FIRSTNAME>
      <LASTNAME>Doe</LASTNAME>
      <EMAIL>johndoe@email.com</EMAIL>
      <PRIMARYPHONE>7035551212</PRIMARYPHONE>
      <CARDNUMBER>4111111111111111</CARDNUMBER>
      <EXP_MM>12</EXP_MM>
      <EXP_YY>33</EXP_YY>
      <CVV2>123</CVV2>
      <ADDRESS1>1 The Street</ADDRESS1>
      <ZIP>12345</ZIP>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <BATCHID>167792</BATCHID>
    <PWCLIENTID>0000000519</PWCLIENTID>
    <AUTHCODE>09636C</AUTHCODE>
    <AVSCODE>Z</AVSCODE>
    <CVVCODE>M</CVVCODE>
    <PAYMETH>C</PAYMETH>
    <PWUNIQUEID>4771740</PWUNIQUEID>
    <AMOUNT>0.00</AMOUNT>
    <MACCOUNT>XXXXXXXXXXXX8628</MACCOUNT>
    <CCTYPE>VISA</CCTYPE>
    <PWCUSTOMID2>b6d469d28fd33cf5acb627bcf6ca0496</PWCUSTOMID2>
</PAYMENTRESPONSE>
```

A verification transaction will verify the customer's card or bank account before submitting the payment.

### Request Parameters

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `SALE` |
| PWSALEAMOUNT | ✓ | int/decimal | Amount of the transaction. |  |
| PWMEDIA | ✓ | string | Defines the payment method. | Fixed options: `CC` and `ECHECK`. |
| CARDNUMBER | ✓ | int | Card number with which to process the payment. Required only when `CC` is submitted in `PWMEDIA`. |  |
| EXP_MM | ✓ | string | Card expiry month. Required only when `CC` is submitted in `PWMEDIA`. | `2/2, >0, <=12` |
| EXP_YY | ✓ | string | Card expiry year. Required only when `CC` is submitted in `PWMEDIA`. | `2/2` |
| CVV2 | ✓ | int | Card Verification Value. Can only be required when `CC` is submitted in `PWMEDIA`. Requirement is configurable on a merchant-by-merchant basis. | `3/4` |
| CUSTOMERNAME |  | string | Full name of the customer, possibly different than the Account Holder. |  |
| FIRSTNAME |  | string | Account Holder's first name. |  |
| LASTNAME |  | string | Account Holder's last name. |  |
| ADDRESS1 |  | string | Account Holder's primary address. |  |
| ZIP |  | string | Account Holder's address postal/zip code. See important note on [Zip Codes](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#Zip-codes). |  |
| EMAIL |  | string | Account Holder's email address. |  |
| PRIMARYPHONE |  | string | Account Holder's primary phone number. |  |

### Response Parameters

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| RESULT | string | UnionPay transaction result. |  |
| BATCHID | int | Batch number. |  |
| PWCLIENTID | string | Paywire client ID. |  |
| AUTHCODE | string | Authorization code associated with the transaction. |  |
| AVSCODE | string | Transaction AVS code result. Refer to the [AVS Codes table](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#avs-codes) |
| CVVCODE | string | CVV response code. |  |
| PAYMETH | string | Payment method. |  |
| PWUNIQUEID | int | The Paywire Unique ID returned in the Initialize response. | `3` |
| AMOUNT | decimal | Payment amount. |  |
| MACCOUNT | string | Masked credit card number. |  |
| CCTYPE | string | Credit card type. |  |
| PWCUSTOMID2 | string | Client custom ID. |  |

API Split Transaction
---------------------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <PWUSER>{username}</PWUSER>
        <PWPASS>{password}</PWPASS>
        <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>100.00</PWSALEAMOUNT>
        <PWINVOICENUMBER>1234567890</PWINVOICENUMBER>
     </TRANSACTIONHEADER>
     <CUSTOMER>
        <COMPANYNAME>The Company</COMPANYNAME>
        <FIRSTNAME>John</FIRSTNAME>
        <LASTNAME>Doe</LASTNAME>
        <EMAIL>jd@example.com</EMAIL>
        <ADDRESS1>1 The Street</ADDRESS1>
        <CITY>New York</CITY>
        <STATE>NY</STATE>
        <ZIP>12345</ZIP>
        <COUNTRY>US</COUNTRY>
        <PRIMARYPHONE>1234567890</PRIMARYPHONE>
        <WORKPHONE>1234567890</WORKPHONE>
        <PWMEDIA>CC</PWMEDIA>
        <CARDNUMBER>4111111111111111</CARDNUMBER>
        <EXP_MM>02</EXP_MM>
        <EXP_YY>22</EXP_YY>
        <CVV2>123</CVV2>
     </CUSTOMER>
     <SPLITDETAILS>
        <SPLITITEM>
          <PWSPLITID>12340001</PWSPLITID>
          <SPLITAMOUNT>10.00</SPLITAMOUNT>
        </SPLITITEM>
        <SPLITITEM>
          <PWSPLITID>12340002</PWSPLITID>
          <SPLITAMOUNT>20.00</SPLITAMOUNT>
        </SPLITITEM>
        <SPLITITEM>
          <PWSPLITID>12340003</PWSPLITID>
          <SPLITAMOUNT>70.00</SPLITAMOUNT>
        </SPLITITEM>
      </SPLITDETAILS>
  </PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
      <RESULT>APPROVAL</RESULT>
      <BATCHID>167792</BATCHID>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <AUTHCODE>09636C</AUTHCODE>
      <AVSCODE>Z</AVSCODE>
      <CVVCODE>M</CVVCODE>
      <PAYMETH>C</PAYMETH>
      <PWUNIQUEID>4567890</PWUNIQUEID>
      <AMOUNT>100.00</AMOUNT>
      <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
      <CCTYPE>VISA</CCTYPE>
      <PWINVOICENUMBER>1234567890</PWINVOICENUMBER>
  </PAYMENTRESPONSE>
```

Split transaction processing is only valid for qualified merchants.

 To process a `SALE` or `CREDIT` with split transactions: Append the `<SPLITDETAILS>` node in addition to the transaction request.

 The `<SPLITDETAILS>` node may contain one or more `<SPLITITEM>` nodes. Each `<SPLITITEM>` contains the Split ID and the Split Amount.

 The split amount total cannot exceed the original transaction amount. If the split amount total is less than the original transaction amount, the rest of the amount will deposit to the main MID that processed the authorization.

### Request Parameters

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| SPLITDETAILS |  | node | Include this node to process split transactions. Should contain one or more `<SPLITITEM>` nodes. | Only one group of `<SPLITDETAILS>` is allowed. |
| SPLITITEM |  | node | Child node of the `<SPLITDETAILS>` node. | Must contain `<PWSPLITID>` and `<SPLITAMOUNT>`. |
| PWSPLITID | ✓ | string | Parameter of `<SPLITITEM>`. The split ID of the ghost MID. | Must be a valid split ID that is associated with the current merchant. |
| SPLITAMOUNT | ✓ | decimal | Parameter of `<SPLITITEM>`. The split amount for the corresponding ghost MID. | Split amount total should be less than or equal to the original transaction amount. |

API UnionPay
------------

> Initialize Credit Card Transaction Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <PWUSER>{username}</PWUSER>
        <PWPASS>{password}</PWPASS>
        <PWTRANSACTIONTYPE>INITIALIZE</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
        <CURRENCY>USD</CURRENCY>
        <PWINVOICENUMBER>testcup200504001</PWINVOICENUMBER>
        <REQUESTTOKEN>FALSE</REQUESTTOKEN>
     </TRANSACTIONHEADER>
     <CUSTOMER>
     <PWMEDIA>CC</PWMEDIA>
     <CARDNUMBER>6222821234560017</CARDNUMBER>
     <EXP_MM>12</EXP_MM>
     <EXP_YY>33</EXP_YY>
     <PRIMARYPHONE>86-13012345678</PRIMARYPHONE>
     <FIRSTNAME>Sample</FIRSTNAME>
     <LASTNAME>Name</LASTNAME>
     </CUSTOMER>
  </PAYMENTREQUEST>
```

> Initialize Credit Card Transaction Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
      <RESULT>APPROVAL</RESULT>
      <BATCHID>641</BATCHID>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PAYMETH>C</PAYMETH>
      <PWUNIQUEID>2314876</PWUNIQUEID>
      <AHNAME>Sample Name</AHNAME>
      <AMOUNT>10.00</AMOUNT>
      <MACCOUNT>XXXXXXXXXXXX0017</MACCOUNT>
      <CCTYPE>CUP</CCTYPE>
      <PWCUSTOMID2>000000189897</PWCUSTOMID2>
      <PWINVOICENUMBER>testcup200504001</PWINVOICENUMBER>
      <ISDEBIT>FALSE</ISDEBIT>
      <CURRENCY>USD</CURRENCY>
  </PAYMENTRESPONSE>
```

> Finalize Credit Card Transaction Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <PWUSER>{username}</PWUSER>
        <PWPASS>{password}</PWPASS>
        <PWTRANSACTIONTYPE>FINALIZE</PWTRANSACTIONTYPE>
        <PWINVOICENUMBER>test_002</PWINVOICENUMBER>
     </TRANSACTIONHEADER>
     <CUSTOMER>
     <PWUNIQUEID>189897</PWUNIQUEID>
     <SECURECODE>111111</SECURECODE>
     <CVV2>123</CVV2>
     </CUSTOMER>
  </PAYMENTREQUEST>
```

> Finalize Credit Card Transaction Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
    <PAYMENTRESPONSE>
        <RESULT>APPROVAL</RESULT>
        <BATCHID>641</BATCHID>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <AUTHCODE>123456</AUTHCODE>
        <CVVCODE> </CVVCODE>
        <PAYMETH>C</PAYMETH>
        <PWUNIQUEID>189897</PWUNIQUEID>
        <AMOUNT>10.00</AMOUNT>
        <MACCOUNT>XXXXXXXXXXXX0017</MACCOUNT>
        <CCTYPE>CUP</CCTYPE>
        <PWTOKEN>XXXXXXXXXXXXXXXXXXXX</PWTOKEN>
        <PWCUSTOMID2>00000023123456</PWCUSTOMID2>
        <PWINVOICENUMBER>test_002</PWINVOICENUMBER>
        <CURRENCY>USD</CURRENCY>
    </PAYMENTRESPONSE>
```

> Initialize Debit Card Transaction Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <PWUSER>{username}</PWUSER>
        <PWPASS>{password}</PWPASS>
        <PWTRANSACTIONTYPE>INITIALIZE</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
        <CURRENCY>USD</CURRENCY>
        <PWINVOICENUMBER>testcup200504005</PWINVOICENUMBER>
        <REQUESTTOKEN>FALSE</REQUESTTOKEN>
     </TRANSACTIONHEADER>
     <CUSTOMER>
     <PWMEDIA>CC</PWMEDIA>
     <CARDNUMBER>6250946000000016</CARDNUMBER>
     <EXP_MM>12</EXP_MM>
     <EXP_YY>33</EXP_YY>
     <PRIMARYPHONE>852-11112222</PRIMARYPHONE>
     <FIRSTNAME>Sample</FIRSTNAME>
     <LASTNAME>Name</LASTNAME>
     </CUSTOMER>
  </PAYMENTREQUEST>
```

> Initialize Debit Card Transaction Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
      <RESULT>APPROVAL</RESULT>
      <BATCHID>641</BATCHID>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PAYMETH>C</PAYMETH>
      <PWUNIQUEID>189899</PWUNIQUEID>
      <AHNAME>Sample Name</AHNAME>
      <AMOUNT>10.00</AMOUNT>
      <MACCOUNT>XXXXXXXXXXXX0016</MACCOUNT>
      <CCTYPE>CUP</CCTYPE>
      <PWCUSTOMID2>000000223154</PWCUSTOMID2>
      <PWINVOICENUMBER>testcup200504005</PWINVOICENUMBER>
      <ISDEBIT>TRUE</ISDEBIT>
      <CURRENCY>USD</CURRENCY>
  </PAYMENTRESPONSE>
```

> Finalize Debit Card Request Transaction Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <PWUSER>{username}</PWUSER>
        <PWPASS>{password}</PWPASS>
        <PWTRANSACTIONTYPE>FINALIZE</PWTRANSACTIONTYPE>
     </TRANSACTIONHEADER>
     <CUSTOMER>
     <PWUNIQUEID>189899</PWUNIQUEID>
     <SECURECODE>111111</SECURECODE>
     </CUSTOMER>
  </PAYMENTREQUEST>
```

> Finalize Debit Card Transaction Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
      <RESULT>APPROVAL</RESULT>
      <BATCHID>641</BATCHID>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <AUTHCODE>123456</AUTHCODE>
      <CVVCODE> </CVVCODE>
      <PAYMETH>C</PAYMETH>
      <PWUNIQUEID>189899</PWUNIQUEID>
      <AHNAME>Sample Name</AHNAME>
      <AMOUNT>10.00</AMOUNT>
      <MACCOUNT>XXXXXXXXXXXX0016</MACCOUNT>
      <CCTYPE>CUP</CCTYPE>
      <PWTOKEN>XXXXXXXXXXXXXXXXXXXX</PWTOKEN>
      <PWCUSTOMID2>000000223154</PWCUSTOMID2>
      <PWINVOICENUMBER>20210170128760201</PWINVOICENUMBER>
      <CURRENCY>USD</CURRENCY>
  </PAYMENTRESPONSE>
```

> Non-Secure Plus Token Sale Request Example:
> 
>  Use the Token and PWUNIQUEID from the above finalize transaction response
> 
>  Debit card is not allowed to process Non-SecurePlus payments. Union Pay will return decline in prod for debit cards.

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <PWUSER>{username}</PWUSER>
        <PWPASS>{password}</PWPASS>
        <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
        <PWINVOICENUMBER>testcup200504002</PWINVOICENUMBER>
     </TRANSACTIONHEADER>
     <CUSTOMER>
     <POSINDICATOR>R</POSINDICATOR>
     <PWUNIQUEID>192824</PWUNIQUEID>
     <PWMEDIA>CC</PWMEDIA>
     <PWTOKEN>3DD22D8DF80A4FB57360</PWTOKEN>
     </CUSTOMER>
  </PAYMENTREQUEST>
```

> Non-Secure Plus Token Sale Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
      <RESULT>APPROVAL</RESULT>
      <BATCHID>691</BATCHID>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <AUTHCODE></AUTHCODE>
      <CVVCODE> </CVVCODE>
      <PAYMETH>C</PAYMETH>
      <PWUNIQUEID>192824</PWUNIQUEID>
      <AMOUNT>10.00</AMOUNT>
      <MACCOUNT>XXXXXXXXXXXX0016</MACCOUNT>
      <CCTYPE>CUP</CCTYPE>
      <PWTOKEN>3DD22D8DF80A4FB57360</PWTOKEN>
      <PWCUSTOMID2>000000223154</PWCUSTOMID2>
      <PWINVOICENUMBER>testcup200504002</PWINVOICENUMBER>
      <CURRENCY>USD</CURRENCY>
  </PAYMENTRESPONSE>
```

Sending a UnionPay payment transaction to the gateway is a two-step process using two distinct transaction types consecutively: INITIALIZE and FINALIZE.

INITIALIZE - Initiate the SMS authentication request. The developer needs to pass the card number, expiration date and the phone number via the tags below. In the INITIALIZE response, there will be an additional field: ISDEBIT=TRUE/FALSE that indicates if the card is a credit or debit card.

FINALIZE - Complete the CUP transaction with the unique ID from the INITIALIZE response. The SMS Code sent to the customer's mobile phone needs to be passed via the SECURECODE tag. If the card is a credit card (read from the INITIALIZE response), then the CVV2 is mandatory.

UnionPay also supports two other transaction types: VOID and CREDIT for voiding transactions prior to settlement or refunding transactions after settlement. The syntax for these transaction types is the same as specified elsewhere in this document.

### Initialize Request Parameters

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `INITIALIZE` |
| PWSALEAMOUNT | ✓ | int/decimal | Set to `0`, otherwise a `SALE` is processed. |  |
| PWINVOICENUMBER |  | string | The merchant's unique invoice number associated with this transaction. |  |
| CARDNUMBER | ✓ | int | Credit card number. |  |
| EXP_MM | ✓ | string | Card expiry month. | `2/2, >0, <=12` |
| EXP_YY | ✓ | string | Card expiry year. | `2/2` |
| PRIMARYPHONE | ✓ | string | Cell phone number to send SMS code. |  |
| FIRSTNAME |  | string | Account Holder's first name. |  |
| LASTNAME |  | string | Account Holder's last name. |  |
| ADDRESS1 |  | string | Account Holder's primary address. |  |
| ADDRESS2 |  | string | Account Holder's secondary address. |  |
| CITY |  | string | Account Holder's city of residence. |  |
| STATE |  | string | Account Holder's state or province of residence. |  |
| COUNTRY |  | string | Account Holder's country of residence. |  |
| ZIP |  | string | Account Holder's address postal/zip code. See important note on [Zip Codes](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#Zip-codes). |  |
| EMAIL |  | string | Account Holder's email address. |  |

### Initialize Response Parameters

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| RESULT | string | UnionPay transaction result. |  |
| BATCHID | int | Batch number. |  |
| PWCLIENTID | string | Paywire client ID. |  |
| PAYMETH | string | Payment method. |  |
| PWUNIQUEID | int | The Paywire Unique ID returned in the Initialize response. | `3` |
| AHNAME | string | The account holder's name that was supplied. |  |
| AMOUNT | decimal | Payment amount. |  |
| MACCOUNT | string | Masked credit card number. |  |
| CCTYPE | string | Credit card type. |  |
| PWCUSTOMID2 | string | Client custom ID. |  |
| PWINVOICENUMBER | string | Client custom invoice number. |  |
| PWINVOICENUMBER | string | Client custom invoice number. |  |
| ISDEBIT | Bool | Indicate if the card is a debit or credit card. | `TRUE`/`FALSE` |  |
| CURRENCY | string | Set the transaction currency. | `USD`/`CNY`/`EUR` |

### Finalize Request Parameters

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWINVOICENUMBER |  | string | Client custom invoice number. |  |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `FINALIZE` |
| PWUNIQUEID | ✓ | int | The Paywire Unique ID returned in the Initialize response. | `3` |
| SECURECODE | ✓ | int | The SMS code returned in the Initialize response. | `3` |
| CVV2 | ✓ | int | Only required for credit cards, not debit cards. | `3` |

### Finalize Response Parameters

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| RESULT | string | UnionPay transaction result. |  |
| BATCHID | int | Batch number. |  |
| PWCLIENTID | string | Paywire client ID. |  |
| AUTHCODE | string | Authorization code associated with the transaction. |  |
| CVVCODE | string | CVV response code. |  |
| PAYMETH | string | Payment method. |  |
| PWUNIQUEID | int | The Paywire Unique ID returned in the Initialize response. | `3` |
| AHNAME | string | The account holder's name that was supplied. |  |
| AMOUNT | decimal | Payment amount. |  |
| MACCOUNT | string | Masked credit card number. |  |
| CCTYPE | string | Credit card type. |  |
| PWTOKEN | string | Token returned by the Paywire Gateway. |  |
| PWCUSTOMID2 | string | Client custom ID. |  |
| PWINVOICENUMBER | string | Client custom invoice number. |  |
| CURRENCY | string | Set the transaction currency. | `USD`/`CNY`/`EUR` |

UnionPay Test Cards:

Credit: 

 6222821234560017 , Phone Number: 86-13012345678 

 Debit:

 6250946000000016 , Phone Number: 852-11112222 

 Exp: 12/33 

 CVV: 123 

 SMS Code: 111111

API Brazil
----------

> Brazil Credit Card Sale Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWUSER>{username}</PWUSER>
        <PWPASS>{password}</PWPASS>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10</PWSALEAMOUNT>
        <PWINVOICENUMBER>test12345</PWINVOICENUMBER>
     </TRANSACTIONHEADER>
     <CUSTOMER>
        <REQUESTTOKEN>FALSE</REQUESTTOKEN>
        <PWMEDIA>CC</PWMEDIA>
        <FIRSTNAME>Silva</FIRSTNAME>
        <LASTNAME>Gonsalves</LASTNAME>
        <CARDNUMBER>4111111111111111</CARDNUMBER>
        <EXP_MM>12</EXP_MM>
        <EXP_YY>30</EXP_YY>
        <CVV2>699</CVV2>
        <TOTALINSTALLMENTS>1</TOTALINSTALLMENTS>
        <EMAIL>c41077766698466611827@sandbox.pagseguro.com.br</EMAIL>
     </CUSTOMER>
     <SECUREDATA>
        <CUSTOMERIP>177.69.0.82</CUSTOMERIP>
        <SELLERCHANNEL>web</SELLERCHANNEL>
        <PRODUCTSCATEGORY>Equipamentos de Esporte</PRODUCTSCATEGORY>
        <GENDER>F</GENDER>
        <CUSTOMERLOGIN>F</CUSTOMERLOGIN>
        <CUSTOMERNAME>Pedro</CUSTOMERNAME>
        <DOCUMENTTYPE>CPF</DOCUMENTTYPE>
        <DOCUMENT>09060697006</DOCUMENT>
        <BIRTHDATE>1986-04-05</BIRTHDATE>
        <FINGERPRINT>1573661233</FINGERPRINT>
     </SECUREDATA>
     <BILLINGINFO>
        <BILLINGDDD>32</BILLINGDDD>
        <BILLINGPHONE>962633862</BILLINGPHONE>
        <BILLINGSTREET>Rua Santa Mônica</BILLINGSTREET>
        <BILLINGNUMBER>281</BILLINGNUMBER>
        <BILLINGNEIGHBORHOOD>Parque Industrial San José</BILLINGNEIGHBORHOOD>
        <BILLINGCITY>Cotia</BILLINGCITY>
        <BILLINGSTATE>SP</BILLINGSTATE>
        <BILLINGZIPCODE>06715825</BILLINGZIPCODE>
        <BILLINGCOUNTRY>Brasil</BILLINGCOUNTRY>
     </BILLINGINFO>
     <PRODUCTLIST>
      <PRODUCT>
        <PRODUCTNAME>Produtox</PRODUCTNAME>
        <PRODUCTPRICE>10.00</PRODUCTPRICE>
        <PRODUCTQTY>1</PRODUCTQTY>
        <PRODUCTCODE>1234a56</PRODUCTCODE>
      </PRODUCT>
     </PRODUCTLIST>
  </PAYMENTREQUEST>
```

> Brazil Credit Card Sale Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
      <PAYMENTRESPONSE>
          <RESULT>APPROVAL</RESULT>
          <BATCHID>73589</BATCHID>
          <PWCLIENTID>0000000540</PWCLIENTID>
          <AUTHCODE>770323</AUTHCODE>
          <PAYMETH>C</PAYMETH>
          <PWUNIQUEID>4770323</PWUNIQUEID>
          <AMOUNT>10.00</AMOUNT>
          <MACCOUNT>XXXXXXXXXXXX8390</MACCOUNT>
          <CCTYPE>MC</CCTYPE>
          <PWTOKEN>DFED3F81124941526978</PWTOKEN>
          <PWCUSTOMID2>15919932357584290</PWCUSTOMID2>
          <PWINVOICENUMBER>test12131</PWINVOICENUMBER>
      </PAYMENTRESPONSE>
```

> Brazil Boleto Sale Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWUSER>{username}</PWUSER>
        <PWPASS>{password}</PWPASS>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <PWTRANSACTIONTYPE>BOLETO</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10</PWSALEAMOUNT>
     </TRANSACTIONHEADER>
     <BOLETOREQUEST>
     <BR_EXPDATE>2020-12-06</BR_EXPDATE>
     <BR_CALLBACKURL>www.sample.com</BR_CALLBACKURL>
     <BR_INSTRUCTIONS>Sample Boleto Instruction</BR_INSTRUCTIONS>
     <BR_EMAIL>Sample.customer@test.com</BR_EMAIL>
     <BR_CUSTOMERNAME>Sample Customer</BR_CUSTOMERNAME>
     <BR_DOCUMENT>023.472.201-01</BR_DOCUMENT>
     <BR_ADDRESS_NUMBER>123</BR_ADDRESS_NUMBER>
     <BR_ADDRESS_STREET>Test Street</BR_ADDRESS_STREET>
     <BR_ADDRESS_COMPLEMENT>Apt 456</BR_ADDRESS_COMPLEMENT>
     <BR_ADDRESS_NEIGHBORHOOD>Jardins</BR_ADDRESS_NEIGHBORHOOD>
     <BR_ADDRESS_CITY>Sao Paulo</BR_ADDRESS_CITY>
     <BR_ADDRESS_STATE>SP</BR_ADDRESS_STATE>
     <BR_ADDRESS_ZIPCODE>06743725</BR_ADDRESS_ZIPCODE>
     <BR_ADDRESS_COUNTRY>Brazil</BR_ADDRESS_COUNTRY>
     <BR_SPLITMODE>FALSE</BR_SPLITMODE>
     </BOLETOREQUEST>
  </PAYMENTREQUEST>
```

> Brazil Boleto Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
      <RESULT>APPROVAL</RESULT>
      <BATCHID>73589</BATCHID>
      <PWCLIENTID>0000000540</PWCLIENTID>
      <AUTHCODE>770323</AUTHCODE>
      <PAYMETH>C</PAYMETH>
      <PWUNIQUEID>4770323</PWUNIQUEID>
      <AMOUNT>10.00</AMOUNT>
      <CCTYPE>BOLETO</CCTYPE>
      <PWCUSTOMID2>15919933554093388</PWCUSTOMID2>
      <PWINVOICENUMBER>20164162228202123</PWINVOICENUMBER>
      <BR_BARCODE>00099828700000010009999250040933880799999990</BR_BARCODE>
      <BR_DIGITABLELINE>00099.99921 50040.933884 07999.999902 9 82870000001000</BR_DIGITABLELINE>
      <BR_URL>https://transactionsandbox.pagador.com.br/post/pagador/reenvia.asp/93fa2074-3cfc-4d17-b490-2daf19a65367</BR_URL>
  </PAYMENTRESPONSE>
```

> Brazil Boleto Split Mode - Step 1 - Get Boleto Cost Example:

```
<?xml version="1.0" encoding="UTF-8"?>
    <PAYMENTREQUEST>
       <TRANSACTIONHEADER>
          <PWVERSION>3</PWVERSION>
          <PWUSER>{username}</PWUSER>
          <PWPASS>{password}</PWPASS>
          <PWCLIENTID>{clientId}</PWCLIENTID>
          <PWKEY>{key}</PWKEY>
          <PWTRANSACTIONTYPE>GETBOLETOFEE</PWTRANSACTIONTYPE>
       </TRANSACTIONHEADER>
       </PAYMENTREQUEST>
```

> Brazil Boleto Split Mode - Step 1 - Boleto Cost Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
      <RESULT>APPROVAL</RESULT>
      <PWCLIENTID>0000000540</PWCLIENTID>
      <PAYMETH>C</PAYMETH>
      <AMOUNT>1.80</AMOUNT>
      <PWINVOICENUMBER>20164162228202123</PWINVOICENUMBER>
  </PAYMENTRESPONSE>
```

> Brazil Boleto Split Mode - Step 2 - Send Boleto Split Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWUSER>{username}</PWUSER>
        <PWPASS>{password}</PWPASS>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <PWTRANSACTIONTYPE>BOLETO</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10</PWSALEAMOUNT>
     </TRANSACTIONHEADER>
     <BOLETOREQUEST>
     <BR_EXPDATE>2020-12-06</BR_EXPDATE>
     <BR_CALLBACKURL>www.sample.com</BR_CALLBACKURL>
     <BR_INSTRUCTIONS>Sample Boleto Instruction</BR_INSTRUCTIONS>
     <BR_EMAIL>Sample.customer@test.com</BR_EMAIL>
     <BR_CUSTOMERNAME>Sample Customer</BR_CUSTOMERNAME>
     <BR_DOCUMENT>023.472.201-01</BR_DOCUMENT>
     <BR_ADDRESS_NUMBER>123</BR_ADDRESS_NUMBER>
     <BR_ADDRESS_STREET>Test Street</BR_ADDRESS_STREET>
     <BR_ADDRESS_COMPLEMENT>Apt 456</BR_ADDRESS_COMPLEMENT>
     <BR_ADDRESS_NEIGHBORHOOD>Jardins</BR_ADDRESS_NEIGHBORHOOD>
     <BR_ADDRESS_CITY>Sao Paulo</BR_ADDRESS_CITY>
     <BR_ADDRESS_STATE>SP</BR_ADDRESS_STATE>
     <BR_ADDRESS_ZIPCODE>06743725</BR_ADDRESS_ZIPCODE>
     <BR_ADDRESS_COUNTRY>Brazil</BR_ADDRESS_COUNTRY>
     <BR_SPLITMODE>TRUE</BR_SPLITMODE>
     <BR_SPLITS>
     <BR_SPLIT_ITEM>
     <BR_SPLITS_DOCUMENT>843.345.449-83</BR_SPLITS_DOCUMENT>
     <BR_SPLITS_VALUE>5</BR_SPLITS_VALUE>
     </BR_SPLIT_ITEM>
     <BR_SPLIT_ITEM>
     <BR_SPLITS_DOCUMENT>885.870.260-38</BR_SPLITS_DOCUMENT>
     <BR_SPLITS_VALUE>3.20</BR_SPLITS_VALUE>
     </BR_SPLIT_ITEM>
     </BR_SPLITS>
     </BOLETOREQUEST>
  </PAYMENTREQUEST>
```

> Brazil Boleto Split Mode - Step 2 - Get Boleto Split Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
      <RESULT>APPROVAL</RESULT>
      <BATCHID>73589</BATCHID>
      <PWCLIENTID>0000000540</PWCLIENTID>
      <AUTHCODE>770324</AUTHCODE>
      <PAYMETH>C</PAYMETH>
      <PWUNIQUEID>4770323</PWUNIQUEID>
      <AMOUNT>10.00</AMOUNT>
      <CCTYPE>BOLETO</CCTYPE>
      <PWCUSTOMID2>15919933554093388</PWCUSTOMID2>
      <PWINVOICENUMBER>20164162228202123</PWINVOICENUMBER>
      <BR_BARCODE>00099828700000010009999250040933880799999990</BR_BARCODE>
      <BR_DIGITABLELINE>00099.99921 50040.933884 07999.999902 9 82870000001000</BR_DIGITABLELINE>
      <BR_URL>https://transactionsandbox.pagador.com.br/post/pagador/reenvia.asp/93fa2074-3cfc-4d17-b490-2daf19a65367</BR_URL>
  </PAYMENTRESPONSE>
```

API Brazil has three transactions: Credit Card Sale, Brazil Boleto Sale and Brazil Boleto Split Mode, The Brazil Boleto Sale and Brazil Boleto Split Mode transactions are associated with Boleto. Boleto is basically a paper payment invoice that a customer carries into a bank and pays. The bank routes the customer's payment back to the merchant.

BOLETO issues a Boleto to a customer which basically redirects the customer to a link that displays the Boleto for printing. GETBOLETOFEE gets the fee associated with a Boleto that the customer pays.

The Paywire gateway can also process regular credit card transactions as demonstrated the examples provided here.

Probably the most important field associated with Brazil processing for all payment types is BR_DOCUMENT which is the Brazilian equivalent of the US Social Security number - the CPF. This field is required for any transactions associted with settled funds which are to be transferred out of Brazil.

Below are some test CPF account numbers:

*   023.472.201-01
*   242.189.959-15
*   843.345.449-83
*   885.870.260-38

### Brazil Credit Sale Parameters

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| CUSTOMER | ✓ | Object | This is the existing customer data group same with all other payment types. |  |
| TOTALINSTALLMENTS |  | Int | The number of installments when the payment is initiated by the merchant. | Must greater than 0 if this tag exists. |
| SECUREDATA | ✓ | object | This group is used to verify customer identity for fraud prevention. |  |
| CUSTOMERIP | ✓ | string | The IP address of the customer. | Must be a valid IPv4 address in the current version. |
| SELLERCHANNEL | ✓ | enum | The sale channel of the transaction. | `Call Center`: purchases made over the phone. `Web`: purchases made by web. `Portal`: purchases from web portals. `Quiosque`: purchases from retail stores. `Movel`: purchases made by mobile devices. |
| PRODUCTSCATEGORY | ✓ | enum | The product category. | `Animais e Bichos de Estimação`: Pets `Roupas e Acessórios`: Clothing and Accessories `Negócios e Indústria`: Business and Industry `Câmeras e Óticas`: Cameras and Optics `Eletrônicos`: Eletronics `Comidas, Bebidas e Cigarro`: Food, Drinks and Cigars `Móveis`: Furniture `Ferramentas`: Tools `Saúde e Beleza`: Health and Beauty `Casa e Jardim`: House and Garden `Malas e Bagagens`: Bags and Luggage’s `Adulto`: Adult `Armas e Munição`: Weapons and Ammunition `Materiais de Escritório`: Office Supplies `Religião e Cerimoniais`: Religion and Ceremonial `Software`: Software `Equipamentos de Esporte`: Sports Equipment `Brinquedos e Jogos`: Toys and Games `Veículos e Peças`: and Parts `Livros`: Books `DVDs e Vídeos`: DVDs and Videos `Revistas e Jornais`: Magazines and Newspapers `Música`: Music `Outras Categorias Não Especificada`: Others Categories Not specified |
| GENDER | ✓ | enum | Customer gender | `M`: Male `F`: Female |
| CUSTOMERLOGIN | ✓ | string | The customer's login username from the merchant's payment system. | Length 1 ~ 100 |
| CUSTOMERNAME | ✓ | string | The customer's full name. | Length 1 ~ 34 |
| DOCUMENTTYPE | ✓ | enum | The document type. | CPF or CNPJ |
| DOCUMENT | ✓ | string | The document number. | Length 11~14 |
| BIRTHDATE | ✓ | string | The customer's birth date. | Date format: `YYYY-MM-DD` |
| FINGERPRINT | ✓ | string | The information to identify the customer device, generated by integrating with the 3rd party fraud prevention systems. | Length 1 ~ 100 |
| BILLINGINFO | ✓ | object | The billing info of the customer. |  |
| BILLINGDDD | ✓ | string | The 2-digit Brazil area code for the phone number. | Always 2 digits, 00 is not accepted. |
| BILLINGPHONE | ✓ | string | The billing phone number. | Length 8 ~ 11 |
| BILLINGSTREET | ✓ | string | The billing address street. | Length 1 ~ 70 |
| BILLINGNUMBER | ✓ | string | The billing address number. | Length 1 ~ 10 |
| BILLINGNEIGHBORHOOD | ✓ | string | The billing address neighborhood. | Length 1 ~ 50 |
| BILLINGCITY | ✓ | string | The billing address city. | Length 1 ~ 50 |
| BILLINGSTATE | ✓ | string | The billing address state/district. | 2 digit state code, for example, SP (São Paulo). |
| BILLINGZIPCODE | ✓ | string | The billing address zip code. | 8 digit zip code, only numbers accepted. |
| BILLINGCOUNTRY | ✓ | string | The billing address country | Length 1 ~ 35 |
| SHIPPINGINFO |  | object | The shipping info of the customer, if not applicable, the entire SHIPPINGINFO group should not appear in the request message. If SHIPPINGINFO is attached in the request, all data elements are required. |  |
| SHIPPINGNAME | ✓ | string | The recipient's name. | Length 1 ~ 34 |
| SHIPPINGMETHOD | ✓ | enum | The shipping method. | `SameDay`: delivery at the same day `NextDay`:delivery at the next day `TwoDay`: delivery in 2 days `ThreeDay`: delivery in 3 days `LowCost`: Low cost delivery `Pickup`: receive in the store `Other`: other `None`: services or signature |
| SHIPPINGDDD | ✓ | string | The 2-digit Brazil area code for the phone number. | Always 2 digits, 00 is not accepted. |
| SHIPPINGPHONE | ✓ | string | The shipping phone number. | Length 8 ~ 11 |
| SHIPPINGSTREET | ✓ | string | The shipping address street. | Length 1 ~ 70 |
| SHIPPINGNUMBER | ✓ | string | The shipping address number. | Length 1 ~ 10 |
| SHIPPINGNEIGHBORHOOD | ✓ | string | The shipping address neighborhood. | Length 1 ~ 50 |
| SHIPPINGCITY | ✓ | string | The shipping address city. | Length 1 ~ 50 |
| SHIPPINGSTATE | ✓ | string | The shipping address state/district. | 2 digit state code, for example, SP (São Paulo). |
| SHIPPINGZIPCODE | ✓ | string | The shipping address zip code. | 8 digit zip code, only numbers accepted. |
| SHIPPINGCOUNTRY | ✓ | string | The shipping address country. | Length 1 ~ 35 |
| PRODUCTLIST | ✓ | object | The product details, can contain multiple PRODUCT items in the list. |  |
| PRODUCT | ✓ | object | The product item object. |  |
| PRODUCTNAME | ✓ | string | The product name. | Length 4 ~ 100 |
| PRODUCTPRICE | ✓ | decimal | The product unit price. | For example: 10.00 |
| PRODUCTQTY | ✓ | decimal | The product quantity. |  |
| PRODUCTCODE | ✓ | string | The product code or SKU. | Length 1 ~ 100 |

### Boleto-Specific Request Parameters

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| BR_EXPDATE | ✓ | string | Last date that this document could be accepted by the bank system: Format: yyyy-MM-dd. |  |
| BR_CALLBACKURL | ✓ | string | Call back URL from the merchant. |  |
| BR_INSTRUCTIONS | ✓ | string | Instructions for the bank and customer to be included in the bank slip. |  |
| BR_EMAIL | ✓ | string | Email for sending the bank slip. |  |
| BR_CUSTOMERNAME | ✓ | string | Credit card number. |  |
| BR_DOCUMENT | ✓ | string | Document number. For Brazilians, the expected document will be C.P.F. or CNPJ - Data format can be 023.472.201-01 or 02347220101. |  |
| BR_ADDRESS_STREET | ✓ | string |  |  |
| BR_ADDRESS_NUMBER | ✓ | string |  |  |
| BR_ADDRESS_COMPLEMENT | ✓ | string |  |  |
| BR_ADDRESS_ZIPCODE | ✓ | string |  |  |
| BR_ADDRESS_NEIGHBORHOOD | ✓ | string |  |  |
| BR_ADDRESS_CITY | ✓ | string |  |  |
| BR_ADDRESS_STATE | ✓ | string |  |  |
| BR_ADDRESS_COUNTRY | ✓ | string |  |  |
| BR_SPLITMODE | ✓ | string |  |  |
| BR_SPLITS |  | string |  |  |
| BR_SPLIT_ITEM |  | string |  |  |
| BR_SPLITS_DOCUMENT |  | string |  |  |
| BR_SPLITS_VALUE |  | decimal |  |  |

### Boleto-Specific Response Parameters

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| BR_BARCODE | String | Barcode string. |  |  |
| BR_DIGITABLELINE | String | Digitable line. |  |  |
| BR_URL | String | Boleto download URL. |  |  |

### Boleto Split Mode Request Parameters

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| BR_EXPDATE | ✓ | string | Last date that this document could be accepted by the bank system, format: yyyy-MM-dd. |  |
| BR_CALLBACKURL | ✓ | string | Call back URL from the merchant. |  |
| BR_INSTRUCTIONS | ✓ | string | Instructions for the bank and customer to be included in the bank slip. |  |
| BR_EMAIL | ✓ | string | Email for sending the bank slip. |  |
| BR_CUSTOMERNAME | ✓ | string | Credit card number. |  |
| BR_DOCUMENT | ✓ | string | Document number. For Brazilians, the expected document will be C.P.F. or CNPJ - Data format can be 023.472.201-01 or 02347220101. |  |
| BR_ADDRESS_STREET | ✓ | string |  |  |
| BR_ADDRESS_NUMBER | ✓ | string |  |  |
| BR_ADDRESS_COMPLEMENT | ✓ | string |  |  |
| BR_ADDRESS_ZIPCODE | ✓ | string |  |  |
| BR_ADDRESS_NEIGHBORHOOD | ✓ | string |  |  |
| BR_ADDRESS_CITY | ✓ | string |  |  |
| BR_ADDRESS_STATE | ✓ | string |  |  |
| BR_ADDRESS_COUNTRY | ✓ | string |  |  |
| BR_SPLITMODE | ✓ | string |  |  |
| BR_SPLITS |  | string |  |  |
| BR_SPLIT_ITEM |  | string |  |  |
| BR_SPLITS_DOCUMENT |  | string |  |  |
| BR_SPLITS_VALUE |  | decimal |  |  |

### Boleto Split Mode Response Parameters

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| BR_BARCODE | string | Barcode string. |  |  |
| BR_DIGITABLELINE | string | Digitable line. |  |  |
| BR_URL | string | Boleto download URL. |  |  |

API Batch Inquiry
-----------------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>BATCHINQUIRY</PWTRANSACTIONTYPE>
      <PWINVOICENUMBER>0987654321234567900</PWINVOICENUMBER>
   </TRANSACTIONHEADER>
   <CUSTOMER />
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>SUCCESS</RESULT>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <BATCHID>1</BATCHID>
    <SALESTOTAL>109.31</SALESTOTAL>
    <SALESRECS>10</SALESRECS>
    <CREDITAMT>0.00</CREDITAMT>
    <CREDITRECS>0</CREDITRECS>
    <NETCRAMT>109.31</NETCRAMT>
    <NETCRRECS>10</NETCRRECS>
    <VOIDAMT>2.25</VOIDAMT>
    <VOIDRECS>4</VOIDRECS>
    <PWINVOICENUMBER>0987654321234567900</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

To get information on the open batch, submit `BATCHINQUIRY` in the `<PWTRANSACTIONTYPE />` parameter. This will return summary information.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `BATCHINQUIRY` |

API Close Batch
---------------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>CLOSE</PWTRANSACTIONTYPE>
      <PWINVOICENUMBER>0987654321234567901</PWINVOICENUMBER>
   </TRANSACTIONHEADER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>SUCCESS</RESULT>
    <BATCHID>1</BATCHID>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <SALESTOTAL>109.31</SALESTOTAL>
    <SALESRECS>10</SALESRECS>
    <CREDITAMT>0.00</CREDITAMT>
    <CREDITRECS>0</CREDITRECS>
    <NETCRAMT>109.31</NETCRAMT>
    <NETCRRECS>10</NETCRRECS>
    <VOIDAMT>2.25</VOIDAMT>
    <VOIDRECS>4</VOIDRECS>
    <PWINVOICENUMBER>0987654321234567901</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

To close an open batch for settlement, submit `CLOSE` in the `<PWTRANSACTIONTYPE />` parameter.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `CLOSE` |

API Search Transactions
-----------------------

### Request Parameters

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>SEARCHTRANS</PWTRANSACTIONTYPE>
      <XOPTION>TRUE</XOPTION>
   </TRANSACTIONHEADER>
   <SEARCHCONDITION>
      <COND_DATEFROM>2018-10-01 00:00</COND_DATEFROM>
      <COND_DATETO>2018-10-26 14:00</COND_DATETO>
      <COND_PWCID />
      <COND_PWCUSTOMID2 />
      <COND_UNIQUEID />
      <COND_BATCHID />
      <COND_TRANSAMT />
      <COND_TRANSTYPE>ALL</COND_TRANSTYPE>
      <COND_RESULT>ALL</COND_RESULT>
      <COND_CARDTYPE>ALL</COND_CARDTYPE>
   </SEARCHCONDITION>
</PAYMENTREQUEST>
```

To search transactions within the gateway, submit `SEARCHTRANS` in the `<PWTRANSACTIONTYPE />` parameter along with the desired `<SEARCHCONDITION />` values.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `SEARCHTRANS` |
| XOPTION |  | Bool | Show the search result in XML or escaped XML. | Options: `TRUE` or `FALSE` |
| COND_DATEFROM |  | DateTime | Search date range from. | Date Format `yyyy-mm-dd HH:MM`. |
| COND_DATETO |  | DateTime | Search date range to. * The time range must be limited within one month, otherwise system will only pull 100 records. | Date Format `yyyy-mm-dd HH:MM`. |
| COND_PWCID |  | string | Paywire Customer Identifier. When submitted, the created token will be associated with this customer. |
| COND_PWCUSTOMID2 |  | string | Processor-specific transaction unique identifier. Not always applicable. |
| COND_USERNAME |  | String | Search by the USERNAME initiating the transaction. |  |
| COND_UNIQUEID |  | int | Search by transaction Unique ID returned by the gateway. |  |
| COND_BATCHID |  | string | Search by Batch ID returned by the gateway. |  |
| COND_TRANSAMT |  | int/decimal | Search by transaction amount. |  |
| COND_TRANSTYPE |  | string | Search by transaction type. | Fixed options: `ALL`, `SALE`, `CREDIT`, `VOID` |
| COND_RESULT |  | string | Search by transaction result returned by the gateway. | See [Transaction Result values](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#transaction-result-values). |
| COND_CARDTYPE |  | string | Search by the card type used for the transaction. | Fixed options: `ALL`, `VISA`, `MC`, `DISC`, `AMEX`, `ACH`, `REMOTE` |
| COND_LASTFOUR |  | int | Search by the last four digits of the account or card used in the transaction searched. | `4/4` |
| COND_CUSTOMERID |  | string | Search by the Paywire customer identifier returned when creating a token. |  |
| COND_RECURRINGID |  | int | Search by the periodic identifier returned when creating a periodic plan. |  |
| COND_PWINVOICENUMBER |  | string | Search by the merchant-submitted or Paywire-generated unique invoice number associated with the transaction. | `0/20`, Alphanumeric |
| COND_PWCUSTOMID1 |  | string | Search by the custom third-party identifier associated with the transaction. |  |

#### Transaction Result values

| Value | Description |
| --- | --- |
| SUCCESS | Pull transactions with result `CAPTURED`, `SETTLED`, `APPROVED`, `PENDING`. |
| UNSUCCESS | Pull transactions with result `DECLINED`, `ERROR`, `REJECTED`, `CHARGEBACK`. |
| CAPTURED | Successful transactions that have not settled. |
| SETTLED | Settled transactions. |
| APPROVED | Successful pre-auth transactions. |
| COMPLETED | Completed pre-auth transactions. |
| DECLINED | Declined transactions. |
| VOIDED | Voided transactions. |
| ERROR | Transactions failed due to error. |
| REJECTED | Transactions rejected by the processor at settlement time. |
| PENDING | Transactions settled, but have not yet received settlement confirmation. |
| CHARGEBACK | Credit card charge back or ACH return. |

### Response Parameters

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>SUCCESS</RESULT>
    <PWCLIENTID>0000010218</PWCLIENTID>
    <SEARCHRESULT>
        <PWPAYDETAIL>
            <PWUID>281195</PWUID>
            <BATCHID>526</BATCHID>
            <TRANSTYPE>SALE</TRANSTYPE>
            <CARDTYPE>VISA</CARDTYPE>
            <CARDNUM>X1111</CARDNUM>
            <SALEAMOUNT>30.00</SALEAMOUNT>
            <TRANSAMOUNT>30.00</TRANSAMOUNT>
            <ADJAMOUNT>0.00</ADJAMOUNT>
            <TIPAMOUNT>0.00</TIPAMOUNT>
            <TAXAMOUNT>0.00</TAXAMOUNT>
            <CREDITAMOUNT>0.00</CREDITAMOUNT>
            <AUTHCODE>281195</AUTHCODE>
            <RESULT>CAPTURED</RESULT>
            <NAME>Test Customer</NAME>
            <RECURRINGID>0</RECURRINGID>
            <TRANSTIME>11/09/2020 05:54 PM</TRANSTIME>
            <PWINVOICENUMBER>test12345</PWINVOICENUMBER>
            <PWCUSTOMID2>c211021fdc26324095c1111111111a0a</PWCUSTOMID2>
        </PWPAYDETAIL>
        <PWPAYDETAIL>
            <PWUID>281195</PWUID>
            <BATCHID>526</BATCHID>
            <TRANSTYPE>SALE</TRANSTYPE>
            <CARDTYPE>MC</CARDTYPE>
            <CARDNUM>X8390</CARDNUM>
            <SALEAMOUNT>30.00</SALEAMOUNT>
            <TRANSAMOUNT>30.00</TRANSAMOUNT>
            <ADJAMOUNT>0.00</ADJAMOUNT>
            <TIPAMOUNT>0.00</TIPAMOUNT>
            <TAXAMOUNT>0.00</TAXAMOUNT>
            <CREDITAMOUNT>0.00</CREDITAMOUNT>
            <DESCRIPTION>testing</DESCRIPTION>
            <RESULT>DECLINED</RESULT>
            <RESPONSETEXT>CVV2 Mismatch</RESPONSETEXT>
            <NAME>Test Customer</NAME>
            <CUSTOMERID>T1AF108AB86C0000</CUSTOMERID>
            <RECURRINGID>0</RECURRINGID>
            <TRANSTIME>11/09/2020 05:57 PM</TRANSTIME>
            <PWINVOICENUMBER>test12346</PWINVOICENUMBER>
            <PWCUSTOMID2>c211021fdc26324095c1111111111a0a</PWCUSTOMID2>
        </PWPAYDETAIL>
    </SEARCHRESULT>
</PAYMENTRESPONSE>
```

> The `SEARCHRESULT` value has been summarized by `...` for brevity.

The gateway will return the following parameters in the XML response:

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| PWUID | int | Unique transaction ID associated with the transaction(s) being searched. |  |
| BATCHID | int | Unique identifier for the batch containing the transaction(s) being searched. |  |
| ORGTRANSID | string | Relevant only for `VOID` and `CREDIT` results - the original unique transaction ID of a referenced transaction. |  |
| TRANSTYPE | string | The type of transaction(s) being searched. | See [API Transaction Types](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-transaction-types) |
| CARDTYPE | string | The type of card the transaction(s) being searched were processed with. |  |
| CARDNUM | int | Last four digits of the card or account number used to complete the transaction(s) being searched. |  |
| TRANSAMOUNT | decimal | Amount of transaction(s) being searched. |  |
| ADJAMOUNT | decimal | Adjustment amount of transaction. |  |
| TIPAMOUNT | decimal | Tip amount of transaction. |  |
| TAXAMOUNT | decimal | Tax amount of transaction. |  |
| CREDITAMOUNT | decimal | Relevant only for refunded transactions: the amount credited back to the customer. |  |
| DESCRIPTION | string | Transaction custom description message. |  |
| AUTHCODE | string | Authorization code associated with the Authorization transaction. |  |
| RESULT | string | Status for the transaction. | See [Transaction Result values](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#transaction-result-values). |
| RESPONSETEXT | string | Response Text of the Transaction. |  |
| NAME | string | Account holder full name. |  |
| CUSTOMERID | string | Paywire customer identifier associated with a token. |  |
| RECURRINGID | int | Periodic identifier of a periodic plan that a transaction forms part of. |  |
| TRANSTIME | DateTime | Timestamp of the transaction. |  |
| PWINVOICENUMBER | string | Merchant’s unique invoice number to be associated with this transaction. If not submitted, this will be generated by the gateway and returned in the XML response. |  |
| PWCUSTOMID1 | string | Custom third-party id to be associated with this transaction. |  |
| PWCUSTOMID2 | string | Processor-specific transaction unique identifier. Not always applicable. |  |
| PWCUSTOMID3 | string | Processor-specific ACH transaction split-fee _paydetail_ identifier. Not always applicable. |  |
| POSINDICATOR | string | Used in conjunction with Token Sales to apply Convenience Fees or Cash Discount for periodic payments handled outside Paywire. | `C`: Regular Token Sale `I`: First Payment of a Periodic Plan `R`: Subsequent Periodic Payment `T`: Last Payment of a Periodic Plan |
| SETTLEMENTDATE | Date | Transaction settlement date. Only available for ACH payments. | `YYYY-MM-DD` |

API Search Chargeback
---------------------

### Request Parameters

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>SEARCHCB</PWTRANSACTIONTYPE>
      <XOPTION>TRUE</XOPTION>
   </TRANSACTIONHEADER>
   <SEARCHCONDITION>
      <COND_DATEFROM>2020-12-01</COND_DATEFROM>
      <COND_DATETO>2020-12-31</COND_DATETO>
      <COND_CBTYPE>ALL</COND_CBTYPE>
      <COND_INSTITUTION>ALL</COND_INSTITUTION>
      <COND_UNIQUEID />
    </SEARCHCONDITION>
</PAYMENTREQUEST>
```

To search Chargeback transactions within the gateway, submit `SEARCHCB` in the `<PWTRANSACTIONTYPE />` parameter along with the desired `<SEARCHCONDITION />` values.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `SEARCHCB` |
| XOPTION |  | Bool | Show the search result in XML or escaped XML. | Options: `TRUE` `FALSE` |
| COND_DATEFROM |  | DateTime | Search date range from. | Date Format `YYYY-MM-DD`. |
| COND_DATETO |  | DateTime | Search date range to. | Date Format `YYYY-MM-DD`. |
| COND_CBTYPE | ✓ | string | Chargeback Search Options. | Options: `ALL` `CHARGEBACK`: Credit card chargebacks `RETRIEVAL`: Credit card retrieval `RETURN`: ACH returns `NOC`: ACH notification of change `REJECT`: Rejected settlement |
| COND_INSTITUTION | ✓ | string | Chargeback Search Options. | Options: `ALL`: Search for Chargebacks for all the MIDS in the institution. `CURRENT`: Search for Chargebacks for the current MID only. |

### Response Parameters

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>SUCCESS</RESULT>
    <PWCLIENTID>{clientId}</PWCLIENTID>
    <SEARCHRESULT>
        <PWCBDETAIL>
            <MID>635464646541</MID>
            <EXTERNALMID>941000131267890</EXTERNALMID>
            <PWUID>4771769</PWUID>
            <CARDTYPE>CUP</CARDTYPE>
            <NAME>TEST NAME</NAME>
            <RETURNDATE>2021-01-14</RETURNDATE>
            <RESULT>CHARGEBACK</RESULT>
            <RETURNAMOUNT>5.00</RETURNAMOUNT>
            <RETURNCODE>4502</RETURNCODE>
            <RESPONSETEXT>Transaction was Settled but Cash/Goods/Services were not Received</RESPONSETEXT>
            <CBCYCLE>1</CBCYCLE>
            <CBCURRENCY>USD</CBCURRENCY>
            <CBTRACENUM>808485</CBTRACENUM>
            <TRAMOUNT>100.00</TRAMOUNT>
            <TRCURRENCY>USD</TRCURRENCY>
            <TRDATETIME>2021-01-12 11:51:53</TRDATETIME>
            <PWINVOICENUBMER>trans_test_002</PWINVOICENUBMER>
            <CARDDATA>622222xxxxxx1234</CARDDATA>
            <DESCRIPTOR>Sample Merchant Sample City USA</DESCRIPTOR>
        </PWCBDETAIL>
        <PWCBDETAIL>
            <MID>635464646123</MID>
            <EXTERNALMID>941000131212345</EXTERNALMID>
            <PWUID>4771711</PWUID>
            <CARDTYPE>CUP</CARDTYPE>
            <NAME> </NAME>
            <RETURNDATE>2020-12-05</RETURNDATE>
            <RESULT>CHARGEBACK</RESULT>
            <RETURNAMOUNT>10.00</RETURNAMOUNT>
            <RETURNCODE>9706</RETURNCODE>
            <RESPONSETEXT>Special Adjustment - Other</RESPONSETEXT>
            <CBCYCLE>1</CBCYCLE>
            <CBCURRENCY>USD</CBCURRENCY>
            <CBTRACENUM>702147</CBTRACENUM>
            <TRAMOUNT>100.00</TRAMOUNT>
            <TRCURRENCY>USD</TRCURRENCY>
            <TRDATETIME>2021-01-05 18:03:24</TRDATETIME>
            <PWINVOICENUBMER>trans_test_002</PWINVOICENUBMER>
            <CARDDATA>622222xxxxxx1234</CARDDATA>
            <DESCRIPTOR>Test Merchant Test City USA</DESCRIPTOR>
        </PWCBDETAIL>
    </SEARCHRESULT>
</PAYMENTRESPONSE>
```

The gateway will return the following parameters in the XML response:

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| MID | int | Merchant Identifier. |  |
| EXTERNALMID | int | External Merchant Identifier. |  |
| PWUID | int | Unique transaction ID associated with the transaction(s) being searched. |  |
| CARDTYPE | string | The type of card the transaction(s) being searched were processed with. |  |
| NAME | string | Account holder full name. |  |
| RETURNDATE | Date | Timestamp of the Search transaction. | `YYYY-MM-DD` |
| RESULT | string | Status for the transaction. | See [Transaction Result values](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#transaction-result-values). |
| RETURNAMOUNT | decimal | Return Amount of transaction(s) being searched. |  |
| RETURNCODE | string | Return code associated with the transaction. |  |
| RESPONSETEXT | string | Response Text of the Transaction. |  |
| CBCYCLE | int | Chargeback Cycle. | Currently it is always "`1`" |
| CBCURRENCY | string | Chargeback Currency. | `USD` / `EUR` / `CNY` |
| CBTRACENUM | int | Trace number for each chargeback record. |  |
| TRAMOUNT | decimal | Original transaction amount. |  |
| TRCURRENCY | string | Original transaction currency. | `USD` / `EUR` / `CNY` |
| TRDATETIME | Datetime | Original transaction date time. | `YYYY-MM-DD HH:mm:ss` |
| PWINVOICENUBMER | int | Original transaction invoice number, provided by merchant. | Length 0~60 |
| CARDDATA | int | Card number first 6 and last 4 number of the card will be displayed. | Example:123456xxxxxx1234 |
| DESCRIPTOR | string | Descriptor showing on customer's bank statement. | Length 0~41 |

API Search Periodic Plans
-------------------------

To search Periodic Plans within the gateway, submit the `GETPERIODICPLAN` in the `<PWTRANSACTIONTYPE />` parameter along with the desired `<SEARCHCONDITION />` by parameters `RECURRINGID`, `PWTOKEN` or `PWCID`. If more than one conditions is passed, the logic will consider only one condition and the precedence order is RECURRINGID>PWTOKEN>PWCID.

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>GETPERIODICPLAN</PWTRANSACTIONTYPE>
      <XOPTION>TRUE</XOPTION>
   </TRANSACTIONHEADER>
   <SEARCHCONDITION>
      <RECURRINGID>123</RECURRINGID>
   </SEARCHCONDITION>
</PAYMENTREQUEST>
```

### Request Parameters

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `GETPERIODICPLAN` |
| XOPTION | ✓ | Bool | Show the search result in XML or escaped XML. | `TRUE` or `FALSE` |
| RECURRINGID |  | int | Periodic Plan ID. |  |
| PWTOKEN |  | string | Token returned by the Paywire Gateway. |  |
| PWCID |  | string | Identifier for Customer stored in the Paywire Vault. |  |

### Response Parameters

> Approved Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>SUCCESS</RESULT>
    <PWCLIENTID>0000010218</PWCLIENTID>
    <SEARCHRESULT>
	<PWCID>281195</PWCID>
	<EMAIL>test@example.com</EMAIL>
	<ADDRESS>Address Line1</ADDRESS>
	<ADDRESS2>Address Line2</ADDRESS2>
	<CITY>Fairbanks</CITY>
	<STATE>AL</STATE>
	<ZIP>99701-0000</ZIP>
	<PWTOKENS>
		<PWTOKENDETAIL>
			<PWMEDIA>ECHECK</PWMEDIA>
			<BANKACCTTYPE>CHECKING</BANKACCTTYPE>
			<PWTOKEN>T7B62A7F2D5A3249</PWTOKEN>
			<MACCOUNT>XXXXX4444</MACCOUNT>
			<ACCOUNTHOLDERFIRSTNAME>FirstName</ACCOUNTHOLDERFIRSTNAME>
			<ACCOUNTHOLDERLASTNAME>LastName</ACCOUNTHOLDERLASTNAME>
			<PWPLANS>
				<PWPLANDETAIL>
					<RECURRINGID>123</RECURRINGID>
					<CREATEDDATETIME>4/18/2021 6: 46: 26 PM</CREATEDDATETIME>
					<DELETEDDATETIME>1/1/0001 12: 00: 00 AM</DELETEDDATETIME>
					<BILLSTARTDATE>4/18/2021</BILLSTARTDATE>
					<BILLENDDATE>5/3/2021</BILLENDDATE>
					<BILLNEXTDATE>5/2/2021</BILLNEXTDATE>
					<BILLCYCLE>B</BILLCYCLE>
					<AMOUNT>9.00</AMOUNT>
					<ADJAMOUNT>0.00</ADJAMOUNT>
					<TOTALAMOUNT>9.00</TOTALAMOUNT>
					<PLANTOTALAMOUNT>0.00</PLANTOTALAMOUNT>
					<PAYMENTLEFT>1</PAYMENTLEFT>
					<PAYMENTPROCESSED>1</PAYMENTPROCESSED>
					<ORDERID>000000014</ORDERID>
					<DESCRIPTION>plan description</DESCRIPTION>
				</PWPLANDETAIL>
			</PWPLANS>
		</PWTOKENDETAIL>
	</PWTOKENS>
    </SEARCHRESULT>
</PAYMENTRESPONSE>
```

> Declined Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>ERROR</RESULT>
    <RESTEXT>INVALID RECURRINGID</RESTEXT>
</PAYMENTRESPONSE>
```

The gateway will return the following parameters in the XML response:

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| PWCID | string | Identifier for Customer stored in the Paywire Vault. |  |
| EMAIL | string | Account Holder's email address. |  |
| ADDRESS | string | Account Holder's primary address. |  |
| ADDRESS2 | string | Account Holder's secondary address. |  |
| CITY | string | Account Holder's city of residence. |  |
| STATE | string | Account Holder's state or province of residence. |  |
| ZIP | string | Account Holder's address postal/zip code. See important note on [Zip Codes](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#Zip-codes). |  |
| PWTOKEN | string | Unique token representing a customer's card or account details stored on the Paywire Gateway. |  |
| PWMEDIA | string | Defines the payment method. | `CC` `ECHECK` |
| BANKACCTTYPE | string | Type of Bank Account. Returned only when `ECHECK` in `PWMEDIA` and a valid `PWTOKEN` are submitted in the request. | `CHECKING`, `SAVINGS` |
| CCTYPE | string | Type of Credit Card. Returned only when `CC` in `PWMEDIA`. | `VISA`, `MC`, `DISC`, `AMEX`, `CUP`, `JCB`, `DINERS` |
| MACCOUNT | string | Masked account number. |  |
| EXP_MM | string | Card expiry month. Returned only when `CC` in `PWMEDIA`. |
| EXP_YY | string | Card expiry year. Returned only when `CC` in `PWMEDIA`. |
| ACCOUNTHOLDERFIRSTNAME | string | Account Holder's first name. |  |
| ACCOUNTHOLDERLASTNAME | string | Account Holder's last name. |  |
| RECURRINGID | int | Periodic identifier of a periodic plan that a transaction forms part of. |  |
| CREATEDDATETIME | Datetime | Transaction created date time. | `YYYY-MM-DD HH:mm:ss` |
| DELETEDDATETIME | Datetime | Transaction deleted date time. | `YYYY-MM-DD HH:mm:ss` |
| BILLSTARTDATE | Datetime | Billing Start Date. | `MM/DD/YYYY` |
| BILLENDDATE | Datetime | Billing End Date. | `MM/DD/YYYY` |
| BILLNEXTDATE | Datetime | Next Billing Date. | `MM/DD/YYYY` |
| BILLCYCLE | string | The BILLING CYCLE at which periodic payments are charged. | `W`: Weekly, `B`: Bi-weekly, `M`: Monthly, `H`: Semi-monthly, `Q`: Quarterly, `S`: Semi-annual, `Y`: Yearly |
| AMOUNT | int/decimal | Amount of the transaction. |
| ADJAMOUNT | int/decimal | The adjustment amount calculated by the gateway, based on the Adjustment rate or fixed amount set in the merchant configuration. This can be either the Cash Discount markdown or the Convenience Fee. |
| TOTALAMOUNT | int/decimal | Sum of AMOUNT and ADJAMOUNT of the transaction. |
| PLANTOTALAMOUNT | int/decimal | Total Plan amount of the periodic plan. |
| PAYMENTLEFT | int | Remaining Payment cycles for the periodic plan. |
| PAYMENTPROCESSED | int | Number of Payments processed for the periodic plan. |
| ORDERID | string | The order number associated with this periodic plan. |
| DESCRIPTION | string | The description associated with this periodic plan. |

API BIN Validation
------------------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>BINVALIDATION</PWTRANSACTIONTYPE>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <BINNUMBER>400057</BINNUMBER>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>SUCCESS</RESULT>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <BINVALDETAIL>
       <BIN>400057</BIN>
       <BRAND>VISA</BRAND>
       <CARDTYPE>DEBIT</CARDTYPE>
       <BANK>PNC Bank, National Association</BANK>
       <COUNTRY>USA</COUNTRY>
       <ISFSA>TRUE</ISFSA>
       <ISPREPAID>FALSE</ISPREPAID>
    </BINVALDETAIL>
</PAYMENTRESPONSE>
```

### Request Parameters

To process a BIN validation, send the BIN number and the system will return the information of the card.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `BINVALIDATION` |
| BINNUMBER | ✓ | string | The first 6 ~ 8 digits of the card number. 8 Digit is recommended for better accuracy |  |

### Response Parameters

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| BIN | string | The BIN number being validated. |  |
| BRAND | string | The card brand of the validated BIN number | `VISA`, `MASTERCARD`, `DISCOVER`, `AMEX`, `CUP` |
| CARDTYPE | string | The card type of the validated BIN number | `CREDIT`, `DEBIT` |
| SUBTYPE | string | The sub type of the validated BIN number if applicable, 0 ~ 50 characters |  |
| BANK | string | The bank name of the validated BIN number if applicable, 0 ~ 50 characters |  |
| COUNTRY | string | The country of the validated BIN number if applicable, 3 characters |  |
| ISFSA | bool | `TRUE` if the card is an FSA/HSA card. | `TRUE`/`FALSE` |
| ISPREPAID | bool | `TRUE` if the card is an prepaid card. | `TRUE`/`FALSE` |

API Send Receipt
----------------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>SENDRECEIPT</PWTRANSACTIONTYPE>
      <PWUNIQUEID>112302</PWUNIQUEID>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <EMAIL>jd@example.com</EMAIL>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>SUCCESS</RESULT>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <PWINVOICENUMBER>18312144115956459</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

To send a receipt for an existing transaction, submit `SENDRECEIPT` in the `<PWTRANSACTIONTYPE />` parameter along with the required parameters.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `SENDRECEIPT` |
| PWUNIQUEID | ✓ | int | Unique transaction ID returned in the transaction response, associated with the transaction to send a receipt for. |  |
| EMAIL | ✓ | string | Account Holder's email address. |  |

API Level 3 Processing
----------------------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <COMPANYNAME>The Company</COMPANYNAME>
      <FIRSTNAME>John</FIRSTNAME>
      <LASTNAME>Doe</LASTNAME>
      <EMAIL>jd@example.com</EMAIL>
      <ADDRESS1>1 The Street</ADDRESS1>
      <CITY>New York</CITY>
      <STATE>NY</STATE>
      <ZIP>12345</ZIP>
      <COUNTRY>US</COUNTRY>
      <PRIMARYPHONE>1234567890</PRIMARYPHONE>
      <WORKPHONE>1234567890</WORKPHONE>
      <PWMEDIA>CC</PWMEDIA>
      <CARDNUMBER>4111111111111111</CARDNUMBER>
      <EXP_MM>02</EXP_MM>
      <EXP_YY>22</EXP_YY>
      <CVV2>123</CVV2>
   </CUSTOMER>
   <LEVEL3>
      <DUTYAMOUNT>1.00</DUTYAMOUNT>
      <DISCOUNTAMOUNT>0.55</DISCOUNTAMOUNT>
      <SHIPAMOUNT>2.50</SHIPAMOUNT>
      <SHIPFROMZIP>09876</SHIPFROMZIP>
      <SHIPTOZIP>12345</SHIPTOZIP>
      <DESTCOUNTRYCODE>US</DESTCOUNTRYCODE>
      <LINEITEMCOUNT>2</LINEITEMCOUNT>
      <PONUMBER>A0L3112FF4</PONUMBER>
      <MERCHANTTAXID>837431974973</MERCHANTTAXID>
      <CUSTOMERTAXID>444264662642</CUSTOMERTAXID>
      <COMMODITYCODE>50340000</COMMODITYCODE>
      <LINEITEMS>
         <LINEITEM>
            <ITEM_DESCRIPTION>Frozen Berries</ITEM_DESCRIPTION>
            <ITEM_CODE>00003154</ITEM_CODE>
            <ITEM_QUANTITY>2</ITEM_QUANTITY>
            <ITEM_UNITMEASURE>Each</ITEM_UNITMEASURE>
            <ITEM_UNITCOST>3.00</ITEM_UNITCOST>
            <ITEM_TAXAMOUNT>0.60</ITEM_TAXAMOUNT>
            <ITEM_TAXRATE>10.00</ITEM_TAXRATE>
            <ITEM_DISCOUNTIND/>
            <ITEM_DISCOUNT>0.33</ITEM_DISCOUNT>
            <ITEM_TOTALAMOUNT>6.27</ITEM_TOTALAMOUNT>
            <ITEM_COMMODITYCODE>50340000</ITEM_COMMODITYCODE>
            <ITEM_TAXTYPE/>
            <ITEM_NETGROSSIND/>
            <ITEM_EXTAMOUNT/>
         </LINEITEM>
         <LINEITEM>
            <ITEM_DESCRIPTION>Frozen Apples</ITEM_DESCRIPTION>
            <ITEM_CODE>00003102</ITEM_CODE>
            <ITEM_QUANTITY>1</ITEM_QUANTITY>
            <ITEM_UNITMEASURE>Each</ITEM_UNITMEASURE>
            <ITEM_UNITCOST>4.00</ITEM_UNITCOST>
            <ITEM_TAXAMOUNT>0.40</ITEM_TAXAMOUNT>
            <ITEM_TAXRATE>10.00</ITEM_TAXRATE>
            <ITEM_DISCOUNTIND/>
            <ITEM_DISCOUNT>0.22</ITEM_DISCOUNT>
            <ITEM_TOTALAMOUNT>4.18</ITEM_TOTALAMOUNT>
            <ITEM_COMMODITYCODE>50340000</ITEM_COMMODITYCODE>
            <ITEM_TAXTYPE/>
            <ITEM_NETGROSS_IND/>
            <ITEM_EXTAMOUNT/>
         </LINEITEM>
      </LINEITEMS>
   </LEVEL3>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <RESULT>APPROVAL</RESULT>
    <BATCHID>1</BATCHID>
    <PWCLIENTID>0000000001</PWCLIENTID>
    <PAYMETH>C</PAYMETH>
    <PWUNIQUEID>112387</PWUNIQUEID>
    <AHNAME>John Doe</AHNAME>
    <AMOUNT>10.45</AMOUNT>
    <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
    <EMAIL>jd@example.com</EMAIL>
    <CCTYPE>VISA</CCTYPE>
    <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

The payment card processing system is set up on three levels:

1.   Level 1 transactions for domestic card holders using personal cards. 
    *   Data includes standard card information such as the card number, card expiration date, billing address, zip code and invoice number.

2.   Level 2 transactions for domestic corporate cards. 
    *   Data required to qualify for this level includes a summary of applicable taxes, customer references and purchase order numbers on top of Level 1 data.

3.   Level 3 transactions for government-issued credit cards or corporate cards. 
    *   Data to include line item details with product codes, description, quantity, and unit costs on top of Level 1 and Level 2 data.

To process Level 3 transactions, please include the required fields below with each payment request.

The following need to be submitted in the `LEVEL3` block:

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| DUTYAMOUNT | ✓ | int | Duty amount in the transaction. |  |
| DISCOUNTAMOUNT | (✓) | int/decimal | The discount amount applied to the transaction. Required when processing a transaction using VISA. |  |
| SHIPAMOUNT | ✓ | int/decimal | The shipping cost in the total transaction amount. |  |
| SHIPFROMZIP | ✓ | string | The originator's ZIP code. | `1/9` |
| SHIPTOZIP | ✓ | string | The destination's ZIP code. | `1/9` |
| DESTCOUNTRYCODE | ✓ | string | The destination's country code. | `2/3`, ISO-3166 codes |
| LINEITEMCOUNT | ✓ | int | Count of line items being submitted. | `1/999` |
| PONUMBER |  | string | Purchase Order number for the transaction. | `0/60` |
| MERCHANTTAXID | (✓) | string | Merchant VAT tax number. Required when processing a transaction using VISA. | `0/20` |
| CUSTOMERTAXID | (✓) | string | Customer VAT registration number. Required when processing a transaction using VISA. | `0/20` |
| COMMODITYCODE | (✓) | string | Summary Commodity Code. Required when processing a transaction using VISA. | `0/4` |

The following need to be submitted in the `LINEITEM` tags within `LINEITEMS`, nested under the `LEVEL3` block:

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| ITEM_DESCRIPTION | ✓ | string | Description of the item. | `1/35` |
| ITEM_CODE | (✓) | string | Product code, identifier or SKU. Required when processing a transaction using VISA or Mastercard. | `0/12` |
| ITEM_QUANTITY | ✓ | decimal | Product quantity. |  |
| ITEM_UNITMEASURE | (✓) | string | Unit of measure for the line item. Required when processing a transaction using VISA or Mastercard. | `0/12` |
| ITEM_UNITCOST | ✓ | string | Cost of item per unit of measure before tax/discount. | `1/12` |
| ITEM_TAXAMOUNT | (✓) | string | Tax amount for item. Required when processing a transaction using VISA or Mastercard. | `0/12` |
| ITEM_TAXRATE | (✓) | decimal | Tax rate for item. Required when processing a transaction using VISA or Mastercard. |  |
| ITEM_DISCOUNTIND | (✓) | string | Identifies whether a discount has been applied. Required when processing a transaction using Mastercard. | Options: `Y`, `N` |
| ITEM_DISCOUNT | (✓) | string | Discount per item. Required when processing a transaction using VISA or Mastercard. | `0/12` |
| ITEM_TOTALAMOUNT | (✓) | string | Total amount of this line item. Required when processing a transaction using VISA. | `0/12` |
| ITEM_COMMODITYCODE | (✓) | string | Commodity Code. Required when processing a transaction using VISA. | `0/12` |
| ITEM_TAXTYPE | (✓) | string | Tax type. Required when processing a transaction using Mastercard. | `0/4` |
| ITEM_NETGROSSIND | (✓) | string | Indicates whether the submitted amount is Net or Gross. Required when processing a transaction using Mastercard. | Options: `N` - Net, `G` - Gross |
| ITEM_EXTAMOUNT | (✓) | string | Extended item amount. Required when processing a transaction using Mastercard. |  |

Digital Wallet Data Group
-------------------------

Basic fields of the `DIGITALWALLET` group:

| Value | Description |
| --- | --- |
| `DWTYPE` | `A` for Apple Pay or `G` for Google Pay |
| `DWPAYLOAD` | The encrypted payload from the digital wallet |

If you have decrypted payload with the transactions, please use `CARDNUMBER`, `EXP_MM`, `EXP_YY` to fill in the card info and the following fields to the `DIGITALWALLET` group instead of `DWPAYLOAD`

| Value | Description |
| --- | --- |
| `ISTDES` | `TRUE` or `FALSE` |
| `CAVV` | The CAVV data or the equivalent crypto data received from the authenticator |
| `ECI` | The ECI value received from the authenticator |
| `UCAF` | The UCAF indicator received from the authenticator (MasterCard Only). |

OSBP Reference
--------------

The OSBP expedites integration with the Paywire Gateway, moving PCI out-of-scope by collecting all card and ACH data on an externally-hosted PCI-Certified payment page.

OSBP Overview
-------------

> Source Code Example:

```
// Sending Page
protected void PaymentButton(object sender, EventArgs e)
{
     string url = "https://dbstage1.paywire.com/OSBP/pwosbp.aspx";
     string content = {XML Payload};
     if !(content == "")
     {
         httpPostForm(content, url);
     }
}

private void httpPostForm(string content, string url)
{
     System.Web.HttpContext.Current.Response.Clear();
     System.Web.HttpContext.Current.Response.Write("<html><head></head>");
     System.Web.HttpContext.Current.Response.Write(string.Format("<body onload=\"document.{0}.submit();\">", "newForm"));
     System.Web.HttpContext.Current.Response.Write("<form name=\"newForm\" target=\"_parent\" method=\"POST\" action=\"" + url + "\">");
     System.Web.HttpContext.Current.Response.Write(string.Format("<input type=hidden name=\"PWREQUEST\"value=\"{0}\">", base64Encode(content)));
     System.Web.HttpContext.Current.Response.Write("</form>");
     System.Web.HttpContext.Current.Response.Write("</body></html>");
}

public string base64Encode(string data)
{
     try
     {
          byte[] encData_byte = new byte[data.Length];
          encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
          string encodedData = Convert.ToBase64string(encData_byte);
          return encodedData;
     }
     catch (Exception e)
     {
         throw new Exception("Error in base64Encode" + e.Message);
     }
}

// Receiving Page
protected void Page_Load(object sender, EventArgs e)
{
     string requeststring = "";
     StreamReader reader = new StreamReader(this.Request.InputStream);
     requeststring = reader.ReadToEnd();
     requeststring = requeststring.Substring(11);
     requeststring = Server.UrlDecode(requeststring);
     Label1.Text = base64Decode(requeststring);
}

public string base64Decode(string data)
{
     try
     {
          System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
          System.Text.Decoder utf8Decode = encoder.GetDecoder();
          byte[] todecode_byte = Convert.FromBase64string(data);
          int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
          char[] decoded_char = new char[charCount];
          utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
          string result = new string(decoded_char);
          return result;
     }
     catch (Exception e)
     {
          throw new Exception("Error in base64Decode" + e.Message);
     }
}
```

> `XML Payload` is the only variable between different transaction types.

To process payments using the Paywire OSBP, you must:

1.   Optionally collect non-PCI customer information such as Name, Address and Contact information from your website.
2.   Create a Sale XML request string including Authentication parameters, Approval URL and Decline URL.
3.   Perform a HTTP POST with a HTML form containing the XML request string to the OSBP endpoint using the relevant URL.
4.   Customer is redirected to the Paywire OSBP, possibly with the option to choose a Payment Method.
5.   Customer completes or cancels the payment.
6.   The Paywire OSBP returns a HTTP POST with an XML response to the Approval or Decline URL, depending on the result of the transaction.

* * *

[![Image 2: OSBP Process Flowchart](https://project.paywire.com/dbtranz/docs/OSBP/files/images/pwosbpflowv1.png)](https://project.paywire.com/dbtranz/docs/OSBP/files/images/pwosbpflowv1.png)

OSBP Endpoints
--------------

The same OSBP endpoint is available for all requests, across all environments:

`POST /OSBP/pwosbp.aspx`

`Content-Type: text/xml`

OSBP Transaction Types
----------------------

The Paywire OSBP only supports `SALE` transactions. Please use the API to post Voids and Credits:

| Value | Description |
| --- | --- |
| SALE | Charge a card or bank account (if applicable). |

OSBP PWCTRANSTYPE
-----------------

Use this to define pre-set behaviors for the chosen Transaction Type:

| Value | Description |
| --- | --- |
| 3 | One-time transaction only. No customer record created. |
| 4 | Process a Sale or Authorization and add a new token for OSBP and VPOS usage. |
| 5 | Periodically, add a new token for OSBP and VPOS usage. |

OSBP One-Time Sale
------------------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <AUTHTOKEN>4C2F8EE94CA2491AAB67EA6541CB17BA</AUTHTOKEN>
      <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
      <PWAPPROVALURL>https://send.paywire.com/Receive.aspx</PWAPPROVALURL>
      <PWDECLINEURL>https://send.paywire.com/Receive.aspx</PWDECLINEURL>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <PWCTRANSTYPE>3</PWCTRANSTYPE>
      <ENFORCEPAYMENTTYPE>FALSE</ENFORCEPAYMENTTYPE>
      <PWMEDIA>CC</PWMEDIA>
      <COMPANYNAME>The Company</COMPANYNAME>
      <FIRSTNAME>John</FIRSTNAME>
      <LASTNAME>Doe</LASTNAME>
      <ADDRESS1>1 The Street</ADDRESS1>
      <ADDRESS2 />
      <CITY>New York</CITY>
      <STATE>NY</STATE>
      <ZIP>12345</ZIP>
      <COUNTRY>US</COUNTRY>
      <EMAIL>jd@example.com</EMAIL>
      <PRIMARYPHONE>410-516-8000</PRIMARYPHONE>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Approved Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
  <PWCLIENTID>0000000001</PWCLIENTID>
  <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
  <RESULT>APPROVAL</RESULT>
  <AMOUNT>10.00</AMOUNT>
  <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
  <PAYMETH>C</PAYMETH>
  <CCTYPE>VISA</CCTYPE>
  <AHNAME>John Doe</AHNAME>
  <AHFIRSTNAME>John</AHFIRSTNAME>
  <AHLASTNAME>Doe</AHLASTNAME>
  <PWUNIQUEID>130820</PWUNIQUEID>
  <BATCHID>1</BATCHID>
  <EMAIL>jd@example.com</EMAIL>
  <AUTHCODE>074572</AUTHCODE>
  <AVSCODE>R</AVSCODE>
</PAYMENTRESPONSE>
```

> Declined Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
   <RESULT>DECLINED</RESULT>
   <RESTEXT>CVV2 MISMATCH</RESTEXT>
   <RESPONSECODE>123</RESPONSECODE>
   <BATCHID>1</BATCHID>
   <PWCLIENTID>0000000001</PWCLIENTID>
   <AUTHCODE />
   <AVSCODE>0</AVSCODE>
   <PAYMETH>C</PAYMETH>
   <PWUNIQUEID>130822</PWUNIQUEID>
   <AHNAME>John Doe</AHNAME>
   <AMOUNT>10.00</AMOUNT>
   <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
   <EMAIL>jd@example.com</EMAIL>
   <CCTYPE>VISA</CCTYPE>
   <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

To process a Sale transaction, submit `SALE` in the `<PWTRANSACTIONTYPE />` parameter along with the mandatory fields:

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | `SALE` | Fixed options. |
| PWSALEAMOUNT | ✓ | int/decimal | Amount of the transaction. |  |
| PWINVOICENUMBER |  | string | The merchant's unique invoice number associated with this transaction. | `0/20`, Alphanumeric |
| PWAPPROVALURL | ✓ | string | A URL on the Merchant's website that the return XML data will be posted to if transaction is approved. |  |
| PWDECLINEURL | ✓ | string | A URL on the merchant's website that the return XML data will be posted to if the transaction is declined or any error occurs. |  |
| CVREQ |  | bool | Defines whether to request the card CVV on the payment form. A value of `FALSE` will hide the CVV field. |  |
| DETAILAMOUNT |  | int | The detail amount for the SAP interface associated with this line item. Required for specific Merchant IDs. |  |
| PWCID |  | string | Paywire Customer Identifier. This should be stored in the merchant database for future use. When submitted with `PWCTRANSTYPE` values of `4` or `5`, it associates the created token with this customer. When not submitted for the same `PWCTRANSTYPE`, a customer is created in the Paywire Vault and a `PWCID` is returned in the response. |  |
| PWCTRANSTYPE | ✓ | string | Transaction sub-type, allowing for different activities. | Fixed options: [OSBP PWCTRANSTYPE](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#osbp-pwctranstype) |
| ENFORCEPAYMENTTYPE |  | bool | If set to `TRUE`, enforce the payment type based on PWMEDIA | `TRUE/FALSE` |
| PWMEDIA |  | string | If `ENFORCEPAYMENTTYPE` is set to `TRUE`, only the specified payment type will display on the payment page. | `CC` or `ECHECK` |
| COMPANYNAME |  | string | Company name of the customer. |  |
| FIRSTNAME |  | string | Account Holder's first name. |  |
| LASTNAME |  | string | Account Holder's last name. |  |
| ADDRESS1 |  | string | Account Holder's primary address. |  |
| ADDRESS2 |  | string | Account Holder's secondary address. |  |
| CITY |  | string | Account Holder's city of residence. |  |
| STATE | (✓) | string | Account Holder's state of residence. Required if configured with [Convenience Fees](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#convenience-fees). |  |
| COUNTRY |  | string | Account Holder's country of residence. |  |
| ZIP |  | string | Account Holder's address postal/zip code. See important note on [Zip Codes](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#Zip-codes). |  |
| EMAIL |  | string | Account Holder's email address. |  |
| PRIMARYPHONE |  | string | Account Holder's primary phone number. |  |
| WORKPHONE |  | string | Account Holder's work phone number. |  |
| CUSTOMERNAME |  | string | Full name of the customer, possibly different than the Account Holder. |  |
| DISABLECF |  | bool | Overrides applying a Convenience Fee or Cash Discount when set to `TRUE`, if configured. Note that Sales Tax will also be disabled. | Default: `FALSE` |
| ADJTAXRATE |  | decimal | Overrides the configured Sales Tax rate. |  |
| PWCUSTOMID1 |  | string | Custom third-party ID to be associated with this transaction. |  |
| PWRECEIPTDESC |  | string | Extra information to be displayed on the receipt. | `0/200` |
| PWCASHIERID |  | string | Paywire-assigned cashier identifier. |  |
| SECCODE |  | string | SEC Code for ECHECK payments. | `3/3` |
| DISABLEDW |  | bool | Disable the digital wallet buttons when set to `TRUE` for the current payment session. | Default: `FALSE` |

### OSBP Response Parameters

| Parameter | Type | Description | Options |
| --- | --- | --- | --- |
| RESULT | string | Transaction Result | `APPROVAL` , `DECLINED` ,`ERROR` |
| RESTEXT | string | Transaction response message. |  |
| RESPONSECODE | string | Card transaction response code returned from the processor. | Data varies based on processor config. See: [Processor-Decline-Code](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#Processor-Decline-Code) |
| BATCHID | string | Transaction batch ID. |  |
| PWCLIENTID | string | Authentication credential provided to you by the administrator. |  |
| PAYMETH | string | Method of payment that the transaction was processed with: `E` for web ACH, `C` for Card. |
| PWUNIQUEID | string | The unique ID of the transaction. |  |
| AHNAME | string | The full name of the account holder. |  |
| AHFIRSTNAME | string | The first name of the account holder. |  |
| AHLASTNAME | string | The last name of the account holder. |  |
| PWADJAMOUNT | decimal | The adjustment amount of the transaction, applicable to Convenience Fee and Cash Discount transactions. |  |
| AMOUNT | decimal | The total approved amount of the transaction, including any adjustments. |  |
| MACCOUNT | string | The masked account number. |  |
| CCTYPE | string | For card payment, this is the cardbrand, for ACH payment, it is always `ACH` | `VISA`, `MC`, `DISC`, `AMEX`, `CUP`, `JCB`, `DINERS`, `ACH` |
| PWINVOICENUMBER | string | The merchants unique invoice number associated with this transaction. |  |
| PWTOKEN | string | The payment token for the payment method. |  |
| PWCID | string | The customer ID. |  |
| RECURRINGID | string | The plan UID for periodic payment plan. |  |
| AUTHCODE | string | The auth code from the processor. |  |
| AVSCODE | string | The AVS Response code from the processor. |  |
| CVVCODE | string | The CVV Response code from the processor. |  |
| ISDEBIT | Bool | Indicate if the card is a debit or credit card. | `TRUE`/`FALSE` |  |

OSBP Store Token Only
---------------------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <AUTHTOKEN>4C2F8EE94CA2491AAB67EA6541CB17BA</AUTHTOKEN>
      <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>0.00</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
      <PWAPPROVALURL>https://send.paywire.com/Receive.aspx</PWAPPROVALURL>
      <PWDECLINEURL>https://send.paywire.com/Receive.aspx</PWDECLINEURL>
      <AUTHONLY>TRUE</AUTHONLY>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <PWCTRANSTYPE>4</PWCTRANSTYPE>
      <COMPANYNAME>The Company</COMPANYNAME>
      <FIRSTNAME>John</FIRSTNAME>
      <LASTNAME>Doe</LASTNAME>
      <ADDRESS1>1 The Street</ADDRESS1>
      <ADDRESS2 />
      <CITY>New York</CITY>
      <STATE>NY</STATE>
      <ZIP>12345</ZIP>
      <COUNTRY>US</COUNTRY>
      <EMAIL>jd@example.com</EMAIL>
      <PRIMARYPHONE>410-516-8000</PRIMARYPHONE>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <PWCLIENTID>000000001</PWCLIENTID>
    <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
    <RESULT>APPROVAL</RESULT>
    <AMOUNT>0.00</AMOUNT>
    <PWSALETAX>0.00</PWSALETAX>
    <PWADJAMOUNT>0.00</PWADJAMOUNT>
    <PWSALEAMOUNT>0.00</PWSALEAMOUNT>
    <MACCOUNT>XXXXXXXXXXXX0608</MACCOUNT>
    <PAYMETH>C</PAYMETH>
    <CCTYPE>MC</CCTYPE>
    <AHNAME>John Doe</AHNAME>
    <AHFIRSTNAME>John</AHFIRSTNAME>
    <AHLASTNAME>Doe</AHLASTNAME>
    <PWUNIQUEID>173372</PWUNIQUEID>
    <BATCHID>1</BATCHID>
    <EMAIL>jd@example.com</EMAIL>
    <PWTOKEN>T040C8E2F56E835</PWTOKEN>
    <PWCUSTOMID2>6bb43696ab8fosq98e228284d684612</PWCUSTOMID2>
    <PWCID>P0000001234</PWCID>
</PAYMENTRESPONSE>
```

To create a token without processing a sale, include a value of `TRUE` in `AUTHONLY` along with `PWCTRANSTYPE` value of `4` and `PWSALEAMOUNT` of `0.00`.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| AUTHONLY | ✓ | bool | Processes an Authorization only when a value of `TRUE` is submitted. |  |
| PWSALEAMOUNT | ✓ | int/decimal | Amount of the transaction. A value of zero needs to be submitted when `AUTHONLY` is set to `TRUE`. | `0` or `0.00` |
| PWCTRANSTYPE | ✓ | string | Submit `4` to store a token. | `4` |

OSBP Create Customer
--------------------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <AUTHTOKEN>4C2F8EE94CA2491AAB67EA6541CB17BA</AUTHTOKEN>
      <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>5.00</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
      <PWAPPROVALURL>https://send.paywire.com/Receive.aspx</PWAPPROVALURL>
      <PWDECLINEURL>https://send.paywire.com/Receive.aspx</PWDECLINEURL>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <PWCTRANSTYPE>4</PWCTRANSTYPE>
      <FIRSTNAME>John</FIRSTNAME>
      <LASTNAME>Doe</LASTNAME>
      <PRIMARYPHONE>86-13012345678</PRIMARYPHONE>
      <FORCESAVETOKEN>FALSE</FORCESAVETOKEN>
      <STATE>CA</STATE>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <PWCLIENTID>000000001</PWCLIENTID>
    <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
    <RESULT>APPROVAL</RESULT>
    <AMOUNT>5.00</AMOUNT>
    <MACCOUNT>XXXXXXXXXXXX0608</MACCOUNT>
    <PAYMETH>C</PAYMETH>
    <CCTYPE>MC</CCTYPE>
    <AHNAME>John Doe</AHNAME>
    <AHFIRSTNAME>John</AHFIRSTNAME>
    <AHLASTNAME>Doe</AHLASTNAME>
    <PWUNIQUEID>173372</PWUNIQUEID>
    <BATCHID>123</BATCHID>
    <AUTHCODE>005960</AUTHCODE>
    <PWCUSTOMERID>T053E5A0293F9232</PWCUSTOMERID>
    <PWTOKEN>T053E5A0293F9232</PWTOKEN>
    <AVSCODE>Y</AVSCODE>
    <PWCID>P0000001234</PWCID>
</PAYMENTRESPONSE>
```

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| AUTHTOKEN | ✓ | string | The Authentication Token to be used when calling the [OSBP](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#authentication). |  |
| PWINVOICENUMBER |  | string | The merchant's unique invoice number associated with this transaction. | `0/20`, Alphanumeric |
| PWAPPROVALURL | ✓ | string | A URL on the merchant's website that the return XML data will be posted to if transaction is approved. |  |
| PWDECLINEURL | ✓ | string | A URL on the merchant's website to which the return XML data will be posted if the transaction is declined or any error occurs. |  |
| PWCTRANSTYPE | ✓ | string | Submit `4` to create Customer Token. | `4` |
| FORCESAVETOKEN | ✓ | Enum | Available for PWCTRANSTYPE = 2 or 4, default is `TRUE`, Set to `FALSE` to allow the customer to decide to save the token or not. | `TRUE` `FALSE` |
| PWCID |  | string | Paywire Customer Identifier. |  |
| FIRSTNAME |  | string | Account Holder's first name. |  |
| LASTNAME |  | string | Account Holder's last name. |  |
| STATE | (✓) | string | Account Holder's state of residence. Required if configured with [Convenience Fees](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#convenience-fees). |  |

OSBP Customer Sale
------------------

> Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <AUTHTOKEN>4C2F8EE94CA2491AAB67EA6541CB17BA</AUTHTOKEN>
      <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>5.00</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
      <PWAPPROVALURL>https://send.paywire.com/Receive.aspx</PWAPPROVALURL>
      <PWDECLINEURL>https://send.paywire.com/Receive.aspx</PWDECLINEURL>
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <PWCTRANSTYPE>2</PWCTRANSTYPE>
      <FIRSTNAME>John</FIRSTNAME>
      <LASTNAME>Doe</LASTNAME>
      <PRIMARYPHONE>86-13012345678</PRIMARYPHONE>
      <PWCID>P0000003978</PWCID>
      <FORCESAVETOKEN>FALSE</FORCESAVETOKEN>
      <STATE>CA</STATE>
   </CUSTOMER>
</PAYMENTREQUEST>
```

> Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
    <PWCLIENTID>000000001</PWCLIENTID>
    <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
    <RESULT>APPROVAL</RESULT>
    <AMOUNT>5.00</AMOUNT>
    <MACCOUNT>XXXXXXXXXXXX0608</MACCOUNT>
    <PAYMETH>C</PAYMETH>
    <CCTYPE>MC</CCTYPE>
    <AHNAME>John Doe</AHNAME>
    <AHFIRSTNAME>John</AHFIRSTNAME>
    <AHLASTNAME>Doe</AHLASTNAME>
    <PWUNIQUEID>173372</PWUNIQUEID>
    <BATCHID>123</BATCHID>
    <AUTHCODE>005960</AUTHCODE>
    <AVSCODE>Y</AVSCODE>
    <PWCID>P0000001234</PWCID>
</PAYMENTRESPONSE>
```

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| AUTHTOKEN | ✓ | string | The Authentication Token to be used when calling the [OSBP](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#authentication). |  |
| PWINVOICENUMBER |  | string | The merchant's unique invoice number associated with this transaction. | `0/20`, Alphanumeric |
| PWAPPROVALURL | ✓ | string | A URL on the merchant's website to which the return XML data will be posted if the transaction is approved. |  |
| PWDECLINEURL | ✓ | string | A URL on the merchant's website to which the return XML data will be posted if the transaction is declined or any error occurs. |  |
| PWCTRANSTYPE | ✓ | string | Submit `2` to conduct a Customer Sale. | `2` |
| FORCESAVETOKEN | ✓ | Enum | Available for PWCTRANSTYPE = 2 or 4, default is `TRUE`, Set to `FALSE` to allow the customer to decide to save the token or not. | `TRUE` `FALSE` |
| PWCID | ✓ | string | Paywire Customer Identifier. |  |
| FIRSTNAME | ✓ | string | Account Holder's first name. |  |
| LASTNAME | ✓ | string | Account Holder's last name. |  |
| STATE | (✓) | string | Account Holder's state of residence. Required if configured with [Convenience Fees](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#convenience-fees). |  |

OSBP Periodic Sale
------------------

> Request Example:

```
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      ...
   </TRANSACTIONHEADER>
   <CUSTOMER>
      <PWCTRANSTYPE>5</PWCTRANSTYPE>
      ...
      <FIRSTNAME>John</FIRSTNAME>
      <LASTNAME>Doe</LASTNAME>
      <EMAIL>jd@example.com</EMAIL>
   </CUSTOMER>
   <RECURRING>
      <STARTON>2018-01-01</STARTON>
      <FREQUENCY>W</FREQUENCY>
      <PAYMENTS>4</PAYMENTS>
   </RECURRING>
</PAYMENTREQUEST>
```

> For brevity, parameters identical to the One-Time-Sale XML request structure have been summarized by `...`
> 
> 
> Approved Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
  <PWCLIENTID>0000000001</PWCLIENTID>
  <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
  <RESULT>APPROVAL</RESULT>
  <AMOUNT>10.00</AMOUNT>
  <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
  <PAYMETH>C</PAYMETH>
  <CCTYPE>VISA</CCTYPE>
  <AHNAME>John Doe</AHNAME>
  <AHFIRSTNAME>John</AHFIRSTNAME>
  <AHLASTNAME>Doe</AHLASTNAME>
  <PWUNIQUEID>123456</PWUNIQUEID>
  <BATCHID>1</BATCHID>
  <EMAIL>jd@example.com</EMAIL>
  <AUTHCODE>0987654</AUTHCODE>
  <PWCID>P0000000001</PWCID>
  <AVSCODE>R</AVSCODE>
  <RECURRING>VISA</RECURRING>
  <RECURRINGID>115</RECURRINGID>
</PAYMENTRESPONSE>
```

> Declined Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
  <PWINVOICENUMBER>001001_20181003100218</PWINVOICENUMBER>
  <RESULT>ERROR</RESULT>
  <RESTEXT>OSBPXML: Invalid FREQUENCY Value</RESTEXT>
</PAYMENTRESPONSE>
```

In order to create a Periodic setup, submit `5` in the `<PWCTRANSTYPE>` parameter and include the `<RECURRING>` block in addition to the [One-Time-Sale parameters](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#osbp-one-time-sale).

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWCTRANSTYPE | ✓ | string | `5` | Fixed options: [OSBP PWCTRANSTYPE](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#osbp-pwctranstype). |
| STARTON | ✓ | Date | Date the first payment must be charged. | Date Format: `yyyy-mm-dd`. |
| FREQUENCY | ✓ | string | The frequency at which periodic payments are charged. | `W`: Weekly `B`: Bi-weekly `M`: Monthly `H`: Semi-monthly `Q`: Quarterly `S`: Semi-annual `Y`: Yearly |
| PAYMENTS | ✓ | int | Number of payments to process until the periodic setup is expired. | `1/999` |
| FIRSTNAME | ✓ | string | Account Holder's first name. |  |
| LASTNAME | ✓ | string | Account Holder's last name. |  |
| EMAIL | ✓ | string | Account Holder's email address. |  |

OSBP UnionPay
-------------

> CUP OSBP #1: Request: Secure Plus One-Time Sale

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <AUTHTOKEN>{authtoken}</AUTHTOKEN>
        <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <PWAPPROVALURL>https://www.clientdomain.com/approval</PWAPPROVALURL>
        <PWDECLINEURL>https://www.clientdomain.com/decline</PWDECLINEURL>
        <ISCUP>TRUE</ISCUP>
        <LANGUAGE>CHS</LANGUAGE>
     </TRANSACTIONHEADER>
     <CUSTOMER>
     <FIRSTNAME>John</FIRSTNAME>
     <LASTNAME>Doe</LASTNAME>
     <PRIMARYPHONE>86-13012345678</PRIMARYPHONE>
     <PWCTRANSTYPE>3</PWCTRANSTYPE>
     </CUSTOMER>
  </PAYMENTREQUEST>
```

> CUP OSBP #1: Response: Secure Plus One-Time Sale

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <RESULT>APPROVAL</RESULT>
        <AMOUNT>10.00</AMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <MACCOUNT>XXXXXXXXXXXX0017</MACCOUNT>
        <PAYMETH>C</PAYMETH>
        <CCTYPE>CUP</CCTYPE>
        <AHNAME>John Doe</AHNAME>
        <AHFIRSTNAME>John</AHFIRSTNAME>
        <AHLASTNAME>Doe</AHLASTNAME>
        <PWUNIQUEID>191601</PWUNIQUEID>
        <AUTHCODE>
        </AUTHCODE>
        <CVVCODE> </CVVCODE>
        <PWCUSTOMID2>000000191599</PWCUSTOMID2>
  </PAYMENTRESPONSE>
```

> CUP OSBP #2: Request: Secure Plus Sale and save customer with token - New Customer (no PWCID):

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <AUTHTOKEN>{authtoken}</AUTHTOKEN>
        <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <PWAPPROVALURL>https://www.clientdomain.com/approval</PWAPPROVALURL>
        <PWDECLINEURL>https://www.clientdomain.com/decline</PWDECLINEURL>
        <ISCUP>TRUE</ISCUP>
        <LANGUAGE>CHS</LANGUAGE>
        <FORCESAVETOKEN>TRUE</FORCESAVETOKEN>
     </TRANSACTIONHEADER>
     <CUSTOMER>
     <FIRSTNAME>John</FIRSTNAME>
     <LASTNAME>Doe</LASTNAME>
     <PRIMARYPHONE>86-13012345678</PRIMARYPHONE>
     <PWCTRANSTYPE>4</PWCTRANSTYPE>
     </CUSTOMER>
  </PAYMENTREQUEST>
```

> CUP OSBP #2 Example transaction notes:
> 
>  Set FORCESAVETOKEN to FALSE to allow the customer to decide whether to save token.

> CUP OSBP #2: Response: Secure Plus Sale and save customer with token - New Customer (no PWCID):

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <RESULT>APPROVAL</RESULT>
        <AMOUNT>10.00</AMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <MACCOUNT>XXXXXXXXXXXX0017</MACCOUNT>
        <PAYMETH>C</PAYMETH>
        <CCTYPE>CUP</CCTYPE>
        <AHNAME>John Doe</AHNAME>
        <AHFIRSTNAME>John</AHFIRSTNAME>
        <AHLASTNAME>Doe</AHLASTNAME>
        <PWUNIQUEID>191601</PWUNIQUEID>
        <BATCHID>696</BATCHID>
        <AUTHCODE></AUTHCODE>
        <PWTOKEN>TFC0206A817A7459</PWTOKEN>
        <CVVCODE> </CVVCODE>
        <PWCUSTOMID2>000000194342</PWCUSTOMID2>
        <PWCID>P0000007087</PWCID>
  </PAYMENTRESPONSE>
```

> CUP OSBP #2 Response transaction notes:
> 
>  PWCID: P0000007087 (The Customer ID, stands for a customer.) 
> 
>  PWTOKEN: TFC0206A817A7459 (The Payment Token, stands for a payment method. A customer can have multiple payment methods.) 
> 
>  PWUNIQUEID: 191601 (The Transaction Unique ID is to be used for the non-secureplus subsequent transactions.)

> CUP OSBP #3: Request: Secure Plus Token Sale - No Customer/PWCID involved, merchant to manage payment token:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <AUTHTOKEN>{authtoken}</AUTHTOKEN>
        <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <PWAPPROVALURL>https://www.clientdomain.com/approval</PWAPPROVALURL>
        <PWDECLINEURL>https://www.clientdomain.com/decline</PWDECLINEURL>
        <ISCUP>TRUE</ISCUP>
        <LANGUAGE>CHS</LANGUAGE>
     </TRANSACTIONHEADER>
     <CUSTOMER>
     <FIRSTNAME>John</FIRSTNAME>
     <LASTNAME>Doe</LASTNAME>
     <PRIMARYPHONE>86-13012345678</PRIMARYPHONE>
     <PWCTRANSTYPE>1</PWCTRANSTYPE>
     <PWTOKEN>TFC0206A817A7459</PWTOKEN>
     </CUSTOMER>
  </PAYMENTREQUEST>
```

> CUP OSBP #3 Request transaction notes:
> 
>  PWTOKEN is the one derived from CUP OSBP #2.

> CUP OSBP #3: Response: Secure Plus Token Sale - No Customer/PWCID involved, merchant to manage payment token:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <RESULT>APPROVAL</RESULT>
        <AMOUNT>10.00</AMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <MACCOUNT>XXXXXXXXXXXX0017</MACCOUNT>
        <PAYMETH>C</PAYMETH>
        <CCTYPE>CUP</CCTYPE>
        <AHNAME>John Doe</AHNAME>
        <AHFIRSTNAME>John</AHFIRSTNAME>
        <AHLASTNAME>Doe</AHLASTNAME>
        <PWUNIQUEID>191601</PWUNIQUEID>
        <BATCHID>696</BATCHID>
        <AUTHCODE></AUTHCODE>
        <PWTOKEN>TFC0206A817A7459</PWTOKEN>
        <CVVCODE> </CVVCODE>
        <PWCUSTOMID2>000000194342</PWCUSTOMID2>
  </PAYMENTRESPONSE>
```

> CUP OSBP #4: Request: Secure Plus Sale and add new token to existing customer (with PWCID) - Paywire to manage the customer saved token. Merchant only manages the Customer ID (PWCID):

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <AUTHTOKEN>{authtoken}</AUTHTOKEN>
        <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <PWAPPROVALURL>https://www.clientdomain.com/approval</PWAPPROVALURL>
        <PWDECLINEURL>https://www.clientdomain.com/decline</PWDECLINEURL>
        <ISCUP>TRUE</ISCUP>
        <LANGUAGE>CHS</LANGUAGE>
        <FORCESAVETOKEN>FALSE</FORCESAVETOKEN>
     </TRANSACTIONHEADER>
     <CUSTOMER>
     <FIRSTNAME>John</FIRSTNAME>
     <LASTNAME>Doe</LASTNAME>
     <PRIMARYPHONE>86-13012345678</PRIMARYPHONE>
     <PWCTRANSTYPE>4</PWCTRANSTYPE>
     <PWCID>P0000007087</PWCID>
     </CUSTOMER>
  </PAYMENTREQUEST>
```

> CUP OSBP #4 Request transaction notes:
> 
>  PWCID from CUP OSBP #2.

> CUP OSBP #4: Response: Secure Plus Sale and add new token to existing customer (with PWCID) - Paywire to manage the customer saved token. Merchant only manage Customer ID (PWCID):

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <RESULT>APPROVAL</RESULT>
        <AMOUNT>10.00</AMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <MACCOUNT>XXXXXXXXXXXX0017</MACCOUNT>
        <PAYMETH>C</PAYMETH>
        <CCTYPE>CUP</CCTYPE>
        <AHNAME>John Doe</AHNAME>
        <AHFIRSTNAME>John</AHFIRSTNAME>
        <AHLASTNAME>Doe</AHLASTNAME>
        <PWUNIQUEID>191601</PWUNIQUEID>
        <BATCHID>691</BATCHID>
        <AUTHCODE></AUTHCODE>
        <PWTOKEN>TFC0206A817A7459</PWTOKEN>
        <CVVCODE> </CVVCODE>
        <PWCUSTOMID2>000000192085</PWCUSTOMID2>
        <PWCID>P0000007087</PWCID>
  </PAYMENTRESPONSE>
```

> CUP OSBP #5: Request: Token Sale - with PWCID:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <AUTHTOKEN>{authtoken}</AUTHTOKEN>
        <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <PWAPPROVALURL>https://www.clientdomain.com/approval</PWAPPROVALURL>
        <PWDECLINEURL>https://www.clientdomain.com/decline</PWDECLINEURL>
        <ISCUP>TRUE</ISCUP>
        <LANGUAGE>ENG</LANGUAGE>
        <FORCESAVETOKEN>FALSE</FORCESAVETOKEN>
     </TRANSACTIONHEADER>
     <CUSTOMER>
     <FIRSTNAME>John</FIRSTNAME>
     <LASTNAME>Doe</LASTNAME>
     <PRIMARYPHONE>86-13012345678</PRIMARYPHONE>
     <PWCTRANSTYPE>2</PWCTRANSTYPE>
     <PWCID>P0001272164</PWCID>
     </CUSTOMER>
  </PAYMENTREQUEST>
```

> CUP OSBP #5 Request transaction notes:
> 
>  PWCID from CUP OSBP #2.

> CUP OSBP #5: Response: Token Sale - with PWCID:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <RESULT>APPROVAL</RESULT>
        <AMOUNT>10.00</AMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <MACCOUNT>XXXXXXXXXXXX0017</MACCOUNT>
        <PAYMETH>C</PAYMETH>
        <CCTYPE>CUP</CCTYPE>
        <AHNAME>John Doe</AHNAME>
        <AHFIRSTNAME>John</AHFIRSTNAME>
        <AHLASTNAME>Doe</AHLASTNAME>
        <PWUNIQUEID>191601</PWUNIQUEID>
        <BATCHID>696</BATCHID>
        <AUTHCODE></AUTHCODE>
        <PWTOKEN>TFC0206A817A7459</PWTOKEN>
        <CVVCODE> </CVVCODE>
        <PWCUSTOMID2>000000192085</PWCUSTOMID2>
        <PWCID>P0001272164</PWCID>
  </PAYMENTRESPONSE>
```

> CUP OSBP #6: Request: Non Secure Plus Recurring/Customer Sale - First Transaction:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <AUTHTOKEN>{authtoken}</AUTHTOKEN>
        <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <PWAPPROVALURL>https://www.clientdomain.com/approval</PWAPPROVALURL>
        <PWDECLINEURL>https://www.clientdomain.com/decline</PWDECLINEURL>
        <ISCUP>TRUE</ISCUP>
        <LANGUAGE>ENG</LANGUAGE>
        <FORCESAVETOKEN>TRUE</FORCESAVETOKEN>
        <NONSECUREPLUS>TRUE</NONSECUREPLUS>
     </TRANSACTIONHEADER>
     <CUSTOMER>
     <FIRSTNAME>John</FIRSTNAME>
     <LASTNAME>Doe</LASTNAME>
     <PRIMARYPHONE>86-13012345678</PRIMARYPHONE>
     <PWCTRANSTYPE>4</PWCTRANSTYPE>
     </CUSTOMER>
  </PAYMENTREQUEST>
```

> CUP OSBP #6: Response: Non Secure Plus Recurring/Customer Sale - First Transaction:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <RESULT>APPROVAL</RESULT>
        <AMOUNT>10.00</AMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <MACCOUNT>XXXXXXXXXXXX0017</MACCOUNT>
        <PAYMETH>C</PAYMETH>
        <CCTYPE>CUP</CCTYPE>
        <AHNAME>John Doe</AHNAME>
        <AHFIRSTNAME>John</AHFIRSTNAME>
        <AHLASTNAME>Doe</AHLASTNAME>
        <PWUNIQUEID>191601</PWUNIQUEID>
        <BATCHID>696</BATCHID>
        <AUTHCODE></AUTHCODE>
        <PWTOKEN>TFC0206A817A7459</PWTOKEN>
        <CVVCODE> </CVVCODE>
        <PWCUSTOMID2>000000192085</PWCUSTOMID2>
        <PWCID>P000000A074</PWCID>
  </PAYMENTRESPONSE>
```

> CUP OSBP #7: Request: Non Secure Plus Recurring/Customer Sale - Subsequent Transaction:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTREQUEST>
     <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <AUTHTOKEN>{authtoken}</AUTHTOKEN>
        <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10.00</PWSALEAMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <PWAPPROVALURL>https://www.clientdomain.com/approval</PWAPPROVALURL>
        <PWDECLINEURL>https://www.clientdomain.com/decline</PWDECLINEURL>
        <ISCUP>TRUE</ISCUP>
        <LANGUAGE>ENG</LANGUAGE>
        <NONSECUREPLUS>TRUE</NONSECUREPLUS>
     </TRANSACTIONHEADER>
     <CUSTOMER>
     <FIRSTNAME>John</FIRSTNAME>
     <LASTNAME>Doe</LASTNAME>
     <PRIMARYPHONE>86-13012345678</PRIMARYPHONE>
     <PWCTRANSTYPE>1</PWCTRANSTYPE>
     <PWTOKEN>TFC0206A817A7459</PWTOKEN>
     <PWUNIQUEID>191601</PWUNIQUEID>
     </CUSTOMER>
  </PAYMENTREQUEST>
```

> CUP OSBP #7 Request transaction notes:
> 
>  PWTOKEN and PWUNIQUEID from CUP OSBP #6

> CUP OSBP #7: Response: Non Secure Plus Recurring/Customer Sale - Subsequent Transaction:

```
<?xml version="1.0" encoding="UTF-8"?>
  <PAYMENTRESPONSE>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWINVOICENUMBER>testcup001</PWINVOICENUMBER>
        <RESULT>APPROVAL</RESULT>
        <AMOUNT>10.00</AMOUNT>
        <CURRENCY>CNY</CURRENCY>
        <MACCOUNT>XXXXXXXXXXXX0017</MACCOUNT>
        <PAYMETH>C</PAYMETH>
        <CCTYPE>CUP</CCTYPE>
        <AHNAME>John Doe</AHNAME>
        <AHFIRSTNAME>John</AHFIRSTNAME>
        <AHLASTNAME>Doe</AHLASTNAME>
        <PWUNIQUEID>191601</PWUNIQUEID>
        <BATCHID>696</BATCHID>
        <AUTHCODE></AUTHCODE>
        <PWTOKEN>TFC0206A817A7459</PWTOKEN>
        <CVVCODE> </CVVCODE>
        <PWCUSTOMID2>000000192085</PWCUSTOMID2>
   </PAYMENTRESPONSE>
```

The UnionPay OSBP is multilingual and supports the display and processing of multiple currencies. Please see the table below for a list of fields that are specific to the UnionPay OSBP:

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| AUTHTOKEN | ✓ | string | The Authentication Token to be used when calling the [OSBP](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#authentication). |  |
| ISCUP | ✓ | string | Must set to TRUE for UnionPay transactions. | `TRUE` |
| CURRENCY | ✓ | string | Set the transaction currency. | `USD`/`CNY`/`EUR` |
| LANGUAGE | ✓ | string | Switch language for the CUP OSBP. | `CHS`/`ENG` |
| FORCESAVETOKEN | ✓ | string | Available for PWCTRANSTYPE = 2 or 4, default is TRUE. Set to FALSE to allow the customer to decide save the token or not. | `TRUE`/`FALSE` |
| NONSECUREPLUS | ✓ | string | Only available for PWCTRANSTYPE = 1 or 4. The user must manage the customer token and the transaction ID of the first transaction. Set to TRUE if the client wants to process token sales as non-secureplus with no SMS code verification for subsequent transactions. Debit card not supported. When this is set to TRUE, user must provide the PWUNIQUEID of the first transaction in the subsequent transaction request. | `TRUE`/`FALSE` |

UnionPay Test Cards:

Credit Card: 

 6222821234560017, Phone Number: 86-13012345678 

 Debit:

 6250946000000016, Phone Number: 852-11112222 

 Exp: 12/33 

 CVV: 123 

 SMS Code: 111111

OSBP Brazil
-----------

The Brazil OSBP has two fields specific to this payment type:

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| BR_DOCUMENT | ✓ | string | For Brazilians, the expected bank slip document will be C.P.F. or CNPJ - Data format can be 023.472.201-01 or 02347220101. |  |
| TOTALINSTALLMENTS |  | string | The total number of installment payments for a periodic payment. |  |

OCX Reference
-------------

The Paywire OCX provides a few methods which easily enables an application to communicate with the Paywire API. This can either capture the payment data from an Ingenico reader or using an in-built UI form.

Compatible readers:

*   Ingenico Lane3000
*   IDTech SRED2

OCX Installation
----------------

The latest version of the OCX can be downloaded [here](https://project.paywire.com/download.html).

Once downloaded:

*   Run `Install.bat` as Administrator.

**OR**

*   Run `pwIngOCXSetup.msi` as Administrator.

OCX Using Reader
----------------

```
private void button1_Click(object sender, EventArgs e)
{
  var xmlStr = $"<PWTYPE>XML</PWTYPE>
                <TRANSTYPE>SALE</TRANSTYPE>
                <CONNECTTO>COM5</CONNECTTO>
                <PWCLIENTID>{clientId}</PWCLIENTID>
                <PWKEY>{key}</PWKEY>
                <PWUSER>{username}</PWUSER>
                <PWPASS>{password}</PWPASS>
                <AMOUNT>10.00</AMOUNT>
                <URL>dbstage1</URL>";
  var result = axpwIngenicoOCX1.Ingenico_ProcessOnDemandOCX(xmlStr);

  Console.WriteLine(result);
}
```

[![Image 3: OCX Reader Process Flowchart](https://project.paywire.com/dbtranz/docs/OSBP/files/images/pwocxflowreaderv1.png)](https://project.paywire.com/dbtranz/docs/OSBP/files/images/pwocxflowreaderv1.png)

To implement the OCX in your application:

1.   Add the `pwIngenicoOCX Control` to your list of UI components, available in the COM Components list once installed.
2.   Insert the `pwIngenicoOCX Control` in your GUI. This enables you to utilize the `AxpwIngenicoOCX` class containing method `Ingenico_ProcessOnDemandOCX(string pARAS)`
3.   Bind a button (or otherwise) from your form that calls `axpwIngenicoOCX.Ingenico_ProcessOnDemandOCX(string pARAS)`. An XML string based on the type of payment to be processed needs to be passed to this method.
4.   The `Ingenico_ProcessOnDemandOCX` method will return an XML string with the results.

To process a transaction:

1.   Click the button (or otherwise) that calls `axpwIngenicoOCX.Ingenico_ProcessOnDemandOCX(string pARAS)`.
2.   Complete the payment using a reader.
3.   The OCX will return a response based on the result of the request.

#### Visual Studio 2017 Example:

1.   Install the OCX as explained [here](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#ocx-installation).
2.   Navigate to the project to which you want to add the OCX.
3.   Open a Form in a 'Design' view.
4.   Open your Toolbox, right-click and click "Choose Items".
5.   Switch to the "COM Components" tab and locate the "pwIngenicoOCX Control" component. If not found you may need to browse for it.
6.   Tick the "pwIngenicoOCX Control" and it should now be available to use in the Toolbox.
7.   Add the "pwIngenicoOCX Control" component to your form, along with a button.
8.   Navigate to view the button code (double-click the button from the design page).
9.   Modify the button's event handler method to call `axpwIngenicoOCX.Ingenico_ProcessOnDemandOCX(string pARAS)`, passing in the relevant XML request payload in the available parameter.

### Transaction Processing

> Sale Request Example:

```
<PWTYPE>XML</PWTYPE>
  <TRANSTYPE>SALE</TRANSTYPE>
  <CONNECTTO>COM5</CONNECTTO>
  <PWCLIENTID>{clientId}</PWCLIENTID>
  <PWKEY>{key}</PWKEY>
  <PWUSER>{user}</PWUSER>
  <PWPASS>{pass}</PWPASS>
  <AMOUNT>1.00</AMOUNT>
  <URL>dbstage1</URL>
```

> Void Request Example:

```
<PWTYPE>XML</PWTYPE>
  <TRANSTYPE>VOID</TRANSTYPE>
  <CONNECTTO>COM5</CONNECTTO>
  <PWCLIENTID>{clientId}</PWCLIENTID>
  <PWKEY>{key}</PWKEY>
  <PWUSER>{user}</PWUSER>
  <PWPASS>{pass}</PWPASS>
  <AMOUNT>1.00</AMOUNT>
  <URL>dbstage1</URL>
  <UID>11373</UID>
```

> Store Token Request Example:

```
<PWTYPE>XML</PWTYPE>
  <TRANSTYPE>STORETOKEN</TRANSTYPE>
  <CONNECTTO>COM5</CONNECTTO>
  <PWCLIENTID>{clientId}</PWCLIENTID>
  <PWKEY>{key}</PWKEY>
  <PWUSER>{user}</PWUSER>
  <PWPASS>{pass}</PWPASS>
  <AMOUNT>0.00</AMOUNT>
  <URL>dbstage1</URL>
```

> Token Sale Request Example:

```
<PWTYPE>XML</PWTYPE>
  <TRANSTYPE>TOKENSALE</TRANSTYPE>
  <CONNECTTO>COM5</CONNECTTO>
  <PWCLIENTID>{clientId}</PWCLIENTID>
  <PWKEY>{key}</PWKEY>
  <PWUSER>{user}</PWUSER>
  <PWPASS>{pass}</PWPASS>
  <AMOUNT>1.00</AMOUNT>
  <URL>dbstage1</URL>
  <TOKEN>0A20D1AE85BC6E3F</TOKEN>
```

To process different transaction types, submit the relevant value in parameter `TRANSTYPE` along with the required parameters as defined below:

| Parameter | Type | Description | Validation |
| --- | --- | --- | --- |
| PWTYPE | string |  | `XML` |
| TRANSTYPE | string | Defines what transaction to process. | `SALE`, `PREAUTH`, `CREDIT`, `VOID`, `STORETOKEN`, `REMOVETOKEN`, `TOKENSALE`, `TOKENPREAUTH`, `TOKENCREDIT`, `BATCHINQUIRY`, `CLOSE` |
| CONNECTTO | string | The COM port or TCP port that the reader is connected to. | `COM`, `TCP` |
| PWCLIENTID | string | Authentication credential provided to you by the administrator. |  |
| PWKEY | string | Authentication credential provided to you by the administrator. |  |
| PWUSER | string | Authentication credential provided to you by the administrator. |  |
| PWPASS | string | Authentication credential provided to you by the administrator. |  |
| AMOUNT | decimal | Amount of transaction being processed. |  |
| URL | string | Sub-domain of the Paywire environment to which you want to process. | `dbstage1`, `dbtranz` |
| UID | int | Unique ID of original transaction. |  |
| POSTTIMEOUT | int | Time out in seconds for the API request from the OCX to the Paywire Gateway | Default: `120` |
| ISDEBIT | int | Defines if card is a debit card. `1` indicates that it is. | `0`, `1` ; Default: `0` |
| ISEMVFALLBACK | int | Set this option to 1 for the reader to allow the customer to swipe the card in cases where the EMV chip is broken. | `0`, `1` |
| ENTRYTIMEOUT | int | Time out in seconds to wait for the customer to complete input on each page displayed by the reader. | Default: `60` |
| ALLOWZIP | int | Submitting `1` will set the reader to display a ZIP Code input screen to the customer when they opt to key-in the card number. | `0`, `1` |
| INVOICE | string | The merchant's unique invoice number associated with this transaction. |  |
| ADDRESS1 | string | Account Holder's address line 1. |  |
| ADDRESS2 | string | Account Holder's address line 2. |  |
| CITY | string | Account Holder's city of residence. |  |
| COUNTRY | string | Account Holder's country of residence. |  |
| COUNTY | string | Account Holder's county of residence. |  |
| NAME | string | Account Holder's name. |  |
| ZIP | int | Account Holder's address postal/zip code: See important note on [Zip Codes](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#Zip-codes). |  |
| STATE | string | Account Holder's state of residence. |  |
| EMAIL | string | Account Holder's email address. |  |
| PHONE | int | Account Holder's contact phone number. |  |
| TOKEN | string | Token returned by Paywire when performing a `STORETOKEN` request. |  |
| AUTHCODE | string | Authorization code returned by the Paywire gateway or elsewhere. |  |

#### Required Parameters per Transaction Type

| Parameter | SALE | PREAUTH | CREDIT | VOID | STORETOKEN | REMOVETOKEN | TOKENSALE | TOKENPREAUTH | TOKENCREDIT | BATCHINQUIRY | CLOSE |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| PWTYPE | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |
| TRANSTYPE | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |
| CONNECTTO | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |
| PWCLIENTID | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |
| PWKEY | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |
| PWUSER | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |
| PWPASS | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |
| AMOUNT | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |
| URL | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |
| UID |  |  |  | ✓ |  |  |  |  |  |  |  |
| POSTTIMEOUT |  |  |  |  |  |  |  |  |  |  |  |
| ISDEBIT |  |  |  |  |  |  |  |  |  |  |  |
| ISEMVFALLBACK |  |  |  |  |  |  |  |  |  |  |  |
| ENTRYTIMEOUT |  |  |  |  |  |  |  |  |  |  |  |
| ALLOWZIP |  |  |  |  |  |  |  |  |  |  |  |
| INVOICE |  |  |  |  |  |  |  |  |  |  |  |
| ADDRESS1 |  |  |  |  |  |  |  |  |  |  |  |
| ADDRESS2 |  |  |  |  |  |  |  |  |  |  |  |
| CITY |  |  |  |  |  |  |  |  |  |  |  |
| COUNTRY |  |  |  |  |  |  |  |  |  |  |  |
| COUNTY |  |  |  |  |  |  |  |  |  |  |  |
| NAME |  |  |  |  |  |  |  |  |  |  |  |
| ZIP |  |  |  |  |  |  |  |  |  |  |  |
| STATE |  |  |  |  |  |  |  |  |  |  |  |
| EMAIL |  |  |  |  |  |  |  |  |  |  |  |
| PHONE |  |  |  |  |  |  |  |  |  |  |  |
| TOKEN |  |  |  |  |  | ✓ | ✓ | ✓ | ✓ |  |  |
| AUTHCODE |  |  |  |  |  |  |  |  |  |  |  |

### OCX Responses using Reader

**Errors**

| Error Code | Error Message | Description | String Example |
| --- | --- | --- | --- |
| 701 | CONNECT ERROR | Returned when a reader is not connected via the method provided in the `CONNECTTO` parameter. | `<RESULT>ERROR</RESULT>` `<ERRORCODE>701</ERRORCODE>` `<ERRORMESSAGE>CONNECT ERROR</ERRORMESSAGE>` |

OCX Using Form
--------------

```
public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            axpwIngenicoOCX.Visible = false;    // Always make sure to initialize the OCX parameter before clicking the OCX control button                                                                                                   
            axpwIngenicoOCX.DialogTransEvent += new AxpwIngenicoOCXLib._DpwIngenicoOCXEvents_DialogTransEventEventHandler(AxpwIngenicoOCX_DialogTransEvent);    // This is the response event of the OCX Payment UI
        }

        private void AxpwIngenicoOCX_DialogTransEvent(object sender, AxpwIngenicoOCXLib._DpwIngenicoOCXEvents_DialogTransEventEvent e)
        {
            txtResponse.Text = e.responsMsg;    // Handle Payment Response
        }

        private void buttonInit_Click(object sender, EventArgs e)
        {
            axpwIngenicoOCX.Visible = true;
            axpwIngenicoOCX.Ocx_Init(txtRequest.Text);
        }
    }
```

[![Image 4: OCX Form Process Flowchart](https://project.paywire.com/dbtranz/docs/OSBP/files/images/pwocxflowformv1.png)](https://project.paywire.com/dbtranz/docs/OSBP/files/images/pwocxflowformv1.png)

To implement the OCX in your application:

1.   Add the `pwIngenicoOCX Control` to your list of UI components, available in the COM Components list once installed.
2.   Insert the `pwIngenicoOCX Control` in your GUI. This enables you to utilize the `AxpwIngenicoOCX` class containing method `Ocx_Init(string pARAS)`.
3.   Bind a button (or otherwise) from your form that calls `axpwIngenicoOCX.Ocx_Init(string pARAS)`. An XML string based on the type of payment to be processed needs to be passed to this method.
4.   Add an event handler method for `AxpwIngenicoOCXLib._DpwIngenicoOCXEvents_DialogTransEventEvent`, which will include `responsMsg` as the XML payment response.

To process a transaction:

1.   Click the button (or otherwise) that calls `axpwIngenicoOCX.Ocx_Init(string pARAS)`.
2.   Click the `AxpwIngenicoOCX` control on your form to pull up the OCX form.
3.   Populate the fields on the OCX form and click to process.
4.   The OCX will return a response based on the result of the request that is captured by your event handler.

#### Visual Studio 2017 Example:

1.   Install the OCX as explained [here](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#ocx-installation).
2.   Navigate to the project to which you want to add the OCX.
3.   Open a Form in a 'Design' view.
4.   Open your Toolbox, right-click and click "Choose Items".
5.   Switch to the "COM Components" tab and locate the "pwIngenicoOCX Control" component. If not found you may need to browse for it.
6.   Tick the "pwIngenicoOCX Control" and it should now be available to use in the Toolbox.
7.   Add the "pwIngenicoOCX Control" component to your form, along with a button.
8.   Navigate to view the button code (double-click the button from the design page).
9.   Modify the button's event handler method to call `axpwIngenicoOCX.Ocx_Init(string pARAS)`, passing in the relevant XML request payload in the available parameter.
10.   If using the UI form (`Ocx_Init`), add an event handler for `AxpwIngenicoOCXLib._DpwIngenicoOCXEvents_DialogTransEventEvent` to handle the OCX response.

### Transaction Types

The XML structure for the OCX is like that of the API, including a `TRANSACTIONHEADER` and a `CUSTOMER` block.

Only following transactions can be processed using the Paywire OCX.

| Value | Description |
| --- | --- |
| SALE | Charge a card or bank account (if applicable). |
| STORETOKEN | Validate a card and return a token. |

Simply submit the relevant value in `PWTRANSACTIONTYPE`, along with the required XML parameters.

### OCX One-Time-Sale

> One-Time-Sale Request Example:

```
<PAYMENTREQUEST>
  <TRANSACTIONHEADER>
    <PWVERSION>3</PWVERSION>
    <PWUSER>{user}</PWUSER>
    <PWPASS>{password}</PWPASS>
    <PWCLIENTID>{client_id}</PWCLIENTID>
    <PWKEY>{key}</PWKEY>
    <URL>dbstage1</URL>
    <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
    <PWSALEAMOUNT>10</PWSALEAMOUNT>
    <PWINVOICENUMBER>0987654321234567800</PWINVOICENUMBER>
  </TRANSACTIONHEADER>
  <CUSTOMER>
    <FIRSTNAME>John</FIRSTNAME>
    <LASTNAME>Doe</LASTNAME>
    <EMAIL>jd@example.com</EMAIL>
    <STATE>CT</STATE>
    <ZIP>06105</ZIP>
    <PWMEDIA>CC</PWMEDIA>
    <REQUESTTOKEN>TRUE</REQUESTTOKEN>
  </CUSTOMER>
</PAYMENTREQUEST>
```

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `SALE` |
| PWCLIENTID | ✓ | string | Authentication credential provided to you by the administrator. |  |
| PWKEY | ✓ | string | Authentication credential provided to you by the administrator. |  |
| PWUSER | ✓ | string | Authentication credential provided to you by the administrator. |  |
| PWPASS | ✓ | string | Authentication credential provided to you by the administrator. |  |
| URL | ✓ | string | Sub-domain of the Paywire environment to which you want to process. | `local`, `dbstage1`, `dbtranz` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `SALE`, `STORETOKEN` |
| PWSALEAMOUNT | ✓ | int/decimal | Payment amount to be processed. |  |
| PWINVOICENUMBER |  | string | The merchant's unique invoice number associated with this transaction. If not submitted, this will be generated by the gateway and returned in the XML response. | `0/60` |
| FIRSTNAME |  | string | Account Holder's first name. |  |
| LASTNAME |  | string | Account Holder's last name. |  |
| ADDRESS1 |  | string | Account Holder's primary address. |  |
| ADDRESS2 |  | string | Account Holder's secondary address. |  |
| CITY |  | string | Account Holder's city of residence. |  |
| STATE | (✓) | string | Account Holder's state of residence. Required if configured with [Convenience Fees](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#convenience-fees). |  |
| ZIP |  | string | Account Holder's address postal/zip code. See important note on [Zip Codes](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#Zip-codes). |  |
| COUNTRY |  | string | Account Holder's country of residence. |  |
| PRIMARYPHONE |  | string | Account Holder's primary phone number. |  |
| WORKPHONE |  | string | Account Holder's work phone number. |  |
| EMAIL |  | string | Account Holder's email address. |  |
| PWMEDIA | ✓ | string | Defines the payment method. | `CC`, `ECHECK` |
| REQUESTTOKEN |  | bool | Returns a `PWTOKEN` in the response when set to `TRUE`. |  |
| DISABLECF |  | bool | Overrides applying a Convenience Fee or Cash Discount when set to `TRUE`, if configured. Note that Sales Tax will also be disabled. | Default: `FALSE` |
| ADJTAXRATE |  | decimal | Overrides the configured Sales Tax rate. |  |
| PWCUSTOMID1 |  | string | Custom third-party ID to be associated with this transaction. |  |
| DESCRIPTION |  | string | Transaction custom description message. | `0/100` |
| PWCID |  | string | Paywire Customer Identifier. If `REQUESTTOKEN` is also submitted as `TRUE`, the created token will be associated with this customer. |  |

### OCX Periodic Sale

> Periodic Sale Request Example:

```
<?xml version="1.0"?>
<PAYMENTREQUEST>
  <TRANSACTIONHEADER>
    <PWVERSION>3</PWVERSION>
    <PWUSER>{user}</PWUSER>
    <PWPASS>{password}</PWPASS>
    <PWCLIENTID>{client_id}</PWCLIENTID>
    <PWKEY>{key}</PWKEY>
    <URL>dbstage1</URL>
    <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
    <PWSALEAMOUNT>10</PWSALEAMOUNT>
    <PWINVOICENUMBER>0987654321234567801</PWINVOICENUMBER>
  </TRANSACTIONHEADER>
  <CUSTOMER>
    <FIRSTNAME>John</FIRSTNAME>
    <LASTNAME>Doe</LASTNAME>
    <EMAIL>jd@example.com</EMAIL>
    <STATE>CT</STATE>
    <ZIP>06105</ZIP>
    <PWMEDIA>ECHECK</PWMEDIA>
  </CUSTOMER>
  <RECURRING>
    <STARTON>2/6/2019 12:00:00 AM</STARTON>
    <FREQUENCY>M</FREQUENCY>
    <PAYMENTS>3</PAYMENTS>
  </RECURRING>
</PAYMENTREQUEST>
```

In order to create a Periodic setup, simply include the `<RECURRING>` block in addition to the [One-Time-Sale parameters](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#ocx-one-time-sale).

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| STARTON | ✓ | Date | Date the first payment must be charged. | `mm/dd/yyyy hh:mm:ss a`. |
| FREQUENCY | ✓ | string | The frequency with which periodic payments are charged. | `W`: Weekly `B`: Bi-weekly `M`: Monthly `H`: Semi-monthly `Q`: Quarterly `S`: Semi-annual `Y`: Yearly |
| PAYMENTS | ✓ | int | Number of payments to process until the periodic setup is expired. | `1/999` |
| EMAIL |  | string | Account Holder's email address. Required either in the XML payload to the `Ocx_Init` method or submitted via the form. |  |

### OCX Store Token

> Store Token Request Example:

```
<PAYMENTREQUEST>
  <TRANSACTIONHEADER>
    <PWVERSION>3</PWVERSION>
    <PWUSER>{user}</PWUSER>
    <PWPASS>{password}</PWPASS>
    <PWCLIENTID>{client_id}</PWCLIENTID>
    <PWKEY>{key}</PWKEY>
    <URL>dbstage1</URL>
    <PWTRANSACTIONTYPE>STORETOKEN</PWTRANSACTIONTYPE>
    <PWSALEAMOUNT>0</PWSALEAMOUNT>
    <PWINVOICENUMBER>0987654321234567802</PWINVOICENUMBER>
  </TRANSACTIONHEADER>
  <CUSTOMER>
    <FIRSTNAME>John</FIRSTNAME>
    <LASTNAME>Doe</LASTNAME>
    <EMAIL>jd@example.com</EMAIL>
    <STATE>CT</STATE>
    <ZIP>06105</ZIP>
    <PWMEDIA>CC</PWMEDIA>
    <REQUESTTOKEN>TRUE</REQUESTTOKEN>
  </CUSTOMER>
</PAYMENTREQUEST>
```

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWVERSION | ✓ | int | The Paywire Gateway version number. | `3` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `STORETOKEN` |
| PWCLIENTID | ✓ | string | Authentication credential provided to you by the administrator. |  |
| PWKEY | ✓ | string | Authentication credential provided to you by the administrator. |  |
| PWUSER | ✓ | string | Authentication credential provided to you by the administrator. |  |
| PWPASS | ✓ | string | Authentication credential provided to you by the administrator. |  |
| URL | ✓ | string | URL subdomain of the Paywire environment to which you want to process. | `local`, `dbstage1`, `dbtranz` |
| PWTRANSACTIONTYPE | ✓ | string | Defines what transaction to process. | `SALE`, `STORETOKEN` |
| PWSALEAMOUNT |  | int/decimal | Amount of transaction being processed. |  |
| PWINVOICENUMBER |  | string | The merchant's unique invoice number associated with this transaction. If not submitted, this will be generated by the gateway and returned in the XML response. | 0/60 |
| FIRSTNAME |  | string | Account Holder's first name. |  |
| LASTNAME |  | string | Account Holder's last name. |  |
| ZIP |  | string | Account Holder's address postal/zip code. See important note on [Zip Codes](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#Zip-codes). |  |
| STATE |  | string | Account Holder's state of residence. |  |
| EMAIL |  | string | Account Holder's email address. |  |
| PWMEDIA | ✓ | string | Defines the payment method. | `CC`, `ECHECK` |
| PWCID |  | string | Paywire Customer Identifier. If `REQUESTTOKEN` is also submitted as `TRUE`, the created token will be associated with this customer. |  |

### OCX Token Sale

> Token Sale Request Example:

```
<PAYMENTREQUEST>
  <TRANSACTIONHEADER>
    <PWVERSION>3</PWVERSION>
    <PWUSER>{user}</PWUSER>
    <PWPASS>{password}</PWPASS>
    <PWCLIENTID>{client_id}</PWCLIENTID>
    <PWKEY>{key}</PWKEY>
    <URL>dbstage1</URL>
    <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
    <PWSALEAMOUNT>10</PWSALEAMOUNT>
    <PWINVOICENUMBER>0987654321234567803</PWINVOICENUMBER>
  </TRANSACTIONHEADER>
  <CUSTOMER>
    <PWTOKEN>A00001xa</PWTOKEN>
    <FIRSTNAME>John</FIRSTNAME>
    <LASTNAME>Doe</LASTNAME>
    <EMAIL>jd@example.com</EMAIL>
    <STATE>CT</STATE>
    <ZIP>06105</ZIP>
    <PWMEDIA>ECHECK</PWMEDIA>
  </CUSTOMER>
</PAYMENTREQUEST>
```

To process a Sale for a pre-existing card, account, or customer, include the `PWTOKEN` value in parameter `PWTOKEN` in place of the Card or Bank Account details. For a Periodic Sale include the `RECURRING` block at the end as you would for a normal Periodic Sale.

| Parameter | Required | Type | Description | Validation |
| --- | --- | --- | --- | --- |
| PWTOKEN | ✓ | string | Can either represent a Card Token or the Paywire Customer ID |  |
| POSINDICATOR |  | string | Used in conjunction with Token Sales to apply Convenience Fees or Cash Discount for periodic payments handled outside Paywire. Submit this in the `TRANSACTIONHEADER` block. | `C`: Regular Token Sale `I`: First Payment of a Periodic Plan `R`: Subsequent Periodic Payment `T`: Last Payment of a Periodic Plan `P`: Periodic Payment |
| PWADJAMOUNT |  | decimal | Adjustment amount. Used to set the Convenience Fee amount to be charged for this transaction. Allowed only when submitted with `POSINDICATOR` set to `P`. Submitting amounts larger than that configured for the merchant will be ignored. | `>0` |

### OCX Responses using Form

> One-Time-Sale Approved Response Example:

```
<PAYMENTRESPONSE>
  <RESULT>APPROVAL</RESULT>
  <BATCHID>1</BATCHID>
  <PWCLIENTID>0000000001</PWCLIENTID>
  <AUTHCODE>123456</AUTHCODE>
  <AVSCODE>N</AVSCODE>
  <CVVCODE>N</CVVCODE>
  <PAYMETH>C</PAYMETH>
  <PWUNIQUEID>895362</PWUNIQUEID>
  <AHNAME>John Doe</AHNAME>
  <PWADJDESC>Convenience Fee</PWADJDESC>
  <PWSALETAX>0.00</PWSALETAX>
  <PWADJAMOUNT>10.00</PWADJAMOUNT>
  <PWSALEAMOUNT>100.00</PWSALEAMOUNT>
  <AMOUNT>110.00</AMOUNT>
  <MACCOUNT>XXXXXXXXXXXX0608</MACCOUNT>
  <EMAIL>jd@sample.com</EMAIL>
  <CCTYPE>MC</CCTYPE>
  <PWTOKEN>841521B1BF514A4A3S34</PWTOKEN>
  <PWCUSTOMID2>24e59759870l3g9an3f4271beaas4g2a</PWCUSTOMID2>
  <PWINVOICENUMBER>0987654321234567800</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

> Periodic Sale Approved Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
  <RESULT>APPROVAL</RESULT>
  <BATCHID>1</BATCHID>
  <PWCLIENTID>0000000001</PWCLIENTID>
  <PAYMETH>C</PAYMETH>
  <PWUNIQUEID>123456</PWUNIQUEID>
  <AHNAME>John Doe</AHNAME>
  <AMOUNT>110.00</AMOUNT>
  <MACCOUNT>XXXXXXXXXXXX0608</MACCOUNT>
  <EMAIL>jd@sample.com</EMAIL>
  <CCTYPE>MC</CCTYPE>
  <PWTOKEN>0PH5A58C17F0491BS076</PWTOKEN>
  <PWCUSTOMID2>1v1e2b1e408v4cd6b8dsnr3bfcfd2a6</PWCUSTOMID2>
  <PWINVOICENUMBER>0987654321234567801</PWINVOICENUMBER>
  <RECURRINGID>48</RECURRINGID>
</PAYMENTRESPONSE>
```

> Token Sale Approved Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
  <RESULT>APPROVAL</RESULT>
  <BATCHID>182</BATCHID>
  <PWCLIENTID>0000000001</PWCLIENTID>
  <AUTHCODE>T66984</AUTHCODE>
  <AVSCODE>N</AVSCODE>
  <PAYMETH>C</PAYMETH>
  <PWUNIQUEID>131303</PWUNIQUEID>
  <AHNAME>John Doe</AHNAME>
  <PWADJDESC>Convenience Fee</PWADJDESC>
  <PWSALETAX>0.00</PWSALETAX>
  <PWADJAMOUNT>10.00</PWADJAMOUNT>
  <PWSALEAMOUNT>100.00</PWSALEAMOUNT>
  <AMOUNT>110.00</AMOUNT>
  <MACCOUNT>XXXXXXXXXXXX0608</MACCOUNT>
  <EMAIL>test@sample.com</EMAIL>
  <CCTYPE>MC</CCTYPE>
  <PWTOKEN>12FE3B1D364FF9B20156</PWTOKEN>
  <PWCUSTOMID2>743f4h616d4976818a6a130ef1c01a54</PWCUSTOMID2>
  <PWINVOICENUMBER>0987654321234567803</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

> Declined Response Example:

```
<PAYMENTRESPONSE>
  <RESULT>DECLINED</RESULT>
  <RESTEXT>CVV2 Mismatch</RESTEXT>
  <BATCHID>1</BATCHID>
  <PWCLIENTID>0000000001</PWCLIENTID>
  <AVSCODE>N</AVSCODE>
  <CVVCODE>N</CVVCODE>
  <PAYMETH>C</PAYMETH>
  <PWUNIQUEID>895363</PWUNIQUEID>
  <AHNAME>John Doe</AHNAME>
  <AMOUNT>110.00</AMOUNT>
  <MACCOUNT>XXXXXXXXXXXX0608</MACCOUNT>
  <EMAIL>jd@sample.com</EMAIL>
  <CCTYPE>MC</CCTYPE>
  <PWCUSTOMID2>24e59759870l3g9an3f4271beaas4g2a</PWCUSTOMID2>
  <PWINVOICENUMBER>0987654321234567800</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

> Error Response Example:

```
<PAYMENTRESPONSE>
  <RESULT>ERROR</RESULT>
  <RESTEXT>CONNECTION ERROR 0</RESTEXT>
</PAYMENTRESPONSE>
```

Much like the API, the OCX will return with an XML response containing `RESULT` and `RESTEXT`.

| Response Parameter | Description | Options |
| --- | --- | --- |
| RESULT | Describes the result of the transaction request. | `APPROVAL`, `SUCCESS`, `DECLINED`, `ERROR` |
| RESTEXT | Described why the transaction has returned an error or was declined. Not returned when `RESULT` is `APPROVAL` or `SUCCESS`. |  |

Errors
------

Convenience Fees
----------------

Paywire also offers a Convenience Fee solution with all integration options, wherein customers are charged a fixed fee per transaction, significantly cutting down merchants' processing costs.

For enrolled merchants, this is configured on the gateway by Paywire and will require that `STATE` is submitted with each payment. There are a few U.S. states that do not allow convenience fees. The gateway will by default assess a Convenience Fee if the `STATE` submitted is not one these.

The [Paywire OSBP](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#osbp-reference) will display the below box throughout if the `STATE` submitted in the request is found to be a _"Fee-State"_.

[![Image 5: OSBP CF Box](https://project.paywire.com/dbtranz/docs/OSBP/files/images/osbp_cfbox.png)](https://project.paywire.com/dbtranz/docs/OSBP/files/images/osbp_cfbox.png)

The message at the bottom is configurable in the VPOS merchant admin page.

A Convenience Fee breakdown is displayed on the Payment Form:

[![Image 6: OSBP CF Breakdown](https://project.paywire.com/dbtranz/docs/OSBP/files/images/osbp_cfbreakdown.png)](https://project.paywire.com/dbtranz/docs/OSBP/files/images/osbp_cfbreakdown.png)

> Approved Response Example:

```
<PAYMENTRESPONSE>
...
    <AMOUNT>110.00</AMOUNT>
    <PWSALETAX>0.00</PWSALETAX>
    <PWADJAMOUNT>10.00</PWADJAMOUNT>
    <PWSALEAMOUNT>100.00</PWSALEAMOUNT>
...
</PAYMENTRESPONSE>
```

The OSBP and API responses will also include the Convenience Fee breakdown where relevant.

Two parameters are provided to override the setup via OSBP, API and OCX:

1.   `DISABLECF` - disables applying a 'Convenience Fee' for a given transaction.
2.   `ADJTAXRATE` - overrides the configured Tax Rate to the submitted value.

Merchant-Managed Periodic Billing
---------------------------------

> First Sale Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
   <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>100.00</PWSALEAMOUNT>
  </TRANSACTIONHEADER>
  <DETAILRECORDS />
  <CUSTOMER>
      <STATE>CT</STATE>
      <PWMEDIA>CC</PWMEDIA>
      <CARDNUMBER>4111111111111111</CARDNUMBER>
      <EXP_MM>02</EXP_MM>
      <EXP_YY>22</EXP_YY>
  </CUSTOMER>
</PAYMENTREQUEST>
```

> First Sale Response Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
  <RESULT>APPROVAL</RESULT>
  <BATCHID>1</BATCHID>
  <PWCLIENTID>0000000001</PWCLIENTID>
  <AUTHCODE>B1246A</AUTHCODE>
  <AVSCODE>N</AVSCODE>
  <PAYMETH>C</PAYMETH>
  <PWUNIQUEID>446331</PWUNIQUEID>
  <PWSALETAX>0.00</PWSALETAX>
  <PWADJAMOUNT>0.00</PWADJAMOUNT>
  <PWSALEAMOUNT>100.00</PWSALEAMOUNT>
  <AMOUNT>100.00</AMOUNT>
  <MACCOUNT>XXXXXXXXXXXX1111</MACCOUNT>
  <CCTYPE>VISA</CCTYPE>
  <PWTOKEN>EEF46WF47AC34341209</PWTOKEN>
  <PWCUSTOMID2>cd3d19cf2123dfe469df07a2c75c8245b</PWCUSTOMID2>
  <PWINVOICENUMBER>19181234094754503</PWINVOICENUMBER>
</PAYMENTRESPONSE>
```

When processing transactions with merchant-managed Periodic Billing Plans that utilize the Paywire Token feature, the merchant is responsible for storing the Convenience Fee amount charged on the first sale.

This same amount must be submitted in `PWADJAMOUNT` along with a `POSINDICATOR` value of `P`.

This ensures that in case the Convenience Fee configured for a given merchant changes during the course of the customer's Periodic Billing Plan, the Convenience Fee charged is not altered.

The following outlines the steps that can be taken for each Periodic Billing Plan:

1.   The merchant submits a [One-Time-Sale](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-one-time-sale) request (that includes `REQUESTTOKEN` set to `TRUE` if not using a card).
2.   The merchant stores the `PWADJAMOUNT` and `PWTOKEN` from the response to be used later.
3.   In the subsequent sales, the merchant submits a [Token Sale](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-token-sale) including the `PWADJAMOUNT` and `PWTOKEN` tags with the respective values stored in (2), along with a `POSINDICATOR` of `P`.

> Subsequent Sale Request Example:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTREQUEST>
  <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>100.00</PWSALEAMOUNT>
      <POSINDICATOR>P</POSINDICATOR>
      <DISABLECF>TRUE</DISABLECF>
  </TRANSACTIONHEADER>
  <CUSTOMER>
      <PWTOKEN>EEF2547BDAC34E642009</PWTOKEN>
      <PWMEDIA>CC</PWMEDIA>
      <STATE>AZ</STATE>
  </CUSTOMER>
</PAYMENTREQUEST>
```

Digital Wallet
--------------

Apple Pay
---------

### Apple Pay Introduction

Apple Pay provides an easy and secure way to make payments in your iOS, iPadOS, and watchOS apps, and on websites in Safari. By using Face ID, Touch ID, or double-clicking Apple Watch, users can quickly and securely provide their payment, shipping, and contact information to check out.

Apple Pay is available on all iOS devices with a Secure Element — an industry-standard, certified chip designed to store payment information safely. In iOS and macOS, users must have an Apple Pay-capable iPhone or Apple Watch to authorize the payment, or a Mac with Touch ID.

Payscout's Apple Pay integration supports the major card networks in the United States: Visa, Mastercard, Discover, Amex.

### Apple Pay Integration with OSBP

Apple Pay can be enabled for our hosted payment page solution - [_OSBP_](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#osbp-reference).

No addition dev work is required for any merchants using the OSBP solution, the Apple Pay option will be added to the OSBP payment page and will show up when the user's browser and device meet the Apple Pay requirement.

To enable Apple Pay for the OSBP, please send a request to [support@payscout.com](mailto:support@payscout.com) and our technical support team will review your merchant account and enable it for you.

### Apple Pay Integration with API

> Adding Apple Pay Payload to the Paywire API Request:

    
```
<PAYMENTREQUEST>
    <TRANSACTIONHEADER>
        <PWVERSION>3</PWVERSION>
        <PWCLIENTID>{clientId}</PWCLIENTID>
        <PWKEY>{key}</PWKEY>
        <PWUSER>{username}</PWUSER>
        <PWPASS>{password}</PWPASS>
        <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
        <PWSALEAMOUNT>10</PWSALEAMOUNT>
        <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
    </TRANSACTIONHEADER>
    <CUSTOMER>
        <PWMEDIA>CC</PWMEDIA>
        <COMPANYNAME>The Company</COMPANYNAME>
        <FIRSTNAME>John</FIRSTNAME>
        <LASTNAME>Doe</LASTNAME>
        <EMAIL>jd@example.com</EMAIL>
        <ADDRESS1>1 The Street</ADDRESS1>
        <CITY>New York</CITY>
        <STATE>NY</STATE>
        <ZIP>12345</ZIP>
        <COUNTRY>US</COUNTRY>
        <PRIMARYPHONE>1234567890</PRIMARYPHONE>
        <WORKPHONE>1234567890</WORKPHONE>
    </CUSTOMER>
    <DIGITALWALLET>
        <DWTYPE>A</DWTYPE>
        <DWPAYLOAD>{YOUR_PAYLOAD_FROM_DIGITALWALLET_Base64Encoded}</DWPAYLOAD>
    </DIGITALWALLET>
    </PAYMENTREQUEST>
```

  
If your website or payment application is not integrated with the OSBP solution, Apple Pay can also be integrated with our transaction API.

To implement the Apple Pay with API, please send a request to [support@payscout.com](mailto:support@payscout.com) and our technical support will help with the integration.

The requirements for using Apple Pay on your website:

1.   Your website must comply with the Apple Pay guidelines. For more information, see Acceptable Use Guidelines for [Apple Pay on the Web](https://developer.apple.com/apple-pay/acceptable-use-guidelines-for-websites/).
2.   You must have an Apple Developer Account and complete the registration. For more information, see [Configuring Your Environment](https://developer.apple.com/documentation/apple_pay_on_the_web/configuring_your_environment).
3.   All pages that include Apple Pay must be served over HTTPS. For more information, see [Setting Up Your Server](https://developer.apple.com/documentation/apple_pay_on_the_web/setting_up_your_server). 
4.   For design guidance, see [Human Interface Guidelines > Apple Pay](https://developer.apple.com/design/human-interface-guidelines/apple-pay/overview/introduction/).

The supported authorization methods for Apple Pay transactions will be the same as the [_API Sale Transactions_](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-one-time-sale).

To send the Apple Pay payload for a transaction request (see examples on the right side):

1.   Utilize the similar request message as the [_API Sale Transactions_](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-one-time-sale).
2.   Set `PWMEDIA` to `CC`.
3.   Remove the `CARDNUMBER`, `EXP_MM`, `EXP_YY`, `CVV2` fields and add the `DIGITALWALLET` group to the transaction request. For field reference: [_Digital Wallet Data Group_](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-digital-wallet-group)
4.   Set `DWTYPE` to `A`.
5.   Fill in the encrypted payload to the `DWPAYLOAD` field.

The Paywire transaction API will decrypt the payload and process the transaction. The card brand type, last four of the card number and other transaction details will be returned the same way as the regular transactions.

The billing address are not mandatory by default, however we could make certain fields mandatory based on your use case.

Google Pay™
-----------

### Google Pay™ Introduction

Google Pay works on Android and any chromium-based web browser. To learn more about it, go to the following link: [https://pay.google.com/intl/en_us/about/](https://pay.google.com/intl/en_us/about/)

Google Pay cannot be used for in-app purchase, it can only be used for selling physical goods and services your business provides. Please refer to Google in-app purchases documentation: [https://developer.android.com/distribute/best-practices/earn/in-app-purchases](https://developer.android.com/distribute/best-practices/earn/in-app-purchases)

Payscout's Google Pay integration supports the major card networks in the United States: Visa, Mastercard, Discover, Amex.

### Google Pay™ Integration with OSBP

Google Pay can be enabled for our hosted payment page solution - [_OSBP_](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#osbp-reference).

No addition dev work is required for any merchants using the OSBP solution, the Google Pay option will be added to the OSBP payment page and will show up when the user's browser and device meet the Google Pay requirement.

All merchants must adhere to the [Google Pay APIs Acceptable Use Policy](https://payments.developers.google.com/terms/aup) and accept the terms defined in the [Google Pay API Terms of Service](https://payments.developers.google.com/terms/sellertos).

To enable Google Pay for the OSBP, please reach out to [support@payscout.com](mailto:support@payscout.com) and our support team will review your merchant account and enable it for you.

### Google Pay™ Integration with API

> Google Pay Request:

  
```
const tokenizationSpecification = {
    type: 'PAYMENT_GATEWAY',
    parameters: {
    'gateway': 'payscout',
    'gatewayMerchantId': 'YOUR_GATEWAY_PWCLIENTID'
    }
   };
```

> Adding Google Pay Payload to the Paywire API Request:

  
```
<PAYMENTREQUEST>
  <TRANSACTIONHEADER>
      <PWVERSION>3</PWVERSION>
      <PWCLIENTID>{clientId}</PWCLIENTID>
      <PWKEY>{key}</PWKEY>
      <PWUSER>{username}</PWUSER>
      <PWPASS>{password}</PWPASS>
      <PWTRANSACTIONTYPE>SALE</PWTRANSACTIONTYPE>
      <PWSALEAMOUNT>10</PWSALEAMOUNT>
      <PWINVOICENUMBER>0987654321234567890</PWINVOICENUMBER>
  </TRANSACTIONHEADER>
  <CUSTOMER>
      <PWMEDIA>CC</PWMEDIA>
      <COMPANYNAME>The Company</COMPANYNAME>
      <FIRSTNAME>John</FIRSTNAME>
      <LASTNAME>Doe</LASTNAME>
      <EMAIL>jd@example.com</EMAIL>
      <ADDRESS1>1 The Street</ADDRESS1>
      <CITY>New York</CITY>
      <STATE>NY</STATE>
      <ZIP>12345</ZIP>
      <COUNTRY>US</COUNTRY>
      <PRIMARYPHONE>1234567890</PRIMARYPHONE>
      <WORKPHONE>1234567890</WORKPHONE>
  </CUSTOMER>
  <DIGITALWALLET>
      <DWTYPE>G</DWTYPE>
      <DWPAYLOAD>{YOUR_PAYLOAD_FROM_DIGITALWALLET_Base64Encoded}</DWPAYLOAD>
  </DIGITALWALLET>
  </PAYMENTREQUEST>
```

If your website or payment application is not integrated with the OSBP solution, Google Pay can also be integrated with our transaction API.

To implement the Google Pay with API, please send a request to [support@payscout.com](mailto:support@payscout.com) and our technical support will help with the integration.

Please also refer to Google's documentation: [https://developers.google.com/pay/api/web/overview](https://developers.google.com/pay/api/web/overview) for more integration details.

To request Google Pay token with Payscout, follow the instructions on [Step 2 Request a payment token for your payment provider](https://developers.google.com/pay/api/web/guides/tutorial#tokenization), please make sure to use `payscout` for the `gateway`field and your gateway `PWCLIENTID` for `gatewayMerchantId`, see examples in the code block on the right side (Google Pay Request).

The Google Pay integration must follow Google's brand guidelines as defined here: [https://developers.google.com/pay/api/web/guides/brand-guidelines](https://developers.google.com/pay/api/web/guides/brand-guidelines)

The supported authorization methods for Google Pay transactions will be the same as the [_API Sale Transactions_](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-one-time-sale).

To send the Google Pay payload for a transaction request, (see examples on the right side):

1.   Utilize the similar request message as the [_API Sale Transactions_](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-one-time-sale).
2.   Set `PWMEDIA` to `CC`.
3.   Remove the `CARDNUMBER`, `EXP_MM`, `EXP_YY`, `CVV2` fields and add the `DIGITALWALLET` group to the transaction request. For field reference: [_Digital Wallet Data Group_](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#api-digital-wallet-group)
4.   Set `DWTYPE` to `G`.
5.   Fill in the encrypted payload to the `DWPAYLOAD` field.

The Paywire transaction API will decrypt the payload and process the transaction. The card brand type, last four of the card number and other transaction details will be returned the same way as the regular transactions.

The billing address are not mandatory by default, however we could make certain fields mandatory based on your use case.

If you want to enable Google Pay on your Android application, please send a request to [support@payscout.com](mailto:support@payscout.com) and also refer to Google's documentation: [https://developers.google.com/pay/api/android/overview](https://developers.google.com/pay/api/android/overview)

If would like to enable 3DS for PAN_ONLY credentials returned via the Google Pay API for your merchant account, please send a request to [support@payscout.com](mailto:support@payscout.com) for availability and our support team will enable it for the merchant account.

Appendix
--------

AVS Codes
---------

The following are the possible values returned in the `AVSCODE` XML parameter:

| Value | Description |
| --- | --- |
| `S` | Service not supported. |
| `E` | Not a mail/phone order. |
| `R` | Issuer system unavailable. |
| `U` | Address unavailable. |
| `Z` | No address or zip match. |
| `W` | 5-digit zip match only. |
| `A` | 9-digit zip match only. |
| `Y` | Address match only. |
| `X` | Exact match, 5-digit zip. |

Zip Code Requirements
---------------------

We strongly suggest adding zip codes in transactions to reduce the decline rate as adding zip codes helps reduce fraud transactions. Card issuers have implemented very strict AVS verification, and issuers may decline key-in transactions that are missing or without proper zip codes.

CVV Codes
---------

The following are the possible values returned in the `CVV` XML parameter:

| Value | Description |
| --- | --- |
| `M` | Match. |
| `P` | Not Processed. |
| `G` | Verification unavailable due to international issuer non-participation (Visa only). |
| `N` | No match. |

US Decline Codes
----------------

The credit/debit card decline code from the US processors: [US Decline Codes](https://project.paywire.com/dbtranz/docs/Gateway/Payscout_Decline_Codes.pdf)

There are also some decline codes generated by the Paywire Gateway:

| Paywire Decline Code | Description |
| --- | --- |
| `SP1` | Stop Payment. The transaction was previously declined with a code that disallows future authentication attempts. |
| `SP2` | Suspend payment. The transaction was previously declined multiple times with specific decline codes within a certain time range. |
| `K01` | Merchant processing limit reached. |
| `K02` | Reserved. |
| `K03` | The transaction was below the minimum allowed amount. |
| `K04` | The transaction exceeded the maximum allowed limit. |
| `K05` | The card's BIN was blocked. |
| `K06` | The card's country was blocked. |

ACH Response Codes
------------------

The ACH response codes including NACHA return codes, NOC codes and the processor specific rejection codes: [ACH Response Codes](https://project.paywire.com/dbtranz/docs/Gateway/Paywire_ACH_Response_Codes.pdf)

UnionPay SMS Decline Codes
--------------------------

| SMS Decline Code | Decline Message |
| --- | --- |
| `S00` | Success. |
| `S01` | Transaction failed. For details please inquire overseas service hotline. |
| `S02` | System is not started or temporarily down, please try again later. |
| `S03` | Transaction communication time out, please initiate inquiry transaction. |
| `S05` | Transaction has been accepted, please inquire about transaction result shortly. |
| `S06` | System is busy, please retry it later. |
| `S10` | Message format error. |
| `S11` | Verify signature error. |
| `S12` | Repeat transaction. |
| `S13` | Message transaction key element missing. |
| `S30` | Transaction failed, please try using other UnionPay card for payment or contact overseas service hotline. |
| `S31` | Merchant state incorrect. The payment is not completed within the order timeout. |
| `S32` | No such transaction right. |
| `S33` | Transaction amount exceeds limit. |
| `S34` | Could not find this transaction. |
| `S35` | Original transaction does not exist or state is incorrect. |
| `S36` | Does not match original transaction information. |
| `S37` | Max number of inquiries exceeded or too frequent operations. |
| `S38` | UnionPay risk constraint. |
| `S39` | Transaction is not within the acceptance time range. |
| `S42` | Balance deduction successful but transaction exceeded payment time limit. |
| `S43` | Business not allowed, please contact overseas service hotline for help. |
| `S44` | Wrong number entered or business not opened, please contact overseas service hotline for help. |
| `S45` | The original transaction has been refunded or cancelled successfully. |
| `S60` | Transaction failure, for details, please inquire with your issuer. |
| `S61` | Card number entered is invalid, please double check and enter. |
| `S62` | Transaction failed, issuer does not support this merchant, please change to another bank card. |
| `S63` | Card state is incorrect. |
| `S64` | Card balance is insufficient. |
| `S65` | Error with PIN, expiration date, or CVN2 entered, transaction failure. |
| `S66` | Cardholder identity information or mobile number entered are incorrect, verification failure. |
| `S67` | Limit on number of PIN entry attempts exceeded. |
| `S68` | Your bank card currently does not support this business, please inquire with your bank or overseas service hotline for help. |
| `S69` | Time limit on entry exceeded, transaction failure. |
| `S70` | Transaction has been redirected, waiting for cardholder input. |
| `S71` | Dynamic password or SMS verification code validation failure. |
| `S72` | You have not signed up for UnionPay card-not-present payment service at the bank counter or on your personal online bank, please go to a bank counter or access your online banking to activate it or contact overseas service hotline for help. |
| `S73` | Payment card has exceeded expiration date. |
| `S76` | Requires encryption verification for activation. |
| `S77` | Bank card has not been activated for authenticated payment. |
| `S78` | Issuer transaction rights limited, for details please contact your issuer. |
| `S79` | The bank card is valid, but issuer does not support SMS verification. |
| `S80` | Transaction failed and the token has expired. |
| `S81` | Monthly accumulated transaction counter (amount) exceeded. |
| `S82` | PIN needs to be verified. |
| `S84` | PIN is required but not submitted. |
| `S85` | Transaction failed, the marketing rules are not met. |
| `S86` | QRC status error. |
| `S87` | Token has exceeded its maximum number of use. |
| `S88` | QRC not found. |
| `S89` | No Token found, invalid TR status or invalid Token status. |
| `S98` | File does not exist. |
| `S99` | General error. |
| `SA6` | Partial success. Successful transaction with defect. |

| Authorization Decline Code | Message |
| --- | --- |
| `A00` | Approve. Successful transaction. |
| `A01` | Decline. The cardholder should contact the Issuer. |
| `A03` | Decline. Invalid merchant. |
| `A04` | Pick-up. This card is picked up. |
| `A05` | Decline. The cardholder's certification fails. |
| `A10` | Partial amount approved. |
| `A11` | Approve. VIP. |
| `A12` | Decline. Invalid transaction. |
| `A13` | Decline. Invalid amount. |
| `A14` | Decline. Invalid card number. |
| `A15` | Decline. No Issuer matching this card. |
| `A16` | Approve. Update the third magnetic track. |
| `A1A` | Decline, The transaction need additional customer authentication. |
| `A20` | Decline. Update the QRC. |
| `A21` | Decline. This card has not been initialized or it is a dormant card. |
| `A22` | Decline. Suspected malfunction related transaction error. |
| `A25` | Decline. There is no original transaction and please contact the Issuer. |
| `A30` | Decline. Format error. |
| `A34` | Pick-up. Fraudulent card, pickup card. |
| `A38` | Decline. The number of PIN entry attempts is beyond the limit, and please contact the Issuer. |
| `A40` | Decline. Transaction that is not supported by the Issuer. |
| `A41` | Pick-up. The card reported for loss is captured now (ATM). Lost card, please pick up it (POS). |
| `A43` | Pick-up. This card is captured (ATM). Please contact the Issuer. Stolen card, please pick it up (POS). |
| `A45` | Decline. Please use IC card. |
| `A51` | Decline. Insufficient available balance. |
| `A54` | Decline. The card expires. |
| `A55` | Decline. Wrong PIN. |
| `A57` | Decline. This card is not allowed for the transaction. |
| `A58` | Decline. The Issuer does not allow this card to be used for this. |
| `A59` | Decline. Error in the card verification. |
| `A61` | Decline. The transaction amount exceeds the limit. |
| `A62` | Decline. Restricted card. |
| `A64` | Decline. The transaction amount does not match the original transaction amount. |
| `A65` | Decline. Exceed the limit for times of withdrawal. |
| `A68` | Decline. The transaction times out. Please retry it. |
| `A75` | Decline. The number of PIN tries exceeds the limit. |
| `A90` | Decline. The daily cutoff of the system is in progress. Please retry it later. |
| `A91` | Decline. The Issuer status is abnormal. Please retry it later. |
| `A92` | Decline. The connectivity of Issuer is abnormal. Please retry it later. |
| `A94` | Decline. Rejected for duplicated transaction. Please retry it later. |
| `A96` | Decline. Rejected for switching center malfunction. Please retry it later. |
| `A97` | Decline. The terminal number has not been registered. |
| `A98` | Decline. The Issuer is time out. |
| `A99` | Decline. Error in PIN format. Please re-sign in. |
| `AA0` | Decline. Error in MAC verification. Please resign in. |
| `AA1` | Reserved |
| `AA2` | Approve. The transaction is successful. Please confirm it with the fund transfer-in bank. |
| `AA3` | Decline. The recipient’s account number is incorrect for the fund transfer-in bank. |
| `AA4` | Approve. The transaction is successful. Please confirm it with the fund transfer-in bank. |
| `AA5` | Approve. The transaction is successful. Please confirm it with the fund transfer-in bank. |
| `AA6` | Approve. The transaction is successful. Please confirm it with the fund transfer-in bank. |
| `AA7` | Security processing failure. |
| `AB1` | Decline. No debt for this service. |
| `AC1` | Decline. The state of the Acquirer is illegal. |
| `AD1` | Incorrect IIN. |
| `AD2` | Date Error. |
| `AD3` | Invalid file type. |
| `AD4` | File processed. |
| `AD5` | No such file. |
| `AD6` | Not supported by Receiver. |
| `AD7` | File locked. |
| `AD8` | Unsuccessful. |
| `AD9` | Incorrect file length. |
| `ADA` | File decompression error. |
| `ADB` | File name error. |
| `ADC` | File cannot be received. |
| `AF1` | File record format error. |
| `AF2` | File record repeated. |
| `AF3` | File record not existing. |
| `AF4` | File record error. |
| `AN1` | Decline. The unregistered account exceeds the limit. |
| `AP1` | Decline. Contact number (e.g. mobile phone number) cannot be found. |
| `AY1` | Approve. |
| `AY3` | Approve. |
| `AZ1` | Decline. |
| `AZ3` | Decline. |

ACH Verify Decline Codes
------------------------

Paywire API Response for ACH verify decline.

The Routing/Account number combination to generate the errors is available at in the following table: [ACH Bank Details.](https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html#ACH-BankDetails)

> Sample Response:

```
<?xml version="1.0" encoding="UTF-8"?>
<PAYMENTRESPONSE>
  <RESULT>ERROR</RESULT>
  <RESTEXT>ACHVERIFY FAILED - AVC1 Invalid Routing Number</RESTEXT>
</PAYMENTRESPONSE>
```

| ResultCode | Result | Message |
| --- | --- | --- |
| `AVC1` | Invalid Routing Number | The Routing Number structure does not conform to the ABA standard. |
| `AVC2` | Suspected Bad Routing Number | The Routing Number structure conforms to the ABA standard, but has a history of returns for an invalid routing number. |
| `AVC3` | Routing Number Not Found | The Routing Number is not found within the ABA list of Routing Numbers. |
| `AVC4` | Routing Number Not ACH Capable | The Routing Number is not Active, not ACH capable, or is of the wrong type according to the ABA list of Routing Numbers. |
| `AVC5` | Suspected Bad Account Pattern | The Routing Number is valid, active, and is ACH capable. The Bank Account Number is suspected to be invalid or has a length or pattern with a history of returns for invalid account. |
| `AVC6` | Valid Account with History of Recent Returns, Unpaid, or Stop Payments | The Routing Number is valid, active, and is ACH capable. The Bank Account Number is valid, but there is recent history of returns/unpaid or stop payments seen in database. |
| `AVC7` | Valid Routing Number with Limited Account Pattern and No History of Recent Transactions | The Routing Number is valid, active, and is ACH capable. There is limited history of the Bank Account pattern and no history of recent transaction seen in database for the provided Bank Account Number. |
| `AVC8` | Valid Routing Number with No History of Account Pattern | The Routing Number is valid, active, and is ACH capable. There is no history of the Bank Account pattern seen in database for the provided Bank Account Number. |
| `AVC9` | Valid Routing Number and Account Pattern | The Routing Number is valid, active, and is ACH capable. The Bank Account pattern is valid. |
| `AVC0` | Unexpected Service Disruption | An unexpected service disruption with one or more data sources occurred. |
| `NV` | Not Validated | Not Validated - Returned when an Internal System Error occurs. |
