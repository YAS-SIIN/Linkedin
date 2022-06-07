using Linkedin.Models;
using Linkedin.Service.Request;
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
    public class RequestController : ControllerBase
    {
        //private readonly ILogger<RequestController> _logger;
        private readonly IRequestService _requestService;
        private readonly IUserService _userservice; 
        public RequestController(IRequestService requestService, IUserService userservice)
        {
            //_logger = logger;
            _requestService = requestService;
            _userservice = userservice;
        }

        [HttpGet]
        public IEnumerable<Request> Get()
        {
            return _requestService.GetAll().Take(100);
        }

        [HttpGet, Route("[action]")]
        public IEnumerable<Request> GetCancel()
        { 
            return _requestService.GetAll().Where(a => a.ExpireDateTime > DateTime.Now);
        }

        [HttpGet, Route("[action]")]
        public IEnumerable<Request> GetSubmit()
        {
            return _requestService.GetAll().Where(a=>a.Status== (short)RequestStatus.Submit);
        }

        [HttpPut]
        public Request ChangeStatusByUser(string UserId)
        {
            User RecivedUserRow = _userservice.GetAll().Where(a => a.ExternalUserId == UserId).ElementAt(0);
            Request RecivedRow = _requestService.GetAll().Where(a=>a.UserId== RecivedUserRow.Id).ElementAt(0);

            RecivedRow.UpdateDateTime = DateTime.Now;
            return _requestService.Update(RecivedRow);
        }

     
    }
}
