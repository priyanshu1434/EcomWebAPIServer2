using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace EcomWebAPIServer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;
        private static readonly Random _random = new Random();
        private static readonly Dictionary<int, int> _passwordResetRequests = new Dictionary<int, int>(); 

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Get()
        {
            var users = service.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Get(int id)
        {
            return Ok(service.GetUser(id));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post(string Name, string Email, string Password, long Phonenumber, string Address)
        {
            var user = new User
            {
                Name = Name,
                Email = Email,
                Password = Password,
                PhoneNumber = Phonenumber,
                Address = Address,
            };
            return StatusCode(201, service.AddUser(user));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Put(int id, string Name, string Email, string Password, long Phonenumber, string Address)
        {
            var user = new User
            {
                Name = Name,
                Email = Email,
                Password = Password,
                PhoneNumber = Phonenumber,
                Address = Address,
            };
            return Ok(service.UpdateUser(id, user));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Delete(int id)
        {
            return Ok(service.DeleteUser(id));
        }

        [HttpPost]
        [Route("forgot-password")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(string email)
        {
            var user = service.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound(new { message = "User with this email not found." });
            }

            
            int otp = _random.Next(100000, 999999); 

           
            _passwordResetRequests[user.UserId] = otp;

            
            Console.WriteLine($"Generated OTP for User ID {user.UserId}: {otp}"); 

            return Ok(new { message = "OTP generated successfully. Please use it to reset your password.", otp = otp });
        }

        [HttpPost]
        [Route("reset-password")]
        [AllowAnonymous]
        public IActionResult ResetPassword(int otp, string newPassword)
        {
            var userId = _passwordResetRequests.FirstOrDefault(x => x.Value == otp).Key;
            if (userId != 0)
            {
               
                _passwordResetRequests.Remove(userId);

                var user = service.GetUser(userId);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                service.UpdatePassword(userId, newPassword);

                return Ok(new { message = "Password reset successfully." });
            }
            else
            {
                return BadRequest(new { message = "Invalid OTP." });
            }
        }
    }
}
