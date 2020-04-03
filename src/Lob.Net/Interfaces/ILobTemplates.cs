using Lob.Net.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobTemplates
    {
        Task<TemplateResponse> CreateAsync(TemplateRequest template, string idempotencyKey = default, CancellationToken cancellationToken = default);
        Task<TemplateResponse> RetrieveAsync(string id, CancellationToken cancellationToken = default);
        Task<TemplateResponse> UpdateAsync(string id, TemplateUpdate templateUpdate, CancellationToken cancellationToken = default);
        Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<ListResponse<TemplateResponse>> ListAsync(TemplateFilter filter = default, CancellationToken cancellationToken = default);
#if NETSTANDARD2_1
        IAsyncEnumerable<TemplateResponse> ListObjectsAsync(TemplateFilter filter = default, CancellationToken cancellationToken = default);
#endif

        Task<TemplateVersionResponse> CreateVersionAsync(string templateId, TemplateVersionRequest templateVersion, string idempotencyKey = default, CancellationToken cancellationToken = default);
        Task<TemplateVersionResponse> RetrieveVersionAsync(string templateId, string versionId, CancellationToken cancellationToken = default);
        Task<TemplateVersionResponse> UpdateVersionAsync(string templateId, string versionId, string description, CancellationToken cancellationToken = default);
        Task<DeleteResponse> DeleteVersionAsync(string templateId, string versionId, CancellationToken cancellationToken = default);
        Task<ListResponse<TemplateVersionResponse>> ListVersionAsync(string templateId, TemplateVersionFilter filter = default, CancellationToken cancellationToken = default);
#if NETSTANDARD2_1
        IAsyncEnumerable<TemplateVersionResponse> ListVersionObjectsAsync(string templateId, TemplateVersionFilter filter = default, CancellationToken cancellationToken = default);
#endif
    }
}
