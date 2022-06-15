using Linkedin.Entities.Context;
using Linkedin.Entities.GenericRepository;
using Linkedin.Entities.UnitOfWork;
using Linkedin.Models;
using Linkedin.Service.Activity;
using Linkedin.Service.Request;
using Linkedin.Service.Schedule;
using Linkedin.Service.UserService;
using Linkedin.Service.Visit;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linkedin.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Linkedin.Web", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination"));
            });

            services.AddControllersWithViews()
.AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

            services.AddDbContext<MyDataBase>(options => options.UseSqlServer(Configuration["ConnectionStrings:DatabaseConnection"]));

            //services.AddTransient<ISettingsService, SettingsService>();
            //services.AddSingleton<IConfiguration>(Configuration);

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGenericRepository<Activity>, GenericRepository<Activity>>();
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Request>, GenericRepository<Request>>();
            services.AddScoped<IGenericRepository<Schedule>, GenericRepository<Schedule>>();
            services.AddScoped<IGenericRepository<Visit>, GenericRepository<Visit>>();

            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVisitService, VisitService>();

            services.AddHostedService<Worker>();
            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Linkedin.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
