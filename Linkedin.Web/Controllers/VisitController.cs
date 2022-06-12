using Linkedin.Models;
using Linkedin.Service.Request;
using Linkedin.Service.UserService;
using Linkedin.Service.Visit;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class VisitController : ControllerBase
    {
        private readonly ILogger<VisitController> _logger;
        private readonly IVisitService _visitservice;
        private readonly IUserService _userservice;
        private readonly IRequestService _requestService;
        public IConfiguration Configuration { get; }

        public VisitController(ILogger<VisitController> logger, IVisitService visitservice, 
            IRequestService requestService, IUserService userservice, IConfiguration configuration)
        {
            _logger = logger;
            _visitservice = visitservice;
            _userservice = userservice;
            _requestService = requestService;
        }

        [HttpGet]
        public IEnumerable<Visit> Get()
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            return _visitservice.GetAll().Take(100);
        }

        [HttpGet, Route("[action]")]
        public IEnumerable<Visit> GetByUser(string UserId)
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            User RecivedUserRow = _userservice.GetAll().Where(a => a.ExternalUserId == UserId).ElementAt(0);
            return _visitservice.GetAll().Where(x => x.UserId == RecivedUserRow.Id).Take(100);
        }

        [HttpPost]
        public Visit Post(string UserId)
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            int countVisitToRequest = int.Parse(Configuration["CountVisitToRequest"]);

            User RecivedUserRow = _userservice.GetAll().Where(a => a.ExternalUserId == UserId).ElementAt(0);

            RecivedUserRow.VisitCount += 1;  
            _userservice.Update(RecivedUserRow);

            if (RecivedUserRow.VisitCount > countVisitToRequest)
            {
                Request RecivedRequestRow = _requestService.GetAll().Where(a => a.UserId == RecivedUserRow.Id).ElementAt(0);
                RecivedRequestRow.Status = (short)RequestStatus.Scheduled;
                RecivedRequestRow.UpdateDateTime = DateTime.Now;
                _requestService.Update(RecivedRequestRow);      
            }

            Visit ObjVisit = new Visit();
            ObjVisit.CreateDateTime = DateTime.Now;
            ObjVisit.Id = RecivedUserRow.Id;

            return _visitservice.Insert(ObjVisit);
        }

     
    }
}
