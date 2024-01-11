using Beacon.Shared.Data;
using Beacon.Shared.Models;

namespace Beacon.Client.Services
{
    public interface IUploadService
    {
        Task<PagedResult<Upload>> GetUploads(string? name, string page);
        Task<Upload> GetUpload(int id);

        Task DeleteUpload(int id);

        Task AddUpload(Upload upload);

        Task UpdateUpload(Upload upload);
    }
}