using Linkedin.Service.Schedule;
using Linkedin.Service.UserService;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using static Linkedin.Common.TypeEnum;

namespace Linkedin.Web
{
    public class Worker : IHostedService, IDisposable
    {                                    
        public IConfiguration Configuration { get; }
        private int executionCount = 0;
        private readonly ILogger<Worker> _logger;
        private Timer? _timer = null;

        private readonly IScheduleService _scheduleservice;
        private readonly IUserService _userservice;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, 
            IScheduleService scheduleservice,
            IUserService userservice)
        {
            _logger = logger;
                                                          
            _userservice = userservice;
            _scheduleservice = scheduleservice;
            Configuration = configuration;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(double.Parse( Configuration["WorkerTimePeriod"])));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            //var Qu = _userservice.GetAll().ToList().Where(x => x.Status == (short)UserStatus.Submit && _scheduleservice.GetAll().Select(a=>a.UserId).Contains(x.Id)).OrderBy(x => x.Id);
                

            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
