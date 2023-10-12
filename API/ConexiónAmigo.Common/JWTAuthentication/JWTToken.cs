using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Common.JWTAuthentication
{
    public static class JWTToken
    {
        public static string GenerateToken(string Email, string UserId, string role, string secretKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new ClaimsIdentity(
                new Claim[]{
                    new Claim("Email" , Email),
                    new Claim("UserId" , UserId),
                    new Claim(ClaimTypes.Role , role)
                });

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = "Test",
                Audience = "Test",
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = signInCredentials
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);


            return handler.WriteToken(token);
        }
    }
}
