using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDBApi.Models;
using IMDBApi.Tools;
using LocalModel.Models;
using LocalModel.Services;
using LocalModel.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]NewUserInfo newUser)
        {
            try
            {
                _userService.Register(newUser.toLocal());
                return Ok("Compte enregistré");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize("Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_userService.GetAll());
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize("Admin")]
        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            try
            {
                return Ok(_userService.GetOne(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize("Admin")]
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                _userService.Switchactivate(Id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize("User")]
        [HttpPut]
        public IActionResult Update(User u)
        {
            try
            {
                _userService.Update(u);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize("Admin")]
        [HttpPut("{Id}")]
        public IActionResult SwitchAdmin(int Id)
        {
            try
            {
                _userService.SwitchAdmin(Id);
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}