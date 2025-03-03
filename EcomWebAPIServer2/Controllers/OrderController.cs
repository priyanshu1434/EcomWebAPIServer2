using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomWebAPIServer2.Controllers
{
    public class OrderController
    {
        [Route("api/[controller]")]
        [ApiController]
        public class OederController : ControllerBase
        {
            private readonly IOrderService service;

            public OederController(IOrderService order)
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
}
