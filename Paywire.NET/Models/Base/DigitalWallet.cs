namespace Paywire.NET.Models.Base;

/// <summary>
/// Digital wallet data for Apple Pay and Google Pay transactions.
/// Use EITHER the encrypted path (DWPAYLOAD) OR the decrypted path (CAVV/ECI fields), not both.
/// </summary>
public class DigitalWallet
{
    /// <summary>
    /// Wallet type. "A" for Apple Pay, "G" for Google Pay.
    /// </summary>
    public string? DWTYPE { get; set; }

    /// <summary>
    /// Base64-encoded encrypted payload from the digital wallet (encrypted path).
    /// </summary>
    public string? DWPAYLOAD { get; set; }

    /// <summary>
    /// "TRUE" or "FALSE" — indicates if the card data has been decrypted (decrypted path).
    /// </summary>
    public string? ISTDES { get; set; }

    /// <summary>
    /// CAVV data from the authenticator (decrypted path).
    /// </summary>
    public string? CAVV { get; set; }

    /// <summary>
    /// ECI value from the authenticator (decrypted path).
    /// </summary>
    public string? ECI { get; set; }

    /// <summary>
    /// UCAF indicator — MasterCard only (decrypted path).
    /// </summary>
    public string? UCAF { get; set; }
}
