using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NeighbourhoodHelp.Infrastructure.Interfaces;
using NeighbourhoodHelp.Model.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }


        //using user info to gen token
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName)

            };


            //this if the token is valid then it encrypts it
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //info about the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]

            };

            //gen token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken((tokenDescriptor));

            return tokenHandler.WriteToken(token);
        }


    }
}
