using System.Diagnostics.CodeAnalysis;

namespace Paywire.NET;

[SuppressMessage("ReSharper", "StringLiteralTypo")]
public static class PaywireTransactionType
{
    /// <summary>
    /// Charge a card or bank account (if applicable).
    /// </summary>
    public static readonly string Sale = "SALE";
    /// <summary>
    /// Void a transaction. The transaction amount must match the amount of the original transaction, and the PWUNIQUEID must match the unique identifier associated with the transaction to void. The transaction must be in the current open batch to void it.
    /// </summary>
    public static readonly string Void = "VOID";
    /// <summary>
    /// Credit a transaction. The transaction amount must be equal to or less than the amount to credit, and the PWUNIQUEID must match the unique identifier associated with the transaction to credit. Only transactions in a closed batch with a status of SETTLED can be credited.
    /// </summary>
    public static readonly string Credit = "CREDIT";
    /// <summary>
    /// Pre-authorize a card.
    /// </summary>
    public static readonly string PreAuth = "PREAUTH";
    /// <summary>
    /// Exchange your credentials for an AUTHTOKEN to use when calling the OSBP.
    /// </summary>
    public static readonly string GetAuthToken = "GETAUTHTOKEN";
    /// <summary>
    /// Input the sale amount to get adjustment, tax, and total transaction amounts. Relevant for merchants configured with Cash Discount or Convenience Fees.
    /// </summary>
    public static readonly string GetConsumerFee = "GETCONSUMERFEE";
    /// <summary>
    /// Creates a Customer in the Paywire Vault.
    /// </summary>
    public static readonly string CreateCustomer = "CREATECUSTOMER";
    /// <summary>
    /// Lists tokens stored against a given customer.
    /// </summary>
    public static readonly string GetCustomerTokens = "GETCUSTOKENS";
    /// <summary>
    /// Validate a card and return a token.
    /// </summary>
    public static readonly string StoreToken = "STORETOKEN";
    /// <summary>
    /// Delete an existing token from Paywire.
    /// </summary>
    public static readonly string RemoveToken = "REMOVETOKEN";
    /// <summary>
    /// Verification transaction will verify the customer's card or bank account before submitting the payment.
    /// </summary>
    public static readonly string Verification = "VERIFICATION";
    /// <summary>
    /// Get the current open batch summary.
    /// </summary>
    public static readonly string BatchInquiry = "BATCHINQUIRY";
    /// <summary>
    /// Close the current open batch.
    /// </summary>
    public static readonly string Close = "CLOSE";
    /// <summary>
    /// Query the database for transaction results.
    /// </summary>
    public static readonly string SearchTransactions = "SEARCHTRANS";
    /// <summary>
    /// Query the database for periodic plan details using RECURRINGID, PWTOKEN or PWCID.
    /// </summary>
    public static readonly string GetPeriodicPlan = "GETPERIODICPLAN";
    /// <summary>
    /// Delete a periodic plan.
    /// </summary>
    public static readonly string DeleteRecurring = "DELETERECURRING";
    /// <summary>
    /// Sends a receipt for a given transaction.
    /// </summary>
    public static readonly string SendReceipt = "SENDRECEIPT";
}