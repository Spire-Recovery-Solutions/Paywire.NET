# Paywire.NET
A .NET 10 SDK using RestSharp for the Paywire.com API

[![Nuget](https://img.shields.io/nuget/v/Paywire.NET)](https://www.nuget.org/packages/Paywire.NET)

## Performance Optimization

### XML Serialization
This library uses `XmlSerializer` for XML serialization/deserialization. `PaywireClient` maintains a static `ConcurrentDictionary<Type, XmlSerializer>` cache, so serializers are only created once per type and reused across all subsequent calls. No manual warm-up is needed.

### Security
XML deserialization uses a hardened `XmlReader` with `DtdProcessing.Prohibit` and `XmlResolver = null` to prevent XXE (XML External Entity) attacks.
