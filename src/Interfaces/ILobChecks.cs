using Lob.Net.Models;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobChecks
    {
        Task<CheckResponse> CreateAsync(CheckRequest check, string idempotencyKey = null);
        Task<CheckResponse> RetrieveAsync(string id);
        Task<DeleteResponse> DeleteAsync(string id);
        Task<ListResponse<CheckResponse>> ListAsync(CheckFilter filter = null);
    }
}
