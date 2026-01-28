# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Paywire.NET is a .NET 8.0 SDK for integrating with the Paywire payment processing API. It provides a strongly-typed, async-first client library with comprehensive support for all Paywire operations.

Paywire is Payscout's proprietary payment platform that handles simple and complex payment needs. The API accepts XML requests via HTTP POST and supports various transaction types including sales, voids, credits, pre-authorizations, and tokenization.

**License**: GPL-3.0
**NuGet**: [![Nuget](https://img.shields.io/nuget/v/Paywire.NET)](https://www.nuget.org/packages/Paywire.NET)

## Environment Setup

### .NET SDK Location
The .NET SDK is located at `~/.dotnet`. If needed, add to PATH:
```bash
export PATH="$HOME/.dotnet:$PATH"
```

### SDK Version
The project uses .NET 8.0.400 (pinned in `global.json` with `latestPatch` rollForward policy).

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
│   │   └── publish.yml              # CI/CD: Build, test, NuGet publish
│   └── dependabot.yml               # Automated dependency updates
├── .gitignore
├── CLAUDE.md                        # This file
├── README.md                        # Usage documentation
├── LICENSE                          # GPL-3.0 License
├── global.json                      # .NET SDK version pinning (8.0.400)
├── generate-serializers.sh          # Optional XML serializer generation script
├── Paywire.NET/
│   ├── Paywire.NET.sln              # Solution file
│   ├── Paywire.NET.csproj           # Main project file
│   ├── PaywireClient.cs             # Core API client (202 lines)
│   ├── PaywireClientOptions.cs      # Configuration + PaywireEndpoint enum
│   ├── PaywireTransactionType.cs    # Transaction type constants (19 types)
│   ├── Factories/
│   │   └── PaywireRequestFactory.cs # Factory methods (592 lines, 25+ methods)
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
    └── Batch.cs                     # Batch operations (Order 4)
```

## Architecture and Code Organization

### Core Components

1. **PaywireClient** (`Paywire.NET/PaywireClient.cs`): Main client class that handles all API communication. Uses RestSharp for HTTP operations with generic `SendRequest<T>()` method for type-safe requests. Includes automatic transaction type inference based on request class.

2. **PaywireRequestFactory** (`Paywire.NET/Factories/PaywireRequestFactory.cs`): Static factory class with 25+ methods providing fluent interfaces to create all request types.

3. **Models** (`Paywire.NET/Models/`): Organized by operation type, each folder containing:
   - Request model (e.g., `SaleRequest`)
   - Response model (e.g., `SaleResponse`)
   - All models use XML attributes for serialization

4. **PaywireClientOptions** (`Paywire.NET/PaywireClientOptions.cs`): Configuration class with authentication credentials and endpoint selection.

### Key Patterns

- **Generic Request Pattern**: `PaywireClient.SendRequest<T>()` handles all API calls generically
- **Automatic Transaction Type Inference**: Client sets `PWTRANSACTIONTYPE` based on request class type (switch expression in `PaywireClient.cs:84-104`)
- **XML Serialization**: All models serialize to/from XML using `System.Xml.Serialization` attributes
- **Factory Pattern**: All requests created through `PaywireRequestFactory` static methods
- **Base Class Hierarchy**: `BasePaywireRequest` / `BasePaywireResponse` for common functionality
- **Result Parsing**: Smart enum parsing with fallback (handles "APPROVED" vs "APPROVAL")
- **Configuration**: Uses `PaywireClientOptions` with support for DI and configuration binding

### Dependencies

**Main Project (Paywire.NET)**:
- RestSharp 112.1.0 - HTTP client library
- Target Framework: .NET 8.0
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

Tests are **integration tests** that hit the actual Paywire staging API. They use **ordered execution** to maintain state across tests.

| Test File | Order | Tests | Purpose |
|-----------|-------|-------|---------|
| `TokenTests.cs` | 1 | 5 | GetAuthToken, StoreToken, TokenSale, TokenCredit, RemoveToken |
| `CreditCard.cs` | 2 | 11 | Verification, ConsumerFee, Sale, Credit, Void, PreAuth, Capture |
| `Transaction.cs` | 3 | 4 | SearchTransactions, Credit, SearchChargeback, SendReceipt |
| `Batch.cs` | 4 | 2 | BatchInquiry, CloseBatch |

**Shared State** (in `BaseTests.cs`):
- `CLIENT`, `TOKEN`, `UNIQUE_ID`, `INVOICE_NUMBER`, `BATCH_ID`, `SALE_AMOUNT`
- `CREDIT_UNIQUE_ID`, `CREDIT_INVOICE_NUMBER`, `PRE_AUTH_UNIQUE_ID`, `PRE_AUTH_INVOICE_NUMBER`

## CI/CD Pipeline

### GitHub Actions Workflow (`publish.yml`)
- **Trigger**: Push to `main` or `alpha` branches
- **Runner**: `ubuntu-latest`
- **Steps**:
  1. Checkout code
  2. Setup .NET 8.0.x
  3. Set version postfix (`-alpha` for alpha branch)
  4. Build with Release configuration
  5. Run tests (with Paywire credentials from secrets)
  6. Pack NuGet with version `1.0.0.<run_number>` or `1.0.0.<run_number>-alpha`
  7. Push to NuGet.org

**Required Secrets**:
- `PWCLIENTID`, `PWUSER`, `PWKEY`, `PWPASS` - Test credentials
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

When updating RestSharp or other dependencies, ensure compatibility with .NET 8.0 target framework. The project uses RestSharp 112.x which has different API than earlier versions.

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

## Microsoft.XmlSerializer.Generator Configuration

**Status**: The `Microsoft.XmlSerializer.Generator` package has been completely removed from the project due to persistent issues.

1. **Issue Background**: The package had a [known issue](https://github.com/dotnet/runtime/issues/90913) where it fails with "Version for package could not be resolved" when multiple .NET runtime versions are installed. This caused GitHub Actions builds to fail with error NU5026.

2. **Solution Implemented**:
   - Removed `Microsoft.XmlSerializer.Generator` package reference entirely
   - Added `<GenerateSerializationAssemblies>Off</GenerateSerializationAssemblies>` to prevent any automatic generation attempts
   - The project now uses runtime serialization generation exclusively

3. **Performance Impact**:
   - Without pre-generated serializers, the first XML serialization of each type is slightly slower (~100ms)
   - Subsequent serializations are unaffected (assemblies are cached in memory)
   - Overall application performance impact is negligible

4. **Optional Warm-up**: For production applications, consider warming up serializers during startup:
   ```csharp
   _ = new XmlSerializer(typeof(SaleRequest));
   _ = new XmlSerializer(typeof(SaleResponse));
   // Add other types you frequently use
   ```

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
