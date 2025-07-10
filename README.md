# Paywire.NET
A .NET 8 wrapper using RestSharp for the Paywire.com API

[![Nuget](https://img.shields.io/nuget/v/Paywire.NET)](https://www.nuget.org/packages/Paywire.NET)

## Performance Optimization

### XML Serialization
This library uses `XmlSerializer` for XML serialization/deserialization. By default, serialization assemblies are generated at runtime on first use, which causes a minor performance penalty (<100ms) during the first API call for each request/response type.

### Pre-generating Serializers (Optional)
For better startup performance, you can pre-generate the XML serializers using the included script:

```bash
# After building your project
./generate-serializers.sh
```

This script works around a [known issue](https://github.com/dotnet/runtime/issues/90913) with `Microsoft.XmlSerializer.Generator` that occurs when multiple .NET runtime versions are installed.

### Alternative: Runtime Warm-up
If you prefer not to use the script, you can warm up the serializers during application initialization:

```csharp
// Warm up common serializers
_ = new XmlSerializer(typeof(SaleRequest));
_ = new XmlSerializer(typeof(SaleResponse));
// Add other types you frequently use
```
