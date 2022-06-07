using Linkedin.Models;
using Linkedin.Service.Activity;
using Linkedin.Service.UserService;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static Linkedin.Common.TypeEnum;

namespace Linkedin.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        //private readonly ILogger<ActivityController> _logger;
        private readonly IActivityService _activityService;
        private readonly IUserService _userservice;
        public ActivityController(IActivityService activityService, IUserService userservice)
        {
            //_logger = logger;
            _activityService = activityService;
            _userservice = userservice;
        }

        [HttpGet]
        public IEnumerable<Activity> Get()
        {
            return _activityService.GetAll().Take(100);
        }

        [HttpGet]
        public Activity GetById(int Id)
        {
            return _activityService.GetById(Id);
        }

        [HttpGet]
        public IEnumerable<Activity> GetByUser(string UserId)
        {
            User RecivedUserRow = _userservice.GetAll().Where(a => a.ExternalUserId == UserId).ElementAt(0);
        
            return _activityService.GetAll().Where(a => a.UserId == RecivedUserRow.Id);
        }

        [HttpPut]
        public bool ChangeStatusByUser(string UserId)
        {
            User RecivedUserRow = _userservice.GetAll().Where(a => a.ExternalUserId == UserId).ElementAt(0);
            IEnumerable<Activity> lstActivities = _activityService.GetAll().Where(a => a.UserId == RecivedUserRow.Id);

            foreach (Activity item in lstActivities)
            {
                item.UpdateDateTime = DateTime.Now;
                _activityService.Update(item);
            }
                                                        
            return true;
        }

        [HttpPost]
        public Activity Post([FromBody] Activity Activity)
        {
            Activity.CreateDateTime = DateTime.Now;
            return _activityService.Insert(Activity);
        }

        //---------------------------
        [HttpPut]
        public Activity Update([FromBody] Activity Activity)
        {
            return _activityService.Update(Activity);
        }


        [HttpDelete]
        public Activity Delete([FromBody] Activity Activity)
        {
            return _activityService.Delete(Activity);
        }
    }
}
