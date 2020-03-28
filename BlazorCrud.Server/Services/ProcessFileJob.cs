using BlazorCrud.Shared.Data;
using BlazorCrud.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorCrud.Server.Services
{
    [DisallowConcurrentExecution]
    public class ProcessFileJob : IJob
    {
        private readonly IServiceProvider _provider;
        private PatientContext _patientContext;
        private OrganizationContext _organizationContext;
        private ClaimContext _claimContext;
        private UploadContext _uploadContext;
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
                _uploadContext = scope.ServiceProvider.GetService<UploadContext>();
                _patientContext = scope.ServiceProvider.GetService<PatientContext>();
                _organizationContext = scope.ServiceProvider.GetService<OrganizationContext>();
                _claimContext = scope.ServiceProvider.GetService<ClaimContext>();
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
            List<Upload> unprocessedUploads = (from u in _uploadContext.Uploads
                                                   where u.ProcessedTimestamp.HasValue != true
                                                   select u).ToList();
            return unprocessedUploads;
        }

        private void ProcessFiles(List<Upload> uploads)
        {
            foreach (Upload u in uploads)
            {
                // Base64 decode file
                byte[] base64Data = System.Convert.FromBase64String(u.FileContent);
                string base64Decoded = System.Text.ASCIIEncoding.UTF8.GetString(base64Data);

                try
                {
                    // Deserialize and process JSON file, persisting entities to database
                    if (u.FileType.ToLower() == "patients")
                    {
                        List<Patient> patients = JsonSerializer.Deserialize<List<Patient>>(base64Decoded);
                        foreach (Patient p in patients)
                        {
                            p.ModifiedDate = DateTime.Now;
                            _patientContext.Patients.Add(p);
                        }
                        _patientContext.SaveChanges();
                    }

                    if (u.FileType.ToLower() == "organizations")
                    {
                        List<Organization> organizations = JsonSerializer.Deserialize<List<Organization>>(base64Decoded);
                        foreach (Organization o in organizations)
                        {
                            o.ModifiedDate = DateTime.Now;
                            _organizationContext.Organizations.Add(o);
                        }
                        _organizationContext.SaveChanges();
                    }

                    if (u.FileType.ToLower() == "claims")
                    {
                        List<Claim> claims = JsonSerializer.Deserialize<List<Claim>>(base64Decoded);
                        foreach (Claim c in claims)
                        {
                            c.ModifiedDate = DateTime.Now;
                            _claimContext.Claims.Add(c);
                        }
                        _claimContext.SaveChanges();
                    }
                }

                catch (Exception ex)
                {
                    _logger.LogInformation("Exception Type: " + ex.GetType());
                }

                // Update file processing status in the system
                u.ProcessedTimestamp = DateTime.Now;
                _logger.LogInformation("File Id: " + u.Id + " Name: " + u.FileType + " processed at " + DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss.fffK"));
            }
            _uploadContext.SaveChanges();
        }
    }
}

