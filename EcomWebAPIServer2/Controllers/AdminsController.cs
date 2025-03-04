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
    [ExceptionHandler]
    public class AdminsController : ControllerBase
    {

        private readonly IAdminService service;

        public AdminsController(IAdminService service)
        {
            this.service = service;

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetAdmins());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(service.GetAdmin(id));
        }

        [HttpPost]
        public IActionResult Post(Admin user)
        {
            return StatusCode(201, service.AddAdmin(user));
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, Admin user)
        {
            return Ok(service.UpdateAdmin(id, user));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(service.DeleteAdmin(id));
        }
    }
}
