using Lob.Net.Models;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobAddresses
    {
        Task<AddressResponse> CreateAsync(AddressRequest address, string idempotencyKey = null);
        Task<AddressResponse> RetrieveAsync(string id);
        Task<DeleteResponse> DeleteAsync(string id);
        Task<ListResponse<AddressResponse>> ListAsync(AddressFilter filter = null);
    }
}
