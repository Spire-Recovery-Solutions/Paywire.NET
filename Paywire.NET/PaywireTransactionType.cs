using System.Diagnostics.CodeAnalysis;

namespace Paywire.NET;

[SuppressMessage("ReSharper", "StringLiteralTypo")]
public static class PaywireTransactionType
{
    /// <summary>
    /// Charge a card or bank account (if applicable).
    /// </summary>
    public const string Sale = "SALE";
    /// <summary>
    /// Void a transaction. The transaction amount must match the amount of the original transaction, and the PWUNIQUEID must match the unique identifier associated with the transaction to void. The transaction must be in the current open batch to void it.
    /// </summary>
    public const string Void = "VOID";
    /// <summary>
    /// Credit a transaction. The transaction amount must be equal to or less than the amount to credit, and the PWUNIQUEID must match the unique identifier associated with the transaction to credit. Only transactions in a closed batch with a status of SETTLED can be credited.
    /// </summary>
    public const string Credit = "CREDIT";
    /// <summary>
    /// Pre-authorize a card.
    /// </summary>
    public const string PreAuth = "PREAUTH";
    /// <summary>
    /// Exchange your credentials for an AUTHTOKEN to use when calling the OSBP.
    /// </summary>
    public const string GetAuthToken = "GETAUTHTOKEN";
    /// <summary>
    /// Input the sale amount to get adjustment, tax, and total transaction amounts. Relevant for merchants configured with Cash Discount or Convenience Fees.
    /// </summary>
    public const string GetConsumerFee = "GETCONSUMERFEE";
    /// <summary>
    /// Creates a Customer in the Paywire Vault.
    /// </summary>
    public const string CreateCustomer = "CREATECUSTOMER";
    /// <summary>
    /// Lists tokens stored against a given customer.
    /// </summary>
    public const string GetCustomerTokens = "GETCUSTOKENS";
    /// <summary>
    /// Validate a card and return a token.
    /// </summary>
    public const string StoreToken = "STORETOKEN";
    /// <summary>
    /// Delete an existing token from Paywire.
    /// </summary>
    public const string RemoveToken = "REMOVETOKEN";
    /// <summary>
    /// Verification transaction will verify the customer's card or bank account before submitting the payment.
    /// </summary>
    public const string Verification = "VERIFICATION";
    /// <summary>
    /// Get the current open batch summary.
    /// </summary>
    public const string BatchInquiry = "BATCHINQUIRY";
    /// <summary>
    /// Close the current open batch.
    /// </summary>
    public const string CloseBatch = "CLOSE";
    /// <summary>
    /// Query the database for transaction results.
    /// </summary>
    public const string SearchTransactions = "SEARCHTRANS";
    /// <summary>
    /// Query the database for periodic plan details using RECURRINGID, PWTOKEN or PWCID.
    /// </summary>
    public const string GetPeriodicPlan = "GETPERIODICPLAN";
    /// <summary>
    /// Delete a periodic plan.
    /// </summary>
    public const string DeleteRecurring = "DELETERECURRING";
    /// <summary>
    /// Sends a receipt for a given transaction.
    /// </summary>
    public const string SendReceipt = "SENDRECEIPT";
    /// <summary>
    /// Sends a receipt for a given transaction.
    /// </summary>
    public const string SearchChargeback = "SEARCHCB";

    /// <summary>
    /// Bin Validation
    /// </summary>
    public const string BinValidation = "BINVALIDATION";
    /// <summary>
    /// Capture
    /// </summary>
    public const string Capture = "CAPTURE";
}