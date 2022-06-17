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
        private readonly ILogger<RequestController> _logger;
        private readonly IRequestService _requestService;
        private readonly IUserService _userservice; 
        public RequestController(ILogger<RequestController> logger, IRequestService requestService, IUserService userservice)
        {
            _logger = logger;
            _requestService = requestService;
            _userservice = userservice;
        }

        [HttpGet]
        public IEnumerable<Request> Get()
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            return _requestService.GetAll().Take(100);
        }

        [HttpGet, Route("[action]")]
        public IEnumerable<Request> GetCancel()
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            return _requestService.GetAll(a => a.ExpireDateTime > DateTime.Now);
        }

        [HttpGet, Route("[action]")]
        public IEnumerable<Request> GetSubmit()
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            return _requestService.GetAll(a=>a.Status== (short)RequestStatus.Submit);
        }

        [HttpPut, Route("[action]")]
        public Request ChangeStatusByUser(string UserId, short Status)
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            User RecivedUserRow = _userservice.GetByUserId(UserId);
            Request RecivedRow = _requestService.GetAll(a=>a.UserId== RecivedUserRow.Id).FirstOrDefault();

            RecivedRow.UpdateDateTime = DateTime.Now;
            RecivedRow.Status = Status;
            return _requestService.Update(RecivedRow);
        }

     
    }
}
