using Linkedin.Models;
using Linkedin.Service.Activity;
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
        //private readonly ILogger<ScheduleController> _logger;
        private readonly IScheduleService _scheduleservice; 
        private readonly IUserService _userservice;
        private readonly IActivityService _activityService;

        public ScheduleController( IScheduleService scheduleservice, 
            IUserService userservice, IActivityService activityService)
        {
            //_logger = logger;
            _scheduleservice = scheduleservice;
            _userservice = userservice;
            _activityService = activityService;
        }
        
        [HttpGet]
        public IEnumerable<object> Get()
        {                                                               
            var Qu = from a in _userservice.GetAll().ToList()
                     where a.Status == (short)UserStatus.InProgress
                     select new { a, 
                        Schedule = _scheduleservice.GetAll().Where(x => x.UserId == a.Id && x.Status == (short)ScheduleStatus.Submit).ToList() ,
                        Activity = _activityService.GetAll().Where(x => x.UserId == a.Id && x.Status == (short)ActivityStatus.Submit).ToList()
                    };
           return Qu;
        }

        [HttpGet]
        public IEnumerable<object> NextVisit()
        {
            var Qu = from a in _userservice.GetAll().ToList()
                     join b in _scheduleservice.GetAll()
                     on a.Id equals b.UserId
                     where a.Status == (short)UserStatus.InProgress
                     orderby b.Id
                     select new
                     {
                         a,
                         Schedule = _scheduleservice.GetAll().Where(x => x.UserId == a.Id && x.Status == (short)ScheduleStatus.Submit).ToList(),
                         Activity = _activityService.GetAll().Where(x => x.UserId == a.Id && x.Status == (short)ActivityStatus.Submit).ToList()
                     };
                     
            return Qu;       
        }

        [HttpGet]
        public User GetByUser(string UserId)
        {
            User RecivedUserRow = _userservice.GetAll().Where(a => a.ExternalUserId == UserId).ElementAt(0);
            RecivedUserRow.Schedule = _scheduleservice.GetAll().Where(x => x.UserId == RecivedUserRow.Id && x.Status == (short)ScheduleStatus.Submit).ToList();
            RecivedUserRow.Activity = _activityService.GetAll().Where(x => x.UserId == RecivedUserRow.Id && x.Status== (short)ActivityStatus.Submit).ToList();
            return RecivedUserRow;
        }

        [HttpPut]
        public Schedule ChangeStatusByUser(string UserId)
        {
            User RecivedUserRow = _userservice.GetAll().Where(a => a.ExternalUserId == UserId).ElementAt(0);
            Schedule RecivedRow = _scheduleservice.GetAll().Where(a => a.UserId == RecivedUserRow.Id).ElementAt(0);
            RecivedRow.Status = (short)ScheduleStatus.Deleted;
            RecivedRow.UpdateDateTime = DateTime.Now;
            return _scheduleservice.Update(RecivedRow);
        }
 
        //----------------------------
        [HttpPost]
        public Schedule Post([FromBody] Schedule Schedule)
        {
            return _scheduleservice.Insert(Schedule);
        }

        [HttpPut]
        public Schedule Update([FromBody] Schedule Schedule)
        {
            return _scheduleservice.Update(Schedule);
        }

        [HttpDelete]
        public Schedule Delete([FromBody] Schedule Schedule)
        {
            return _scheduleservice.Delete(Schedule);
        }
    }
}
