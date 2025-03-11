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
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService service;

        public CartItemsController(ICartItemService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetCartItems());
        }

        [HttpGet]
        [Route("user-cart/{id}")]
        public IActionResult GetCart(int id)
        {
            return Ok(service.GetCartItemsByUserId(id));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(service.GetCartItem(id));
        }

        [HttpPost]
        public IActionResult Post(CartItem product)
        {
            return StatusCode(201, service.AddCartItem(product));
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, CartItem product)
        {
            return Ok(service.UpdateCartItem(id, product));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(service.DeleteCartItem(id));
        }
    }
}
