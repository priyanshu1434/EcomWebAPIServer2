using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Services;
using System.IO;

namespace EcomWebAPIServer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IAutho jwtAuth;

        public UsersController(IUserService service, IAutho jwtAuth)
        {
            this.service = service;
            this.jwtAuth = jwtAuth;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Get()
        {
            var users = service.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Get(int id)
        {
            return Ok(service.GetUser(id));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post(User user)
        {
            return StatusCode(201, service.AddUser(user));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Put(int id, User user)
        {
            return Ok(service.UpdateUser(id, user));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Delete(int id)
        {
            return Ok(service.DeleteUser(id));
        }

       

        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredentials user)
        {
            var token = jwtAuth.Authentication(user.Name, user.Password);
            if (token == null)
                return Unauthorized();
            return Ok(new { Token = token });
        }
        public class UserCredentials
        {
            public string Name { get; set; }
            public string Password { get; set; }
        }
    }
}