using Linkedin.Entities.Context;
using Linkedin.Entities.GenericRepository;
using Linkedin.Entities.UnitOfWork;
using Linkedin.Models;
using Linkedin.Service.Activity;
using Linkedin.Service.Request;
using Linkedin.Service.Schedule;
using Linkedin.Service.UserService;
using Linkedin.Service.Visit;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linkedin.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
            {

                services.AddDbContext<MyDataBase>(options => options.UseSqlServer(@"Data Source=(localdb)\\MSSQLLocalDB; UID=sa; Password=ABCabc123456;Database=Linkdin;"));

                services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
                services.AddTransient<IUnitOfWork, UnitOfWork>();
                services.AddTransient<IGenericRepository<Activity>, GenericRepository<Activity>>();
                services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
                services.AddTransient<IGenericRepository<Request>, GenericRepository<Request>>();
                services.AddTransient<IGenericRepository<Schedule>, GenericRepository<Schedule>>();
                services.AddTransient<IGenericRepository<Visit>, GenericRepository<Visit>>();

                services.AddTransient<IActivityService, ActivityService>();
                services.AddTransient<IScheduleService, ScheduleService>();
                services.AddTransient<IRequestService, RequestService>();
                services.AddTransient<IUserService, UserService>();
                services.AddTransient<IVisitService, VisitService>();
                services.AddScoped<Worker>();

                services.AddHostedService<Worker>();

                services.AddMemoryCache();
            }) 
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();    
                });
    }
}
