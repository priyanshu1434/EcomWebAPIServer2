using EcomWebAPIServer2.Exception;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomWebAPIServer2.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        [ExceptionHandler]
        public class OrderController : ControllerBase
        {
            private readonly IOrderService service;

            public OrderController(IOrderService service)
            {
                this.service = service;
            }

            [HttpGet]
            [Authorize(Roles = "admin,User")]
            
            public IActionResult Get()
            {
                return Ok(service.GetOrders());
            }

            [HttpGet]
            [Route("{id}")]
            [Authorize(Roles = "admin,User")]
            public IActionResult Get(int id)
            {
                return Ok(service.GetOrder(id));
            }

            [HttpGet]
            [Route("user-order/{id}")]
            [Authorize(Roles = "admin,User")]
            public IActionResult Getorder(int id)
            {
                return Ok(service.GetOrdersByUserId(id));
            }

            [HttpPost]
            [Authorize(Roles = "admin,User")]
            public IActionResult Post(int userid, string shippingaddress)
            {
            var qq = new Order
            {
                UserId = userid,
                ShippingAddress = shippingaddress
            };
            return StatusCode(201, service.AddOrder(qq));
            }

            [HttpPut]
            [Route("{id}")]
            [Authorize(Roles = "admin,User")]
            public IActionResult Put(int id, Order order)
            {
                return Ok(service.UpdateOrder(id, order));
            }

            [HttpDelete]
            [Route("{id}")]
            [Authorize(Roles = "admin,User")]
            public IActionResult Delete(int id)
            {
                return Ok(service.DeleteOrder(id));
            }
        }
}
