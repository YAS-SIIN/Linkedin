using Linkedin.Models;
using Linkedin.Service.Activity;
using Linkedin.Service.Request;
using Linkedin.Service.Schedule;
using Linkedin.Service.UserService;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static Linkedin.Common.TypeEnum;

namespace Linkedin.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ILogger<ScheduleController> _logger;
        private readonly IScheduleService _scheduleservice; 
        private readonly IUserService _userservice;
        private readonly IActivityService _activityService;
        private readonly IRequestService _requestService;

        public ScheduleController(ILogger<ScheduleController> logger,IScheduleService scheduleservice, 
            IUserService userservice, IRequestService requestService, IActivityService activityService)
        {
            _logger = logger;
            _scheduleservice = scheduleservice;
            _userservice = userservice;
            _activityService = activityService;
            _requestService = requestService;
        }
        
        [HttpGet]
        public IEnumerable<object> Get()
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            return _userservice.GetAll(x => x.Status == (short)UserStatus.InProgress).
                Select(x => new { x, Schedule = _scheduleservice.GetAll().Where(a => a.UserId == x.Id && a.Status == (short)ScheduleStatus.Submit).ToList(),
                    Activity = _activityService.GetAll().Where(b => b.UserId == x.Id && b.Status == (short)ActivityStatus.Submit).ToList(),
                    Request = _requestService.GetAll().Where(x => x.UserId == x.Id).ToList()
                });              
        }

        [HttpGet,Route("[action]")]
        public object NextVisit()
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            var Qu = from a in _userservice.GetAll().ToList()
                     join b in _scheduleservice.GetAll()
                     on a.Id equals b.UserId
                     where a.Status == (short)UserStatus.InProgress
                     orderby b.Id
                     select new
                     {
                         a,
                         Schedule = _scheduleservice.GetAll(x => x.UserId == a.Id && x.Status == (short)ScheduleStatus.Submit).ToList(),
                         Activity = _activityService.GetAll(x => x.UserId == a.Id && x.Status == (short)ActivityStatus.Submit).ToList(),
                         Request = _requestService.GetAll(x => x.UserId == a.Id).ToList()
                     };

                     
            return Qu.ElementAt(0);       
        }

        [HttpGet, Route("[action]")]      
        public User GetByUser(string UserId)
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            User RecivedUserRow = _userservice.GetAll(a => a.ExternalUserId == UserId).ToList().ElementAt(0);
            RecivedUserRow.Schedule = _scheduleservice.GetAll(x => x.UserId == RecivedUserRow.Id && x.Status == (short)ScheduleStatus.Submit).ToList();
            RecivedUserRow.Activity = _activityService.GetAll(x => x.UserId == RecivedUserRow.Id && x.Status== (short)ActivityStatus.Submit).ToList();
            RecivedUserRow.Request = _requestService.GetAll(x => x.UserId == RecivedUserRow.Id).ToList().FirstOrDefault() ;
            return RecivedUserRow;
        }

        [HttpPut, Route("[action]")]
        public Schedule ChangeStatusByUser(string UserId, short Status)
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            User RecivedUserRow = _userservice.GetByUserId(UserId);
            Schedule RecivedRow = _scheduleservice.GetAll(a => a.UserId == RecivedUserRow.Id).FirstOrDefault();
            RecivedRow.Status = Status;
            RecivedRow.UpdateDateTime = DateTime.Now;
            return _scheduleservice.Update(RecivedRow);
        }
        
    }
}
