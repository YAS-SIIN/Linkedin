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
            Configuration = configuration;
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

            User RecivedUserRow = _userservice.GetByUserId(UserId);
            return _visitservice.GetAll(x => x.UserId == RecivedUserRow.Id).Take(100);
        }

        [HttpPost]
        public bool Post(string UserId)
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            int countVisitToRequest = int.Parse(Configuration["CountVisitToRequest"]);

            return _userservice.VisitUser(UserId, countVisitToRequest);
                 
        }

     
    }
}
