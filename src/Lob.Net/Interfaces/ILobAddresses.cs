using Lob.Net.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobAddresses
    {
        Task<AddressResponse> CreateAsync(AddressRequest address, string idempotencyKey = default, CancellationToken cancellationToken = default);
        Task<AddressResponse> RetrieveAsync(string id, CancellationToken cancellationToken = default);
        Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<ListResponse<AddressResponse>> ListAsync(AddressFilter filter = default, CancellationToken cancellationToken = default);
#if NETSTANDARD2_1
        IAsyncEnumerable<AddressResponse> ListObjectsAsync(AddressFilter filter = default, CancellationToken cancellationToken = default);
#endif
    }
}
