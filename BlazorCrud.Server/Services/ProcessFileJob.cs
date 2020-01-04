using BlazorCrud.Shared.Data;
using BlazorCrud.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCrud.Server.Services
{
    [DisallowConcurrentExecution]
    public class ProcessFileJob : IJob
    {
        private readonly IServiceProvider _provider;
        private UploadContext _context;
        private readonly ILogger<ProcessFileJob> _logger;

        public ProcessFileJob(IServiceProvider provider, IConfiguration config, ILogger<ProcessFileJob> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (var scope = _provider.CreateScope())
            {
                _context = scope.ServiceProvider.GetService<UploadContext>();
                _logger.LogInformation("File Processing Job Initiated: " + DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss.fffK"));
                List<Upload> unprocessedUploads = GetUnprocessedUploads();
                _logger.LogInformation("Count of files requiring processing: " + unprocessedUploads.Count);
                if (unprocessedUploads.Count > 0)
                {
                    ProcessFiles(unprocessedUploads);
                }
                _logger.LogInformation("File Processing Job Completed: " + DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss.fffK"));
            }
            return Task.CompletedTask;
        }

        private List<Upload> GetUnprocessedUploads()
        {
            List<Upload> unprocessedUploads = (from u in _context.Uploads
                                                   where u.ProcessedTimestamp.HasValue != true
                                                   select u).ToList();
            return unprocessedUploads;
        }

        private void ProcessFiles(List<Upload> uploads)
        {
            foreach (Upload u in uploads)
            {
                u.ProcessedTimestamp = DateTime.Now;
                _logger.LogInformation("File Id: " + u.Id + " Name: " + u.FileName + " processed at " + DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss.fffK"));
            }
            _context.SaveChanges();
        }
    }
}

