using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Services;
using EcomWebAPIServer2.Exception;


namespace EcomWebAPIServer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ExceptionHandler]
    //[MyAsyncActionFilter("Async controller")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetUsers());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(service.GetUser(id));
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            return StatusCode(201, service.AddUser(user));
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, User user)
        {
            return Ok(service.UpdateUser(id, user));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(service.DeleteUser(id));
        }
    }

}

