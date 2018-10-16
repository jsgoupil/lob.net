using Lob.Net.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lob.Net
{
    internal class LobBaseRequest<ModelRequest, ModelResponse, ModelFilter>
        where ModelFilter : BaseFilter
    {
        protected ILobCommunicator lobCommunicator;
        protected readonly string url;

        public LobBaseRequest(
            ILobCommunicator lobCommunicator,
            string url
        )
        {
            this.lobCommunicator = lobCommunicator;
            this.url = url;
        }

        public Task<ModelResponse> CreateAsync(ModelRequest model, string idempotencyKey = null)
        {
            return lobCommunicator.PostAsync<ModelResponse>(url, model, idempotencyKey);
        }

        public Task<ModelResponse> RetrieveAsync(string id)
        {
            return lobCommunicator.GetAsync<ModelResponse>($"{url}/{id}");
        }

        public Task<DeleteResponse> DeleteAsync(string id)
        {
            return lobCommunicator.DeleteAsync<DeleteResponse>($"{url}/{id}");
        }

        public async Task<ListResponse<ModelResponse>> ListAsync(ModelFilter filter = null)
        {
            var queryString = filter != null ? await new FormUrlEncodedContent(filter.GetFilterDictionary()).ReadAsStringAsync() : string.Empty;
            return await lobCommunicator.GetAsync<ListResponse<ModelResponse>>($"{url}?{queryString}");
        }
    }
}
