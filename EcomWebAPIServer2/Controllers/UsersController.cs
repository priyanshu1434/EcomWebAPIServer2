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
using Microsoft.AspNetCore.Cors;

namespace EcomWebAPIServer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        //[Authorize(Roles = "User")]
        public IActionResult Get()
        {
            var users = service.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,User")]
        public IActionResult Get(int id)
        {
            return Ok(service.GetUser(id));
        }

        [HttpGet("email/{email}")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await service.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        //[AllowAnonymous]
        public IActionResult Post(string Name, string Email, string Password, long Phonenumber, string Address)
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

        [HttpPut("{id}")]
        //[Authorize(Roles = "User")]
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

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin,User")]
        public IActionResult Delete(int id)
        {
            return Ok(service.DeleteUser(id));
        }
    }
}
