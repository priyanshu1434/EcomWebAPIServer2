using EcomWebAPIServer2.Exception;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Services;
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
            public IActionResult Get()
            {
                return Ok(service.GetOrders());
            }

            [HttpGet]
            [Route("{id}")]
            public IActionResult Get(int id)
            {
                return Ok(service.GetOrder(id));
            }

            [HttpGet]
            [Route("user-order/{id}")]
            public IActionResult Getorder(int id)
            {
                return Ok(service.GetOrdersByUserId(id));
            }

        [HttpPost]
            public IActionResult Post(Order order)
            {
                return StatusCode(201, service.AddOrder(order));
            }

            [HttpPut]
            [Route("{id}")]
            public IActionResult Put(int id, Order order)
            {
                return Ok(service.UpdateOrder(id, order));
            }

            [HttpDelete]
            [Route("{id}")]
            public IActionResult Delete(int id)
            {
                return Ok(service.DeleteOrder(id));
            }
        }
}
