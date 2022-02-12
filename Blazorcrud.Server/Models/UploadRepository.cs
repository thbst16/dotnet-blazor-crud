using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Server.Models
{
    public class UploadRepository : IUploadRepository
    {
        private readonly AppDbContext _appDbContext;

        public UploadRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Upload> AddUpload(Upload upload)
        {
            var result = await _appDbContext.Uploads.AddAsync(upload);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Upload?> DeleteUpload(int Id)
        {
            var result = await _appDbContext.Uploads.FirstOrDefaultAsync(u => u.Id==Id);
            if (result!=null)
            {
                _appDbContext.Uploads.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Upload not found");
            }
            return result;
        }

        public async Task<Upload?> GetUpload(int Id)
        {
            var result = await _appDbContext.Uploads.FirstOrDefaultAsync(u => u.Id==Id);
            if(result != null)
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException("Upload not found");
            }
        }

        public PagedResult<Upload> GetUploads(string? name, int page)
        {
            int pageSize = 5;

            if (name != null)
            {
                return _appDbContext.Uploads
                    .Where(u => u.FileName.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(u => u.UploadTimestamp)
                    .GetPaged(page, pageSize);
            }
            else
            {
                return _appDbContext.Uploads
                    .OrderBy(u => u.UploadTimestamp)
                    .GetPaged(page, pageSize);
            }
        }

        public async Task<Upload?> UpdateUpload(Upload upload)
        {
            var result = await _appDbContext.Uploads.FirstOrDefaultAsync(u => u.Id==upload.Id);
            if (result!=null)
            {
                // Update existing upload
                _appDbContext.Entry(result).CurrentValues.SetValues(upload);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Upload not found");
            }
            return result;
        }
    }
}