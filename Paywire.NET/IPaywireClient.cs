using System;
using System.Threading;
using System.Threading.Tasks;
using Paywire.NET.Models.Base;

namespace Paywire.NET
{
    public interface IPaywireClient : IDisposable
    {
        Task<T> SendRequest<T>(BasePaywireRequest request, CancellationToken cancellationToken = default) where T : BasePaywireResponse, new();
    }
}
