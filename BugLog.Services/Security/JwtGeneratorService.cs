using System;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace BugLog.Services.Security
{
    public class JwtGeneratorService : IJwtGeneratorService
    {
        public string GenerateJwtToken(SystemUser user) {

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("n:LY;6p(c<EGC6TX+ER>$bj,@SWQ4Q!@{6RH;B#{s9C7mUqd"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}