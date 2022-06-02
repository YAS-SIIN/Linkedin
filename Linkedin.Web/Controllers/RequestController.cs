using Linkedin.Models;
using Linkedin.Service.Request;

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
    public class RequestController : ControllerBase
    {
        //private readonly ILogger<RequestController> _logger;
        private readonly IRequestService _requestService;
        public RequestController(IRequestService requestService)
        {
            //_logger = logger;
            _requestService = requestService;
        }

        [HttpGet]
        public IEnumerable<Request> Get()
        {
            return _requestService.GetAll();
        }

        [HttpPost]
        public Request Post([FromBody] Request User)
        {
            return _requestService.Insert(User);
        }

        [HttpPut]
        public Request Update([FromBody] Request User)
        {
            return _requestService.Update(User);
        }

        [HttpDelete]
        public Request Delete([FromBody] Request User)
        {
            return _requestService.Delete(User);
        }
    }
}
