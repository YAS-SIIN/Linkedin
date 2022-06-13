using Linkedin.Entities.Context;
using Linkedin.Entities.UnitOfWork;
using Linkedin.Models;
using Linkedin.Service.Request;
using Linkedin.Service.Schedule;
using Linkedin.Service.UserService;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IUserService _userservice;
        private readonly IScheduleService _scheduleservice;
        private readonly IRequestService _requestService;

        private readonly MyDataBase _myDataBase;
        private readonly IUnitOfWork _unitOfWork;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceProvider, 
            IConfiguration configuration)
        {
            _logger = logger;
           var a= serviceProvider.CreateScope();                                
            _unitOfWork = a.ServiceProvider.GetRequiredService<IUnitOfWork>();       
            _userservice = a.ServiceProvider.GetRequiredService<IUserService>();
            _scheduleservice = a.ServiceProvider.GetRequiredService<IScheduleService>();
            _requestService = a.ServiceProvider.GetRequiredService<IRequestService>();
            Configuration = configuration;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(double.Parse(Configuration["WorkerTimePeriod"])));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            int countUserRow = int.Parse(Configuration["CountUserRow"]);

            var Qu2 = from a in _userservice.GetAll().ToList()
                      join b in _scheduleservice.GetAll()
                      on a.Id equals b.UserId
                      where a.Status == (short)UserStatus.InProgress && b.Status == (short)ScheduleStatus.Submit
                      orderby b.Id
                      select a;

            var endCount = countUserRow - Qu2.Count();

            if (endCount > 0)
            {
                var Qu = _userservice.GetAll().ToList()
    .Where(x => x.Status == (short)UserStatus.Submit && !_scheduleservice.GetAll().Where(a => a.Status == (short)ScheduleStatus.Submit).Select(a => a.Id).Contains(x.Id))
.OrderBy(x => x.Id).Take(endCount);

                foreach (var item in Qu)
                {
                    Schedule objSchedule = new Schedule();

                    objSchedule.UserId = Qu.ToList().ElementAt(0).Id;
                    objSchedule.Status = (short)ScheduleStatus.Submit;
                    objSchedule.CreateDateTime = DateTime.Now;
                    objSchedule.UpdateDateTime = DateTime.Now;
                    _scheduleservice.Insert(objSchedule);

       
                    item.Status = (short)UserStatus.InProgress;
                    _userservice.Update(item);

                    Request objRequest = new Request();

                    objRequest.UserId = Qu.ToList().ElementAt(0).Id;
                    objRequest.Status = (short)ScheduleStatus.Submit;
                    objRequest.CreateDateTime = DateTime.Now;
                    objRequest.UpdateDateTime = DateTime.Now;
                    _requestService.Insert(objRequest);   
                }
            }

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
