using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Exception;
using EcomWebAPIServer2.Services;
using Microsoft.AspNetCore.Authorization;

namespace EcomWebAPIServer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionHandler]
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Get()
        {
            var products = service.GetProducts();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Get(int id)
        {
            return Ok(service.GetProduct(id));
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Post(Product product)
        {
            return StatusCode(201, service.AddProduct(product));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Put(int id, Product product)
        {
            return Ok(service.UpdateProduct(id, product));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Delete(int id)
        {
            return Ok(service.DeleteProduct(id));
        }
    }
}
