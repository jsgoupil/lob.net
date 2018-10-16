using System.Threading.Tasks;

namespace Lob.Net
{
    internal interface ILobCommunicator
    {
        Task<T> GetAsync<T>(string url);
        Task<T> DeleteAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object body, string idempotencyKey = null);
    }
}
