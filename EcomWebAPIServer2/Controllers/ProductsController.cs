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
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Cors;

namespace EcomWebAPIServer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    [ExceptionHandler]
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        [HttpGet]
        //[Authorize(Roles = "User")]
        public IActionResult Get()
        {
            var products = service.GetProducts();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize(Roles = "User")]
        public IActionResult Get(int id)
        {
            return Ok(service.GetProduct(id));
        }

        [HttpPost]
        //[Authorize(Roles = "User")]
        public IActionResult Post(int productid, string productname, string productdescription, double productprice, string productcategory, string productImgurl)
        {
            var qq = new Product
            {
                ProductId = productid,
                ProductName = productname,
                ProductDescription = productdescription,
                ProductPrice = productprice,
                ProductCategory = productcategory,
                ProductImgURL = productImgurl
            };
            return StatusCode(201, service.AddProduct(qq));
        }

        [HttpPut]
        [Route("{id}")]
        //[Authorize(Roles = "User")]
        public IActionResult Put(int id, Product product)
        {
            return Ok(service.UpdateProduct(id, product));
        }

        [HttpDelete]
        [Route("{id}")]
        //[Authorize(Roles = "User")]
        public IActionResult Delete(int id)
        {
            return Ok(service.DeleteProduct(id));
        }
    }
}
