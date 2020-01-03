using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace BlazorCrud.Server.Services
{
    [DisallowConcurrentExecution]
    public class HeatbeatJob : IJob
    {
        private readonly ILogger<HeatbeatJob> _logger;
        public HeatbeatJob(ILogger<HeatbeatJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Ping from Quartz-scheduled heartbeat job");
            return Task.CompletedTask;
        }
    }
}
