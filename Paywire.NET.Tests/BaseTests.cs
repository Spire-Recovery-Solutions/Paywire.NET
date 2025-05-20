// BaseTests.cs

using System;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Paywire.NET.Factories;
using Paywire.NET.Models.Base;

namespace Paywire.NET.Tests;

/// <summary>
/// Base class for Paywire API integration tests.
/// Contains shared state and configuration setup.
/// </summary>
public abstract class BaseTests
{
    protected static PaywireClient CLIENT { get; private set; } = null!;
    protected static string TOKEN { get; set; } = string.Empty;
    protected static string UNIQUE_ID { get; set; } = string.Empty;
    protected static string INVOICE_NUMBER { get; set; } = string.Empty;
    protected static string BATCH_ID { get; set; } = string.Empty;
    protected static string SALE_AMOUNT { get; set; } = string.Empty;
    protected static string CREDIT_UNIQUE_ID { get; set; } = string.Empty;
    protected static string CREDIT_INVOICE_NUMBER { get; set; } = string.Empty;
    protected static string PRE_AUTH_UNIQUE_ID { get; set; } = string.Empty;
    protected static string PRE_AUTH_INVOICE_NUMBER { get; set; } = string.Empty;

    [OneTimeSetUp]
    public virtual void OneTimeSetUp()
    {
        // Load configuration using the latest .NET configuration pattern
        var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddUserSecrets(typeof(BaseTests).Assembly)
            .Build();

        // Initialize client with proper error handling
        CLIENT = new PaywireClient(new PaywireClientOptions
        {
            AUTHENTICATION_CLIENT_ID = config["PWCLIENTID"] ?? throw new InvalidOperationException("PWCLIENTID not found in configuration"),
            AUTHENTICATION_USERNAME = config["PWUSER"] ?? throw new InvalidOperationException("PWUSER not found in configuration"),
            AUTHENTICATION_KEY = config["PWKEY"] ?? throw new InvalidOperationException("PWKEY not found in configuration"),
            AUTHENTICATION_PASSWORD = config["PWPASS"] ?? throw new InvalidOperationException("PWPASS not found in configuration"),
            
            ENDPOINT = PaywireEndpoint.Staging
        });

        // Optional additional configuration example:
        // Client = new PaywireClient(
        //     new PaywireClientOptions
        //     {
        //         AuthenticationClientId = config["PWCLIENTID"],
        //         AuthenticationUsername = config["PWUSER"],
        //         AuthenticationKey = config["PWKEY"],
        //         AuthenticationPassword = config["PWPASS"],
        //         Endpoint = PaywireEndpoint.Staging
        //     },
        //     enableLogging: true,
        //     useHttpClientFactory: true,
        //     timeoutMilliseconds: 30000
        // );
    }
}