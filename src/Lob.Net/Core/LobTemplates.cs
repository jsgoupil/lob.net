using System.Net.Http;
using System.Threading.Tasks;
using Lob.Net.Models;

namespace Lob.Net
{
    internal class LobTemplates : LobBaseRequest<TemplateRequest, TemplateResponse, TemplateFilter>, ILobTemplates
    {
        private const string URL = "/v1/templates";
        private const string URL_VERSIONS = "/v1/templates/{0}/versions";

        public LobTemplates(
            ILobCommunicator lobCommunicator
        )
            : base(lobCommunicator, URL)
        {
        }

        public Task<TemplateResponse> UpdateAsync(string id, TemplateUpdate templateUpdate)
        {
            return lobCommunicator.PostAsync<TemplateResponse>($"{url}/{id}", templateUpdate);
        }

        public Task<TemplateVersionResponse> CreateVersionAsync(string templateId, TemplateVersionRequest templateVersion, string idempotencyKey = null)
        {
            var url = string.Format(URL_VERSIONS, templateId); 
            return lobCommunicator.PostAsync<TemplateVersionResponse>(url, templateVersion, idempotencyKey);
        }

        public Task<TemplateVersionResponse> RetrieveVersionAsync(string templateId, string versionId)
        {
            var url = string.Format(URL_VERSIONS, templateId); 
            return lobCommunicator.GetAsync<TemplateVersionResponse>($"{url}/{versionId}");
        }

        public Task<TemplateVersionResponse> UpdateVersionAsync(string templateId, string versionId, string description = null)
        {
            var url = string.Format(URL_VERSIONS, templateId); 
            return lobCommunicator.PostAsync<TemplateVersionResponse>($"{url}/{versionId}", new
            {
                Description = description
            });
        }

        public Task<DeleteResponse> DeleteVersionAsync(string templateId, string versionId)
        {
            var url = string.Format(URL_VERSIONS, templateId); 
            return lobCommunicator.DeleteAsync<DeleteResponse>($"{url}/{versionId}");
        }

        public async Task<ListResponse<TemplateVersionResponse>> ListVersionAsync(string templateId, TemplateVersionFilter filter = null)
        {
            var url = string.Format(URL_VERSIONS, templateId);
            var queryString = filter != null ? await new FormUrlEncodedContent(filter.GetFilterDictionary()).ReadAsStringAsync() : string.Empty;
            return await lobCommunicator.GetAsync<ListResponse<TemplateVersionResponse>>($"{url}?{queryString}");
        }
    }
}
