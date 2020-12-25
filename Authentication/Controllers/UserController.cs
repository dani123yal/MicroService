using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Helper;
using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private User CurrentUser { get { return (User)HttpContext.Items["User"]; } }
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        [Authorize]
        public IActionResult GetUsers()
        {
            List<string> users = new List<string>()
            {
                "Dani",
                "Baig",
                "Aishah"
            };
            return Ok(users);
        }

        [HttpGet("Authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
