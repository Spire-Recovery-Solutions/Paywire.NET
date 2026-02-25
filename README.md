# Paywire.NET
A .NET 10 SDK using RestSharp for the Paywire.com API

[![Nuget](https://img.shields.io/nuget/v/Paywire.NET)](https://www.nuget.org/packages/Paywire.NET)

## Performance Optimization

### XML Serialization
This library uses `XmlSerializer` for XML serialization/deserialization. `PaywireClient` maintains a static `ConcurrentDictionary<Type, XmlSerializer>` cache, so serializers are only created once per type and reused across all subsequent calls. No manual warm-up is needed.

### Security
XML deserialization uses a hardened `XmlReader` with `DtdProcessing.Prohibit` and `XmlResolver = null` to prevent XXE (XML External Entity) attacks.

## Digital Wallet Support (Apple Pay / Google Pay)

The SDK supports Apple Pay and Google Pay transactions via the `DIGITALWALLET` XML group. Digital wallet transactions reuse existing `SALE` and `PREAUTH` transaction types.

### Quick Start (Encrypted Payload)

```csharp
// Apple Pay sale
var applePaySale = PaywireRequestFactory.DigitalWalletSale(
    saleAmount: 25.00,
    walletType: "A",           // "A" = Apple Pay
    walletPayload: "BASE64_ENCRYPTED_PAYLOAD_FROM_APPLE",
    firstName: "John",
    lastName: "Doe",
    state: "TX",
    email: "john@example.com"
);
var response = await client.SendRequest<SaleResponse>(applePaySale);

// Google Pay pre-authorization
var googlePayPreAuth = PaywireRequestFactory.DigitalWalletPreAuth(
    saleAmount: 100.00,
    walletType: "G",           // "G" = Google Pay
    walletPayload: "BASE64_ENCRYPTED_PAYLOAD_FROM_GOOGLE",
    firstName: "Jane",
    lastName: "Smith",
    state: "CA"
);
var preAuthResponse = await client.SendRequest<PreAuthResponse>(googlePayPreAuth);
```

### Full-Control Overload

```csharp
var request = PaywireRequestFactory.Sale(
    header: new TransactionHeader { PWSALEAMOUNT = 50.00 },
    customer: new Customer { PWMEDIA = "CC", STATE = "NY" },
    digitalWallet: new DigitalWallet
    {
        DWTYPE = "A",
        DWPAYLOAD = "BASE64_ENCRYPTED_PAYLOAD"
    }
);
```

### Decrypted Path

If you decrypt the wallet payload yourself, use the decrypted fields instead:

```csharp
var wallet = new DigitalWallet
{
    DWTYPE = "G",
    ISTDES = "TRUE",
    CAVV = "CAVV_DATA_HERE",
    ECI = "05",
    UCAF = "UCAF_INDICATOR"   // MasterCard only
};
```
