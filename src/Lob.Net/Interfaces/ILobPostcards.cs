using Lob.Net.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobPostcards
    {
        Task<PostcardResponse> CreateAsync(PostcardRequest postcard, string idempotencyKey = default, CancellationToken cancellationToken = default);
        Task<PostcardResponse> RetrieveAsync(string id, CancellationToken cancellationToken = default);
        Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<ListResponse<PostcardResponse>> ListAsync(PostcardFilter filter = null, CancellationToken cancellationToken = default);
#if NETSTANDARD2_1
        IAsyncEnumerable<PostcardResponse> ListObjectsAsync(PostcardFilter filter = null, CancellationToken cancellationToken = default);
#endif
    }
}
