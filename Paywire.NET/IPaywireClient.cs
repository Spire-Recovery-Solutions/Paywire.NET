using Paywire.NET.Models.Base;

namespace Paywire.NET;

public interface IPaywireClient : IDisposable
{
    Task<T> SendRequest<T>(BasePaywireRequest request, CancellationToken ct = default) where T : BasePaywireResponse, new();
}
