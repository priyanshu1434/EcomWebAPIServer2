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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "User")]
        public IActionResult Get()
        {
            return Ok(service.GetCartItems());
        }

        [HttpGet]
        [Route("user-cart/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult GetCart(int id)
        {
            return Ok(service.GetCartItemsByUserId(id));
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Get(int id)
        {
            return Ok(service.GetCartItem(id));
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Post(int productid, int userid, int quantity)

        {
            var p = new CartItem
            {
                ProductId = productid,
                UserId = userid,
                Quantity = quantity
            };
            return StatusCode(201, service.AddCartItem(p));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Put(int id, CartItem product)
        {
            return Ok(service.UpdateCartItem(id, product));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Delete(int id)
        {
            return Ok(service.DeleteCartItem(id));
        }
    }
}