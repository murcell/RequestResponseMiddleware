using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using MiddlewareTestApi.Models;
using Microsoft.Extensions.Logging;

namespace MiddlewareTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;

        public UserController(ILogger<UserController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("id")]
        public IActionResult GetUserInfo(int id)
        {
            logger.LogInformation("Hello from GetUserInfo method");
            return Ok(new UserLoginResponseModel() { Success = true, UserEmail = "murselgurkan@gmail.com" });
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]UserLoginRequestModel model)
        {
            logger.LogInformation("Hello from Login method");
            return Ok(new UserLoginResponseModel() { Success = true, UserEmail = "murselgurkan@gmail.com" });
        }

        [HttpPost]
        [Route("loginOnly")]
        public IActionResult LoginOnly([FromBody] UserLoginRequestModel model)
        {
            logger.LogInformation("Hello from LoginOnly method");
            return Ok();
        }
    }


}
