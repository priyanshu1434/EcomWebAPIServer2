using EcomWebAPIServer2.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcomWebAPIServer2
{
    public class Auth : IAuth
    {
        private readonly string key;
        private readonly EcomContext _context;

        public Auth(string key, EcomContext context)
        {
            this.key = key;
            _context = context;
        }

        public AuthResult Authentication(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == username && u.Password == password);

            if (user == null)
            {
                return null;
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
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // 4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token, UserId, and Role from method
            return new AuthResult
            {
                Token = tokenHandler.WriteToken(token),
                UserId = user.UserId,
                Role = user.Role
            };
        }

        public class AuthResult
        {
            public string Token { get; set; }
            public int UserId { get; set; }
            public string Role { get; set; }
        }
    }
}
