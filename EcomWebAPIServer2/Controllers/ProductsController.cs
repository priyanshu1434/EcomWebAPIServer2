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

namespace EcomWebAPIServer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionHandler]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService service;

        public ProductsController(IProductService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetProducts());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(service.GetProduct(id));
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            return StatusCode(201, service.AddProduct(product));
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, Product product)
        {
            return Ok(service.UpdateProduct(id, product));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(service.DeleteProduct(id));
        }
    }
}
