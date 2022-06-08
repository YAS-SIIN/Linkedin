﻿using Linkedin.Models;
using Linkedin.Service.UserService;
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
        private readonly IUserService _userservice;
        public VisitController(ILogger<VisitController> logger, IVisitService visitservice, IUserService userservice)
        {
            _logger = logger;
            _visitservice = visitservice;
            _userservice = userservice;
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
        public Visit Post([FromBody] Visit Visit)
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");
               
            Visit.CreateDateTime = DateTime.Now;
            return _visitservice.Insert(Visit);
        }

     
    }
}
