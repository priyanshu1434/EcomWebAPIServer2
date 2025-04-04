﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using EcomWebAPIServer2.Models;

namespace EcomWebAPIServer2.Services
{
    public class Auth : IAutho
    {
        private readonly string key;
        private readonly EcomContext context;

        public Auth(string key, EcomContext context)
        {
            this.key = key;
            this.context = context;
        }

        public (string Token, int UserId)? Authentication(string email, string password)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                return (null, 0);
            }

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypt
            var tokenKey = Encoding.ASCII.GetBytes(key);

            // 3. Create JWT descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // 4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token and UserId from method
            return (tokenHandler.WriteToken(token), user.UserId);
        }
    }
}
