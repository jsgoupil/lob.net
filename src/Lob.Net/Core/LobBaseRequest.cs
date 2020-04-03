using Lob.Net.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Net
{
    public class LobBaseRequest<ModelRequest, ModelResponse, ModelFilter>
        where ModelFilter : BaseFilterWithMetadata
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

        public Task<ModelResponse> CreateAsync(ModelRequest model, string idempotencyKey = default, CancellationToken cancellationToken = default)
        {
            return lobCommunicator.PostAsync<ModelResponse>(url, model, idempotencyKey, cancellationToken);
        }

        public Task<ModelResponse> RetrieveAsync(string id, CancellationToken cancellationToken = default)
        {
            return lobCommunicator.GetAsync<ModelResponse>($"{url}/{id}", cancellationToken);
        }

        public Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            return lobCommunicator.DeleteAsync<DeleteResponse>($"{url}/{id}", cancellationToken);
        }

        public async Task<ListResponse<ModelResponse>> ListAsync(ModelFilter filter = default, CancellationToken cancellationToken = default)
        {
            var queryString = await GetListQueryStringAsync(filter);
            return await ListAsync<ModelResponse>($"{url}?{queryString}", cancellationToken);
        }

#if NETSTANDARD2_1
        public async IAsyncEnumerable<ModelResponse> ListObjectsAsync(ModelFilter filter = default, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var queryString = await GetListQueryStringAsync(filter);
            await foreach (var value in InternalListObjectsAsync<ModelResponse>($"{url}?{queryString}", cancellationToken))
            {
                yield return value;
            }
        }

#pragma warning disable CS8425 // Async-iterator member has one or more parameters of type 'CancellationToken' but none of them is decorated with the 'EnumeratorCancellation' attribute, so the cancellation token parameter from the generated 'IAsyncEnumerable<>.GetAsyncEnumerator' will be unconsumed
        protected async IAsyncEnumerable<T> InternalListObjectsAsync<T>(string currentUrl, CancellationToken cancellationToken)
#pragma warning restore CS8425 // Async-iterator member has one or more parameters of type 'CancellationToken' but none of them is decorated with the 'EnumeratorCancellation' attribute, so the cancellation token parameter from the generated 'IAsyncEnumerable<>.GetAsyncEnumerator' will be unconsumed
        {
            do
            {
                var listResponse = await ListAsync<T>(currentUrl);
                foreach (var modelResponse in listResponse.Data)
                {
                    yield return modelResponse;
                }

                if (string.IsNullOrEmpty(listResponse.NextUrl))
                {
                    break;
                }

                currentUrl = listResponse.NextUrl;
            } while (!cancellationToken.IsCancellationRequested);
        }
#endif

        protected Task<ListResponse<T>> ListAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            return lobCommunicator.GetAsync<ListResponse<T>>(url, cancellationToken);
        }

        protected async Task<string> GetListQueryStringAsync<T>(T filter = default)
            where T : BaseFilter
        {
            return filter != null ? await new FormUrlEncodedContent(filter.GetFilterDictionary()).ReadAsStringAsync() : string.Empty;
        }
    }
}