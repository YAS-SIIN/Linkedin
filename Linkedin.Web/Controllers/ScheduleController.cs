using Linkedin.Models;
using Linkedin.Service.Schedule;

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
        public ScheduleController( IScheduleService scheduleservice)
        {
            //_logger = logger;
            _scheduleservice = scheduleservice;
        }

        [HttpGet]
        public IEnumerable<Schedule> Get()
        {
            return _scheduleservice.GetAll();
        } 

        [HttpGet]
        public IEnumerable<Schedule> NextVisit()
        {
            return _scheduleservice.GetAll();
        }

        [HttpGet]
        public IEnumerable<Schedule> ByUser(string UserId)
        {
            return _scheduleservice.GetAll().Where(x=>x.UserId == UserId);
        }

        [HttpPut]
        public Schedule status(int Id)
        {
            Schedule RecivedRow = _scheduleservice.GetAll().Where(x => x.Id == Id).ElementAt(0);
            if (RecivedRow.Status == (short)ScheduleStatus.Submit)
            {
                RecivedRow.Status = (short)ScheduleStatus.Done;
            }
            else
            {
                RecivedRow.Status = (short)ScheduleStatus.Submit;
            }
           
            return _scheduleservice.Update(RecivedRow);
        }

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
