using Linkedin.Models;
using Linkedin.Service.Request;

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
            return _requestService.GetAll().Take(100);
        }

        [HttpGet]
        public IEnumerable<Request> GetCancel()
        {
            return _requestService.GetAll().Where(a => a.Status == (short)RequestStatus.Canceled);
        }

        [HttpGet]
        public IEnumerable<Request> GetSubmit()
        {
            return _requestService.GetAll().Where(a=>a.Status== (short)RequestStatus.Submit);
        }

        [HttpPost]
        public Request Post([FromBody] Request Request)
        {
            return _requestService.Insert(Request);
        }


        [HttpDelete]
        public Request DeleteByUser(string UserId)
        {
            Request RecivedRow = _requestService.GetAll().Where(a=>a.UserId==UserId).ElementAt(0);
            return _requestService.Delete(RecivedRow);
        }

        [HttpPut]
        public Request Update([FromBody] Request Request)
        {
            return _requestService.Update(Request);
        }

        [HttpDelete]
        public Request Delete([FromBody] Request Request)
        {
            return _requestService.Delete(Request);
        }
    }
}
