using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using EcomWebAPIServer2.Exception;

namespace EcomWebAPIServer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;
        private static readonly Random _random = new Random();
        private static readonly Dictionary<int, int> _passwordResetRequests = new Dictionary<int, int>(); // UserId, OTP

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Authorize(Roles = "User, admin")]
        public IActionResult Get()
        {
            var users = service.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "admin,User")]
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

            try
            {
                service.AddUser(user);
                return StatusCode(201, "User created successfully");
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "User, admin")]
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
        [Authorize(Roles = "admin,User")]
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

            // Generate a random OTP
            int otp = _random.Next(100000, 999999); // 6-digit OTP

            // Store the OTP associated with the user ID
            _passwordResetRequests[user.UserId] = otp;

            // In a real application, you would send this OTP to the user's email/phone.
            // Since you're not using external services, we'll just return it for demonstration.
            Console.WriteLine($"Generated OTP for User ID {user.UserId}: {otp}"); // For demonstration purposes

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
                // OTP is valid, proceed to reset the password
                _passwordResetRequests.Remove(userId);

                var user = service.GetUser(userId);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                // Update the user's password in the database
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
