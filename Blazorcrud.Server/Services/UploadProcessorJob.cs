using Blazorcrud.Server.Models;
using Blazorcrud.Shared.Models;
using Quartz;
using System.Text.Json;

namespace Blazorcrud.Server.Services
{
    [DisallowConcurrentExecution]
    public class UploadProcessorJob : IJob
    {
        private readonly ILogger<UploadProcessorJob> _logger;
        private readonly AppDbContext _appDbContext;

        public UploadProcessorJob(ILogger<UploadProcessorJob> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("File Processing Job Initiated: " + DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss.fffK"));
            List<Upload> unprocessedUploads = GetUnprocessedUploads();
            _logger.LogInformation("Count of files requiring processing: " + unprocessedUploads.Count);
            if (unprocessedUploads.Count > 0)
            {
                ProcessFiles(unprocessedUploads);
            }
            _logger.LogInformation("File Processing Job Completed: " + DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss.fffK"));
            return Task.CompletedTask;
        }

        private List<Upload> GetUnprocessedUploads()
        {
            List<Upload> unprocessedUploads = (from u in _appDbContext.Uploads
                                                where u.ProcessedTimestamp.HasValue != true
                                                select u).ToList();
            return unprocessedUploads;
        }

        private void ProcessFiles(List<Upload> uploads)
        {
            foreach (Upload u in uploads)
            {
                byte[] base64Data = System.Convert.FromBase64String(u.FileContent);
                string base64Decoded = System.Text.ASCIIEncoding.UTF8.GetString(base64Data);

                try
                {
                    List<Person> people = JsonSerializer.Deserialize<List<Person>>(base64Decoded);
                    foreach (Person p in people)
                    {
                        _appDbContext.People.Add(p);
                    }
                    _appDbContext.SaveChanges();

                    // Update file processing status in the system
                    u.ProcessedTimestamp = DateTime.Now;
                    _appDbContext.SaveChanges();
                    _logger.LogInformation("File Id: " + u.Id + " Name: " + u.FileName + " processed at " + DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss.fffK"));
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Exception Type: " + ex.GetType());
                }
            }
        }
    }
}