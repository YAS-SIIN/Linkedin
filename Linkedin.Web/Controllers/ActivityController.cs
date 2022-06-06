using Linkedin.Models;
using Linkedin.Service.Activity;

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
        private readonly IActivityService _ActivityService;
        public ActivityController(IActivityService ActivityService)
        {
            //_logger = logger;
            _ActivityService = ActivityService;
        }

        [HttpGet]
        public IEnumerable<Activity> Get()
        {
            return _ActivityService.GetAll().Take(100);
        }

        [HttpGet]
        public Activity GetById(int Id)
        {
            return _ActivityService.GetById(Id);
        }

        [HttpGet]
        public IEnumerable<Activity> ByUser(string UserId)
        {
            return _ActivityService.GetAll().Where(x => x.UserId == UserId);
        }

        [HttpPost]
        public Activity Post([FromBody] Activity Activity)
        {
            return _ActivityService.Insert(Activity);
        }

        [HttpPut]
        public Activity Update([FromBody] Activity Activity)
        {
            return _ActivityService.Update(Activity);
        }

        [HttpPut]
        public Activity Like([FromBody] Activity Activity)
        {
            Activity.Status = (short)ActivityStatus.Liked;
            return _ActivityService.Update(Activity);
        }

        [HttpDelete]
        public Activity Delete([FromBody] Activity Activity)
        {
            return _ActivityService.Delete(Activity);
        }
    }
}
