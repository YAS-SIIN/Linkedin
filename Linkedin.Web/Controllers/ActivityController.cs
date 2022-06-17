using Linkedin.Models;
using Linkedin.Service.Activity;
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
    public class ActivityController : ControllerBase
    {
        private readonly ILogger<ActivityController> _logger;
        private readonly IActivityService _activityService;
        private readonly IUserService _userservice;
        public ActivityController(ILogger<ActivityController> logger, IActivityService activityService, IUserService userservice)
        {
            _logger = logger;
            _activityService = activityService;
            _userservice = userservice;
        }

        [HttpGet]
        public IEnumerable<Activity> Get()
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");
            return _activityService.GetAll().Take(100);
        }

        [HttpGet, Route("[action]")]
        public Activity GetById(int Id)
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            return _activityService.GetById(Id);
        }

        [HttpGet, Route("[action]")]
        public IEnumerable<Activity> GetByUser(string UserId)
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            User RecivedUserRow = _userservice.GetByUserId(UserId);

            return _activityService.GetAll(a => a.UserId == RecivedUserRow.Id).ToList();
        }

        [HttpPut, Route("[action]")]
        public bool ChangeStatusByUser(string UserId, short Status)
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            User RecivedUserRow = _userservice.GetByUserId(UserId);
            List<Activity> lstActivities = _activityService.GetAll(a => a.UserId == RecivedUserRow.Id).ToList();

            foreach (Activity item in lstActivities)
            {
                item.UpdateDateTime = DateTime.Now;
                item.Status = Status;
            }

            _activityService.UpdateList(lstActivities);

            return true;
        }

        [HttpPost]
        public Activity Post([FromBody] Activity Activity)
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            Activity.Status = (short)UserStatus.Submit;
            Activity.CreateDateTime = DateTime.Now;
            Activity.UpdateDateTime = DateTime.Now;
            //Activity.Id = 5;
            return _activityService.Insert(Activity);
        }
           
    }
}
