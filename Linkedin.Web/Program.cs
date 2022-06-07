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
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();    
                });
    }
}
