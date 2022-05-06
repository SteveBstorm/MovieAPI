using DAL.Interface;
using IMDBApi.Models;
using LocalModel.Models;
using LocalModel.Services.Interface;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IMDBApi.Services
{
    public class TokenService : ITokenService
    {

       // private List<User> _users;
        private readonly AppSettings _appSettings;
        private IUserService _userService;

        public TokenService(IOptions<AppSettings> app, IUserService userService)
        {
            _appSettings = app.Value;
            _userService = userService;

           // _users = _userService.GetAll().ToList();
        }

        public ConnectedUser Authenticate(string email, string password)
        {
            User user;
            if (_userService.CheckUser(new User { Email = email, Password = password }) == true)
            {
                user = _userService.GetByMail(email);
                //user = _users.SingleOrDefault(x => x.Email == email && x.Password == password);
            }
            else return null;
            

            //if (user == null) return null;

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
                }),
                Issuer = _appSettings.Issuer,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return new ConnectedUser
            {
                Id = user.Id,
                Email = user.Email,
                BirthDate = user.BirthDate,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                IsAdmin = user.IsAdmin,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
