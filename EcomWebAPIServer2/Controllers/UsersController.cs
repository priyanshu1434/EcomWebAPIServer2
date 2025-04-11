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
        //public IActionResult Post(User user)
        //{
        //    return StatusCode(201, service.AddUser(user));
        //}
        public IActionResult Post( string Name, string Email, string Password, long Phonenumber, string Address)
        {
            var user = new User
            {
                

                Name = Name,
                Email = Email,
                Password = Password,
                PhoneNumber = Phonenumber,
                Address = Address,
               
                

            };
            return StatusCode(201, service.AddUser(user));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Put(int id, string Name, string Email, string Password, long Phonenumber, string Address)
        {
            var user = new User
            {


                Name = Name,
                Email = Email,
                Password = Password,
                PhoneNumber = Phonenumber,

                Address = Address,



            };
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
        // POST api/<UsersController>/authentication
        [HttpPost("authentication")]
        public IActionResult Authentication(string email, string password)
        {
            var result = jwtAuth.Authentication(email, password);
            if (result == null)
            { 
                return Unauthorized();
            }
               

            return Ok(new { Token = result.Value.Token, UserId = result.Value.UserId });
        }
    }
}