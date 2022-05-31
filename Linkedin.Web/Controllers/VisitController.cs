using Linkedin.Models;
using Linkedin.Service.Visit;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linkedin.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly ILogger<VisitController> _logger;
        private readonly IVisitService _visitservice;
        public VisitController(ILogger<VisitController> logger, IVisitService visitservice)
        {
            _logger = logger;
            _visitservice = visitservice;
        }

        [HttpGet]
        public IEnumerable<Visit> Get()
        {
            return _visitservice.GetAll();
        }

        [HttpPost]
        public Visit Post([FromBody] Visit Visit)
        {
            return _visitservice.Insert(Visit);
        }

        [HttpPut]
        public Visit Update([FromBody] Visit Visit)
        {
            return _visitservice.Update(Visit);
        }

        [HttpDelete]
        public Visit Delete([FromBody] Visit Visit)
        {
            return _visitservice.Delete(Visit);
        }
    }
}
