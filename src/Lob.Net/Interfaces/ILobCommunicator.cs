using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobCommunicator
    {
        Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default);
        Task<T> DeleteAsync<T>(string url, CancellationToken cancellationToken = default);
        Task<T> PostAsync<T>(string url, object body, CancellationToken cancellationToken = default);
        Task<T> PostAsync<T>(string url, object body, string idempotencyKey = default, CancellationToken cancellationToken = default);
        Task<T> PostAsync<T>(string url, object body, IDictionary<string, string> extraHeaders = null, CancellationToken cancellationToken = default);
    }
}
