using Linkedin.Models;
using Linkedin.Service.Activity;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return _ActivityService.GetAll();
        }

        [HttpPost]
        public Activity Post([FromBody] Activity User)
        {
            return _ActivityService.Insert(User);
        }

        [HttpPut]
        public Activity Update([FromBody] Activity User)
        {
            return _ActivityService.Update(User);
        }

        [HttpDelete]
        public Activity Delete([FromBody] Activity User)
        {
            return _ActivityService.Delete(User);
        }
    }
}
