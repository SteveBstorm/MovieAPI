using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDBApi.Models;
using IMDBApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Retourne un token sur base d'un model loginInfo
        /// </summary>
        /// <param name="model">LoginInfo</param>
        /// <returns>UserWithToken</returns>
        [HttpPost("auth")]
        public IActionResult Auth([FromBody]LoginInfo model)
        {
            UserWithToken user = _tokenService.Authenticate(model.Email, model.Password);

            if(user == null)
            {
                return BadRequest(new { message = "Utilisateur inexistant !" });
            }

            return Ok(user);
        }

    }
}