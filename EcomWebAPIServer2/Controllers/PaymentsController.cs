using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Exception;
using EcomWebAPIServer2.Services;

namespace EcomWebAPIServer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionHandler]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService service;

        public PaymentController(IPaymentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetPayments());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(service.GetPayment(id));
        }

        [HttpGet]
        [Route("user-payments/{id}")]
        public IActionResult GetPayment(int id)
        {
            return Ok(service.GetPaymentById(id));
        }

        [HttpPost]
        public IActionResult Post(Payment payment)
        {
            return StatusCode(201, service.AddPayment(payment));
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, Payment payment)
        {
            return Ok(service.UpdatePayment(id, payment));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(service.DeletePayment(id));
        }
    }
}