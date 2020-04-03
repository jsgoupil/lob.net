using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Lob.Net.Models;

namespace Lob.Net
{
    public class LobTemplates : LobBaseRequest<TemplateRequest, TemplateResponse, TemplateFilter>, ILobTemplates
    {
        private const string URL = "/v1/templates";
        private const string URL_VERSIONS = "/v1/templates/{0}/versions";

        public LobTemplates(
            ILobCommunicator lobCommunicator
        )
            : base(lobCommunicator, URL)
        {
        }

        public Task<TemplateResponse> UpdateAsync(string id, TemplateUpdate templateUpdate, CancellationToken cancellationToken = default)
        {
            return lobCommunicator.PostAsync<TemplateResponse>($"{url}/{id}", templateUpdate, cancellationToken);
        }

        public Task<TemplateVersionResponse> CreateVersionAsync(string templateId, TemplateVersionRequest templateVersion, string idempotencyKey = default, CancellationToken cancellationToken = default)
        {
            var url = string.Format(URL_VERSIONS, templateId); 
            return lobCommunicator.PostAsync<TemplateVersionResponse>(url, templateVersion, idempotencyKey, cancellationToken);
        }

        public Task<TemplateVersionResponse> RetrieveVersionAsync(string templateId, string versionId, CancellationToken cancellationToken = default)
        {
            var url = string.Format(URL_VERSIONS, templateId); 
            return lobCommunicator.GetAsync<TemplateVersionResponse>($"{url}/{versionId}", cancellationToken);
        }

        public Task<TemplateVersionResponse> UpdateVersionAsync(string templateId, string versionId, string description = default, CancellationToken cancellationToken = default)
        {
            var url = string.Format(URL_VERSIONS, templateId); 
            return lobCommunicator.PostAsync<TemplateVersionResponse>($"{url}/{versionId}", new
            {
                Description = description
            }, cancellationToken);
        }

        public Task<DeleteResponse> DeleteVersionAsync(string templateId, string versionId, CancellationToken cancellationToken = default)
        {
            var url = string.Format(URL_VERSIONS, templateId); 
            return lobCommunicator.DeleteAsync<DeleteResponse>($"{url}/{versionId}", cancellationToken);
        }

        public async Task<ListResponse<TemplateVersionResponse>> ListVersionAsync(string templateId, TemplateVersionFilter filter = default, CancellationToken cancellationToken = default)
        {
            var url = string.Format(URL_VERSIONS, templateId);
            var queryString = await GetListQueryStringAsync(filter);
            return await ListAsync<TemplateVersionResponse>($"{url}?{queryString}");
        }

#if NETSTANDARD2_1
        public async IAsyncEnumerable<TemplateVersionResponse> ListVersionObjectsAsync(string templateId, TemplateVersionFilter filter = default, [EnumeratorCancellation]CancellationToken cancellationToken = default)
        {
            var url = string.Format(URL_VERSIONS, templateId);
            var queryString = await GetListQueryStringAsync(filter);
            await foreach (var value in InternalListObjectsAsync<TemplateVersionResponse>($"{url}?{queryString}", cancellationToken))
            {
                yield return value;
            }
        }
#endif
    }
}
