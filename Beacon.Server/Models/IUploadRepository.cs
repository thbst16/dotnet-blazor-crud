using Beacon.Shared.Data;
using Beacon.Shared.Models;

namespace Beacon.Server.Models
{
    public interface IUploadRepository
    {
        PagedResult<Upload> GetUploads(string? name, int page);
        Task<Upload?> GetUpload(int Id);
        Task<Upload> AddUpload(Upload upload);
        Task<Upload?> UpdateUpload(Upload upload);
        Task<Upload?> DeleteUpload(int id);
    }
}