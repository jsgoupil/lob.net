using Lob.Net.Models;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobLetters
    {
        Task<LetterResponse> CreateAsync(LetterRequest letter, string idempotencyKey = null);
        Task<LetterResponse> RetrieveAsync(string id);
        Task<CancelResponse> CancelAsync(string id);
        Task<ListResponse<LetterResponse>> ListAsync(LetterFilter filter = null);
    }
}
