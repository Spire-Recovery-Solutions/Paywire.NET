# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Paywire.NET is a .NET 10 SDK for integrating with the Paywire payment processing API. It provides a strongly-typed, async-first client library with comprehensive support for all Paywire operations.

Paywire is Payscout's proprietary payment platform that handles simple and complex payment needs. The API accepts XML requests via HTTP POST and supports various transaction types including sales, voids, credits, pre-authorizations, and tokenization.

**License**: GPL-3.0
**NuGet**: [![Nuget](https://img.shields.io/nuget/v/Paywire.NET)](https://www.nuget.org/packages/Paywire.NET)

## Environment Setup

### SDK Version
The project uses .NET 10.0.100 (pinned in `global.json` with `latestPatch` rollForward policy).

## Build and Test Commands

### Building the Project
```bash
# From root directory
cd Paywire.NET
dotnet restore
dotnet build --configuration Release --no-restore
```

### Running Tests
```bash
cd Paywire.NET.Tests
dotnet test
```

Tests require Paywire credentials set as environment variables or user secrets:
- `PWCLIENTID` - Paywire Client ID
- `PWUSER` - Paywire Username
- `PWKEY` - Paywire API Key
- `PWPASS` - Paywire Password

### Creating NuGet Package
```bash
cd Paywire.NET
dotnet pack --configuration Release --no-restore --output nupkg /p:Version=1.0.0.XXX
```

## Project Structure

```
Paywire.NET/
├── .github/
│   ├── workflows/
│   │   ├── publish.yml              # CI/CD: Build, test, NuGet publish
│   │   └── pr-check.yml            # PR checks: unit tests + coverage
│   └── dependabot.yml               # Automated dependency updates
├── .gitignore
├── CLAUDE.md                        # This file
├── README.md                        # Usage documentation
├── LICENSE                          # GPL-3.0 License
├── global.json                      # .NET SDK version pinning (10.0.100)
├── Paywire.NET/
│   ├── Paywire.NET.sln              # Solution file
│   ├── Paywire.NET.csproj           # Main project file
│   ├── PaywireClient.cs             # Core API client (189 lines)
│   ├── IPaywireClient.cs            # Client interface for DI/testing
│   ├── PaywireClientOptions.cs      # Configuration + PaywireEndpoint enum
│   ├── PaywireTransactionType.cs    # Transaction type constants (19 types)
│   ├── Factories/
│   │   └── PaywireRequestFactory.cs # Factory methods (593 lines, 25+ methods)
│   └── Models/
│       ├── Base/                    # Base classes and shared models
│       │   ├── BasePaywireRequest.cs
│       │   ├── BasePaywireResponse.cs
│       │   ├── Customer.cs          # Customer/payment data model
│       │   ├── PaywireResult.cs     # Result enum
│       │   └── TransactionHeader.cs # Auth and transaction metadata
│       ├── BatchInquiry/
│       ├── BinValidation/
│       ├── Capture/
│       ├── CloseBatch/
│       ├── Credit/
│       ├── GetAuthToken/
│       ├── GetConsumerFee/
│       ├── PreAuth/
│       ├── Receipt/
│       ├── RemoveToken/
│       ├── Sale/
│       ├── SearchChargebacks/
│       ├── SearchTransactions/
│       ├── StoreToken/
│       ├── TokenSale/
│       ├── Verification/
│       └── Void/
└── Paywire.NET.Tests/
    ├── Paywire.NET.Tests.csproj
    ├── BaseTests.cs                 # Shared state and configuration
    ├── TokenTests.cs                # Token operations (Order 1)
    ├── CreditCard.cs                # Card transactions (Order 2)
    ├── Transaction.cs               # Transaction operations (Order 3)
    ├── Batch.cs                     # Batch operations (Order 4)
    ├── ErrorPathTests.cs            # Negative/error path tests (Order 0)
    ├── ExpandedIntegrationTests.cs  # ECHECK, search, token overloads (Order 5)
    └── UnitTests/                   # Offline unit tests (no API needed)
        ├── PaywireResultParsingTests.cs
        ├── XmlSerializationTests.cs
        ├── PaywireRequestFactoryTests.cs
        └── TransactionTypeInferenceTests.cs
```

## Architecture and Code Organization

### Core Components

1. **PaywireClient** (`Paywire.NET/PaywireClient.cs`): Main client class implementing `IPaywireClient`. Uses RestSharp for HTTP operations with generic `SendRequest<T>(request, CancellationToken)` method for type-safe requests. Includes automatic transaction type inference, XXE-safe XML deserialization (`DtdProcessing.Prohibit`, `XmlResolver = null`), a static `ConcurrentDictionary<Type, XmlSerializer>` cache to prevent memory leaks, and implements `IDisposable` to dispose the `RestClient`.

2. **PaywireRequestFactory** (`Paywire.NET/Factories/PaywireRequestFactory.cs`): Static factory class with 25+ methods providing fluent interfaces to create all request types.

3. **Models** (`Paywire.NET/Models/`): Organized by operation type, each folder containing:
   - Request model (e.g., `SaleRequest`)
   - Response model (e.g., `SaleResponse`)
   - All models use XML attributes for serialization

4. **PaywireClientOptions** (`Paywire.NET/PaywireClientOptions.cs`): Configuration class with authentication credentials and endpoint selection.

### Key Patterns

- **Interface**: `IPaywireClient` for DI registration and testing
- **Generic Request Pattern**: `PaywireClient.SendRequest<T>(request, ct)` handles all API calls generically with CancellationToken support
- **Automatic Transaction Type Inference**: Client sets `PWTRANSACTIONTYPE` based on request class type (switch expression in `PaywireClient.cs`)
- **XML Serialization**: All models serialize to/from XML using `System.Xml.Serialization` attributes
- **Security**: XXE-safe deserialization with `DtdProcessing.Prohibit` and `XmlResolver = null`
- **Memory Safety**: Static `ConcurrentDictionary<Type, XmlSerializer>` cache prevents OOM from dynamic assembly generation
- **Factory Pattern**: All requests created through `PaywireRequestFactory` static methods
- **Base Class Hierarchy**: `BasePaywireRequest` / `BasePaywireResponse` for common functionality
- **Result Parsing**: Smart enum parsing with fallback (handles "APPROVED" vs "APPROVAL", "COMPLETED")
- **Configuration**: Uses `PaywireClientOptions` with support for DI and configuration binding
- **Disposable**: `PaywireClient` implements `IDisposable` to properly dispose `RestClient`

### Dependencies

**Main Project (Paywire.NET)**:
- RestSharp 112.1.0 - HTTP client library
- Target Framework: .NET 10.0
- Nullable: Enabled
- ImplicitUsings: Enabled

**Test Project (Paywire.NET.Tests)**:
- Microsoft.Extensions.Configuration 9.0.5
- Microsoft.Extensions.Configuration.EnvironmentVariables 9.0.5
- Microsoft.Extensions.Configuration.UserSecrets 9.0.5
- Microsoft.NET.Test.Sdk 17.14.0
- NUnit 4.3.2
- NUnit3TestAdapter 5.0.0
- coverlet.collector 6.0.4

### API Integration Details

- **Base URLs**:
  - Staging: `https://dbstage1.paywire.com`
  - Production: `https://dbtranz.paywire.com`
- **API Endpoint**: `/API/pwapi`
- **HTTP Method**: POST with XML body via RestSharp's `AddXmlBody()`
- **Authentication**: Credentials set in TransactionHeader (PWCLIENTID, PWKEY, PWUSER, PWPASS)
- **API Version**: Version 3 (set via `PWVERSION` in request)
- **Default Timeout**: 30 seconds (configurable)
- **Request Format**: XML with `PAYMENTREQUEST` root element
- **Response Format**: XML with `PAYMENTRESPONSE` root element
- **Documentation**:
  - Local: `docs/paywire-api-reference.md` (LLM-friendly markdown)
  - Online: https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html

### Testing Approach

The project has **57 unit tests** (no API needed) and **41 integration tests** (hit the Paywire staging API). Total: **98 tests**.

**Unit Tests** (`UnitTests/` directory — run without credentials):

| Test File | Tests | Purpose |
|-----------|-------|---------|
| `PaywireResultParsingTests.cs` | 13 | Enum parsing, fallbacks, unknown values |
| `XmlSerializationTests.cs` | 9 | XML roundtrip, nullable handling, root attributes |
| `PaywireRequestFactoryTests.cs` | 32 | Every factory overload's output fields |
| `TransactionTypeInferenceTests.cs` | 3 | Request type → transaction type mapping |

**Integration Tests** (require `PWCLIENTID`, `PWUSER`, `PWKEY`, `PWPASS`):

| Test File | Order | Tests | Purpose |
|-----------|-------|-------|---------|
| `ErrorPathTests.cs` | 0 | 5 | Invalid credentials, expired card, timeout |
| `TokenTests.cs` | 1 | 5 | GetAuthToken, StoreToken, TokenSale, TokenCredit, RemoveToken |
| `CreditCard.cs` | 2 | 12 | Verification, ConsumerFee, Sale, Credit, Void, PreAuth, Capture |
| `Transaction.cs` | 3 | 6 | SearchTransactions, Credit, SearchChargeback, SendReceipt |
| `Batch.cs` | 4 | 2 | BatchInquiry, CloseBatch |
| `ExpandedIntegrationTests.cs` | 5 | 8 | ECHECK, search filters, token/preauth overloads |

**Shared State** (in `BaseTests.cs`):
- `CLIENT`, `TOKEN`, `UNIQUE_ID`, `INVOICE_NUMBER`, `BATCH_ID`, `SALE_AMOUNT`
- `CREDIT_UNIQUE_ID`, `CREDIT_INVOICE_NUMBER`, `PRE_AUTH_UNIQUE_ID`, `PRE_AUTH_INVOICE_NUMBER`

**Running unit tests only** (no credentials needed):
```bash
dotnet test --filter "FullyQualifiedName~UnitTests"
```

## CI/CD Pipeline

### GitHub Actions Workflows

**`publish.yml`** — Build, test, and publish to NuGet:
- **Trigger**: Push to `main` or `alpha` branches
- **Runner**: `ubuntu-latest` with `timeout-minutes: 30`
- **Concurrency**: `publish-${{ github.ref }}`, cancel-in-progress: false
- **Steps**: Checkout (fetch-depth: 0) → Setup .NET 10.0.x → NuGet cache → Build → Test → Pack → Push to NuGet.org
- **Version**: `1.0.0.<run_number>` (alpha branch appends `-alpha`)

**`pr-check.yml`** — Unit tests on pull requests:
- **Trigger**: `pull_request`
- **Runs only unit tests**: `--filter "FullyQualifiedName~UnitTests"` (no API credentials needed)
- **Artifacts**: Coverage report uploaded via `actions/upload-artifact`
- **Note**: Only GitHub-owned and verified marketplace actions are allowed by repo policy

**Required Secrets**:
- `PWCLIENTID`, `PWUSER`, `PWKEY`, `PWPASS` - Test credentials (publish workflow only)
- `NUGET_API_KEY` - NuGet publish key

### Dependabot Configuration
- Daily updates for GitHub Actions and NuGet packages
- Auto-groups all updates into single PRs
- Reviewers: `@Spire-Recovery-Solutions/devs`

## Common Development Tasks

### Adding a New Transaction Type

1. Create request/response models in `Paywire.NET/Models/{OperationType}/`
2. Add XML serialization attributes matching Paywire's API schema
3. Add factory method in `PaywireRequestFactory`
4. Add case to transaction type switch in `PaywireClient.SendRequest<T>()` (line 84-104)
5. Add integration test in appropriate test file
6. Update `PaywireTransactionType` constants if needed

### Debugging API Issues

1. Check XML serialization by manually serializing request objects
2. Verify credentials are correctly set
3. Use Paywire's staging environment for testing
4. Common response codes:
   - `APPROVAL`: Transaction approved
   - `DECLINED`: Transaction declined
   - Check AVS and CVV response codes for additional validation details
5. Check `RESTEXT` field in response for error messages

### Updating Dependencies

When updating RestSharp or other dependencies, ensure compatibility with the .NET 10.0 target framework. The project uses RestSharp 112.x which has different API than earlier versions.

## Known Issues and TODOs

### TODO Items
- `PaywireRequestFactory.cs:439`: `// TODO: Check what field addCustomer goes into`

### Missing Transaction Type Models
Some transaction types defined in `PaywireTransactionType.cs` lack corresponding models:
- `CreateCustomer` - No model implemented
- `GetCustomerTokens` - No model implemented
- `GetPeriodicPlan` - No model implemented
- `DeleteRecurring` - No model implemented

### Obsolete Properties
In `ConsumerFeeSummary.cs`, `CDDESCRIPTIONVPOS` and `CDDESCRIPTIONOSBP` are marked obsolete due to XML parsing issues with embedded line breaks.

## XML Serialization

- `Microsoft.XmlSerializer.Generator` was removed due to a [known issue](https://github.com/dotnet/runtime/issues/90913) with multi-runtime environments
- `<GenerateSerializationAssemblies>Off</GenerateSerializationAssemblies>` is set in the csproj
- `PaywireClient` maintains a static `ConcurrentDictionary<Type, XmlSerializer>` cache — serializers are created once per type and reused, so no manual warm-up is needed
- XML deserialization uses a hardened `XmlReader` with `DtdProcessing.Prohibit` and `XmlResolver = null` (XXE protection)

## Paywire Transaction Types

The SDK supports all major Paywire transaction types:

| Type | Description | Model Status |
|------|-------------|--------------|
| **SALE** | One-time payment transaction | ✅ Implemented |
| **PREAUTH** | Pre-authorization (capture later) | ✅ Implemented |
| **CAPTURE** | Capture a pre-authorized transaction | ✅ Implemented |
| **VOID** | Void transaction in open batch | ✅ Implemented |
| **CREDIT** | Refund/credit for settled transactions | ✅ Implemented |
| **STORETOKEN** | Store payment method for future use | ✅ Implemented |
| **TOKENSALE** | Sale using stored token | ✅ Implemented |
| **REMOVETOKEN** | Remove stored payment token | ✅ Implemented |
| **VERIFICATION** | Card verification (0.00 auth) | ✅ Implemented |
| **GETAUTHTOKEN** | Get authentication token | ✅ Implemented |
| **GETCONSUMERFEE** | Calculate consumer fees | ✅ Implemented |
| **SEARCHTRANSACTIONS** | Search transaction history | ✅ Implemented |
| **SEARCHCHARGEBACKS** | Search chargeback data | ✅ Implemented |
| **BATCHINQUIRY** | Get batch information | ✅ Implemented |
| **CLOSEBATCH** | Close current batch | ✅ Implemented |
| **BINVALIDATION** | Validate BIN information | ✅ Implemented |
| **RECEIPT** | Get transaction receipt | ✅ Implemented |
| **CREATECUSTOMER** | Create customer profile | ❌ Not implemented |
| **GETCUSTOMERTOKENS** | Get customer's stored tokens | ❌ Not implemented |
| **GETPERIODICPLAN** | Get recurring plan details | ❌ Not implemented |
| **DELETERECURRING** | Delete recurring subscription | ❌ Not implemented |

Each transaction type requires specific mandatory fields as defined in the corresponding request models.
