using Lob.Net.Models;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobPostcards
    {
        Task<PostcardResponse> CreateAsync(PostcardRequest postcard, string idempotencyKey = null);
        Task<PostcardResponse> RetrieveAsync(string id);
        Task<DeleteResponse> DeleteAsync(string id);
        Task<ListResponse<PostcardResponse>> ListAsync(PostcardFilter filter = null);
    }
}
