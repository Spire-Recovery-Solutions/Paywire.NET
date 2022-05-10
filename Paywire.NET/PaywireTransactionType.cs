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
    public static readonly string PreAuth = "PREAUTH";
    public static readonly string GetAuthToken = "GETAUTHTOKEN";
    public static readonly string GetConsumerFee = "GETCONSUMERFEE";
    public static readonly string CreateCustomer = "CREATECUSTOMER";
    public static readonly string GetCustomerTokens = "GETCUSTOKENS";
    public static readonly string StoreToken = "STORETOKEN";
    public static readonly string RemoveToken = "REMOVETOKEN";
    public static readonly string Verification = "VERIFICATION";
    public static readonly string BatchInquiry = "BATCHINQUIRY";
    public static readonly string Close = "CLOSE";
    public static readonly string SearchTransactions = "SEARCHTRANS";
    public static readonly string GetPeriodicPlan = "GETPERIODICPLAN";
    public static readonly string DeleteRecurring = "DELETERECURRING";
    public static readonly string SendReceipt = "SENDRECEIPT";
}

/*
      * SALE	Charge a card or bank account (if applicable).
      * VOID	Void a transaction. The transaction amount must match the amount of the original transaction, and the PWUNIQUEID must match the unique identifier associated with the transaction to void. The transaction must be in the current open batch to void it.
      * CREDIT	Credit a transaction. The transaction amount must be equal to or less than the amount to credit, and the PWUNIQUEID must match the unique identifier associated with the transaction to credit. Only transactions in a closed batch with a status of SETTLED can be credited.
      * PREAUTH	Pre-authorize a card.
      * GETAUTHTOKEN	Exchange your credentials for an AUTHTOKEN to use when calling the OSBP.
      * GETCONSUMERFEE	Input the sale amount to get adjustment, tax, and total transaction amounts. Relevant for merchants configured with Cash Discount or Convenience Fees.
      * CREATECUSTOMER	Creates a Customer in the Paywire Vault.
      * GETCUSTOKENS	Lists tokens stored against a given customer.
      * STORETOKEN	Validate a card and return a token.
      * REMOVETOKEN	Delete an existing token from Paywire.
      * VERIFICATION	Verification transaction will verify the customer's card or bank account before submitting the payment.
      * BATCHINQUIRY	Get the current open batch summary.
      * CLOSE	Close the current open batch.
      * SEARCHTRANS	Query the database for transaction results.
      * GETPERIODICPLAN	Query the database for periodic plan details using RECURRINGID, PWTOKEN or PWCID.
      * DELETERECURRING	Delete a periodic plan.
      * SENDRECEIPT	Sends a receipt for a given transaction.
      */