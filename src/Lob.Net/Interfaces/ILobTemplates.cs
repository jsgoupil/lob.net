using Lob.Net.Models;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobTemplates
    {
        Task<TemplateResponse> CreateAsync(TemplateRequest template, string idempotencyKey = null);
        Task<TemplateResponse> RetrieveAsync(string id);
        Task<TemplateResponse> UpdateAsync(string id, TemplateUpdate templateUpdate);
        Task<DeleteResponse> DeleteAsync(string id);
        Task<ListResponse<TemplateResponse>> ListAsync(TemplateFilter filter = null);

        Task<TemplateVersionResponse> CreateVersionAsync(string templateId, TemplateVersionRequest templateVersion, string idempotencyKey = null);
        Task<TemplateVersionResponse> RetrieveVersionAsync(string templateId, string versionId);
        Task<TemplateVersionResponse> UpdateVersionAsync(string templateId, string versionId, string description);
        Task<DeleteResponse> DeleteVersionAsync(string templateId, string versionId);
        Task<ListResponse<TemplateVersionResponse>> ListVersionAsync(string templateId, TemplateVersionFilter filter = null);
    }
}
