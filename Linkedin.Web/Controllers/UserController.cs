using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Linkedin.Models;
using Linkedin.Service.UserService;
using System.Net.Http;

namespace Linkedin.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly ILogger<UserController> _logger;
        private readonly IUserService _userservice;
        public UserController(IUserService userservice)
        {
            //_logger = logger;
            _userservice = userservice;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userservice.GetAll();
        }
         
        [HttpPost]
        public User Post([FromBody] User User)
        {
         return   _userservice.Insert(User);
        }
         
        [HttpPut]
        public User Update([FromBody] User User )
        {
            return _userservice.Update(User);
        }

        [HttpDelete]
        public User Delete([FromBody] User User)
        {
            return _userservice.Delete(User);
        }
    }
}
