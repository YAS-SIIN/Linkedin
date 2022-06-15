﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Linkedin.Models;
using Linkedin.Service.UserService;
using System.Net.Http;
using static Linkedin.Common.TypeEnum;

namespace Linkedin.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       private readonly ILogger<UserController> _logger;
        private readonly IUserService _userservice;          
        public UserController(ILogger<UserController> logger, IUserService userservice)
        {
            _logger = logger;
            _userservice = userservice;                                                       
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {                                                                                      
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            return _userservice.GetAll();
        }

        [HttpPost]
        public User Post([FromBody] User User)
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            User.Status = (short)UserStatus.Submit;
            User.CreateDateTime = DateTime.Now;
            return _userservice.Insert(User);
        }
                    
        [HttpDelete]
        public User DeleteByUser(string UserId)
        {
            _logger.LogInformation($"ControllerName: {ControllerContext.RouteData.Values["action"] } - ActionName: {ControllerContext.RouteData.Values["action"] }");

            User RecivedUserRow = _userservice.GetAll().Where(a => a.ExternalUserId == UserId).FirstOrDefault();
            RecivedUserRow.Status = (short)UserStatus.Deleted;
            return _userservice.Update(RecivedUserRow);
        }

      
    }
}
