using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IConfiguration _configuration;
        IUserService _userService;

        public AuthManager(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByEmail(userForLoginDto.Email);
            if (userToCheck.Data == null)
                return new ErrorDataResult<User>(userToCheck.Message);

            if (userToCheck.Data.Password != userForLoginDto.Password)
                return new ErrorDataResult<User>(Messages.PasswordError);

            var userRoleClaims = _userService.GetClaims(userToCheck.Data).Data;
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, userToCheck.Data.FirstName + " " + userToCheck.Data.LastName),
               new Claim(ClaimTypes.Email, userToCheck.Data.Email),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoleClaims)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole.Name));
            }
            string token = GenerateToken(authClaims);
            return new SuccessDataResult<User>(token);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            var user = new User()
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                Password = userForRegisterDto.Password
            };

            _userService.Add(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            string token = GenerateToken(authClaims);
            return new SuccessDataResult<User>(token);

        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
